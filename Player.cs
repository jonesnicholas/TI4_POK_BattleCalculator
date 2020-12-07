using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TI4BattleSim
{
    public enum Faction
    {
        None, Arborec, Barony, Saar, Muaat, 
        Hacan, Sol, Ghosts, L1, Mentak, 
        Naalu, Nekro, Sardakk, Jolnar, Winnu, 
        Xxcha, Yin, Yssaril,
        Argent, Empyrean, Mahact, NRA, Nomad,
        Titans, Cabal
    };

    public enum HitType
    {
        Generic, Graviton, AFB, X89
    };

    public class Player
    {
        public List<Unit> units;
        public Faction faction; //todo: may need to use class instead of enum for agents
        public TechModel techs;
        public List<Faction> commanders;
        public bool isActive;

        public Player()
        {
            units = new List<Unit>();
            faction = Faction.None;
            techs = new TechModel();
            commanders = new List<Faction>();
            isActive = false;
        }

        public static Player CopyPlayer(Player player)
        {
            Player copy = new Player(player.units, player.faction, player.techs, player.commanders, player.isActive);
            return copy;
        }

        public Player(List<Unit> unitMenu, Faction fct, TechModel techModel = null, List<Faction> cmds = null, bool activePlayer = false)
        {
            faction = fct;
            techs = techModel == null ? new TechModel() : TechModel.CopyTechModel(techModel); //todo: verify that these are making copies, not references
            commanders = cmds == null ? new List<Faction>() : new List<Faction>(cmds);
            units = Unit.CreateUnitList(unitMenu, techs, faction);
            isActive = activePlayer;
        }

        public bool HasFlagship()
        {
            if (units == null)
                return false;
            return units.Any(unit => unit.type == UnitType.Flagship);
        }

        public int DoAntiFighterBarrage(Battle battle, Player target)
        {
            Unit highRoller = units.OrderBy(unit => unit.antiFighter.ToHit).First();
            int mod = 0;
            if (commanders.Contains(Faction.Argent))
                mod++;
            highRoller.antiFighter.NumDice += mod;
            int hits = units.Sum(unit => unit.antiFighter.doCombat(battle, this, target));
            highRoller.antiFighter.NumDice -= mod;
            return hits;
        }

        public void DoStartOfCombatRound(Theater theater)
        {
            if (faction == Faction.Barony && theater == Theater.Space)
            {
                foreach (Unit flagship in units.Where(unit => unit.type == UnitType.Flagship))
                {
                    flagship.damage = Damage.None;
                }
            }
        }

        public int DoCombatRolls(Battle battle, Player target, Theater theater)
        {
            //todo: try to make NRA flagship cleaner
            if (faction == Faction.NRA && HasFlagship())
            {
                foreach (Unit mech in units.Where(unit => unit.type == UnitType.Mech))
                {
                    mech.spaceCombat.NumDice++;
                    mech.groundCombat.NumDice++;
                }
            }

            int hits = 0;
            if (theater == Theater.Space)
            {
                hits =  units.Sum(unit => unit.spaceCombat.doCombat(battle, this, target));
            }
            if (theater == Theater.Ground)
            {
                hits =  units.Sum(unit => unit.groundCombat.doCombat(battle, this, target));
            }
            if (theater == Theater.Hybrid)
                throw new Exception("Tried to roll for combat without correct Theater");

            if (faction == Faction.NRA && HasFlagship())
            {
                foreach (Unit mech in units.Where(unit => unit.type == UnitType.Mech))
                {
                    mech.spaceCombat.NumDice--;
                    mech.groundCombat.NumDice--;
                }
            }

            return hits;
        }

        public int DoSpaceCannonOffense(Battle battle, Player target)
        {
            if (target.faction == Faction.Argent && target.HasFlagship())
                return 0;
            // todo incorporate:
            //  JolNar Commander
            Unit highRoller = units.OrderBy(unit => unit.spaceCannon.ToHit).First();

            int mod = 0;
            if (commanders.Contains(Faction.Argent))
                mod++;
            if (HasTech(Tech.PlasmaScoring))
                mod++;

            highRoller.spaceCannon.NumDice += mod;

            int hMod = target.HasTech(Tech.Antimass) ? -1 : 0;

            int hits = units.Sum(unit => unit.spaceCannon.doCombat(battle, this, target, hitMod:hMod));
            highRoller.spaceCannon.NumDice -= mod;
            return hits;
        }

        internal int DoSpaceCannonDefense(Battle battle, Player target)
        {
            if (target.HasTech(Tech.L4Disruptors))
                return 0;

            // todo incorporate:
            //  JolNar Commander
            Unit highRoller = units.Where(unit => unit.theater != Theater.Space).OrderBy(unit => unit.spaceCannon.ToHit).First();
            int mod = 0;
            if (commanders.Contains(Faction.Argent))
                mod++;
            if (HasTech(Tech.PlasmaScoring))
                mod++;

            highRoller.spaceCannon.NumDice += mod;

            int hMod = target.HasTech(Tech.Antimass) ? -1 : 0;

            int hits = 
                units.Where(unit => unit.theater != Theater.Space)
                .Sum(unit => unit.spaceCannon.doCombat(battle, this, target, hitMod:hMod));
            highRoller.spaceCannon.NumDice -= mod;
            return hits;
        }

        public int DoBombardment(Battle battle, Player target)
        {
            if (target.units.Any(unit => unit.hasPlanetaryShield) && !target.units.Any(unit => unit.bypassPlanetaryShield))
                return 0;
            // todo incorporate:
            //  JolNar Commander
            // x89?
            Unit highRoller = units.OrderBy(unit => unit.bombard.ToHit).First();
            int mod = 0;
            if (commanders.Contains(Faction.Argent))
                mod++;
            if (HasTech(Tech.PlasmaScoring))
                mod++;

            highRoller.bombard.NumDice += mod;
            int hits = units.Sum(unit => unit.bombard.doCombat(battle, this, target));
            highRoller.bombard.NumDice -= mod;
            return hits;
        }

        public void AssignHits(Battle battle, int hits, Player source, Theater theater, HitType hitType = HitType.Generic)
        {
            if (hits <= 0)
                return;

            if (hitType == HitType.AFB)
            {
                AssignAFBHits(battle, hits, source);
                return;
            }
            //todo: handle graviton
            //todo: properly handle Titan PDS in ground combats

            //todo: find way to intelligently 'switch in' different hit assigning philosophies.
            // first, assign to safe sustains
            hits = AssignToSustains(hits, source, theater, safe:true);
            // then, if risking direct hit, assign to unsafe sustains
            hits = AssignToSustains(hits, source, theater);
            // then, assign to ships that might blow up
            AssignDestroys(battle, hits, source, theater);
        }

        void AssignAFBHits(Battle battle, int hits, Player source)
        {
            if (hits <= 0)
                return;
            List<Unit> fighters = units.Where(unit => unit.type == UnitType.Fighter).ToList();
            while (fighters.Count > 0 && hits > 0)
            {
                Unit fighter = fighters.First();
                fighters.Remove(fighter);
                fighter.DestroyUnit(battle, this);
                units.Remove(fighter);
                hits--;
            }

            //leftover hits wasted unless source is Argent Flight
            if (source.faction == Faction.Argent)
            {
                List<Unit> targets = units.Where(unit =>
                    unit.ParticipatesInCombat(Theater.Space) &&
                    unit.CanSustain(Theater.Space) &&
                    unit.damage == Damage.None
                ).ToList();

                //todo: fully analyze decisions involving Titan Cruisers, Dread 1 v Dread 2, and flagships (including Barony)
                // I *think* Dread2 > cruiser2 > Dread 1 > flaghsip > carrier
                while (targets.Count > 0 && hits > 0)
                {
                    //todo: implement intelligent targeting
                    Unit target = targets.First();
                    // intentionally *not* calling the sustainDamage method, as this doesn't create sustain damage triggers
                    target.damage = Damage.Damaged;
                    targets.Remove(target);
                    hits--;
                }
            }
        }

        int AssignToSustains(int hits, Player source, Theater theater, bool safe = false)
        {
            if (theater == Theater.Space && source.faction == Faction.Mentak && source.HasFlagship())
            {
                return hits;
            }

            List<Unit> targets = new List<Unit>();
            if (theater == Theater.Space)
            {
                targets = units.Where(unit =>
                    unit.ParticipatesInCombat(theater) && //todo: make this work for Nomad Mechs
                    unit.spaceCombat.canSustain &&
                    unit.damage == Damage.None
                ).ToList();
            }
            if (theater == Theater.Ground)
            {
                targets = units.Where(unit => 
                    unit.ParticipatesInCombat(theater) &&
                    unit.groundCombat.canSustain &&
                    unit.damage == Damage.None
                ).ToList();
            }
            if (safe)
            {
                targets = targets.Where(unit =>
                unit.type == UnitType.Mech || //todo: also make this work for Titans PDS
                unit.type == UnitType.Dreadnought && unit.upgraded
                ).ToList();
            }
            //todo: most of the time, this will be dreads or mechs. 
            //todo: add UI Cruisers and PDS
            //todo: add Nomad and NaazRokha Mechs to space
            //todo: support barony double-sustain
            //todo: intelligently sort and eliminate low-priority targets first
            while (targets.Count > 0 && hits > 0)
            {
                Unit target = targets.First();
                target.SustainDamage();
                hits -= (HasTech(Tech.NES) ? 2 : 1);
            }

            return hits;
        }

        void AssignDestroys(Battle battle, int hits, Player source, Theater theater)
        {
            // Note, if we reach here, we assume that we've already sustain damage on everything we safely can, 
            // and therefore assign hits assuming we are going to start losing things
            // todo: find a way to intelligently 'swap in' targeting logic.
            // for now, sort by 'combat rating', and lose
            // fighters < destroyer < cruiser < dreads <= flagship < warsun
            // with carriers opportunistically lost when their capacity is "No longer needed"

            // todo: make sure to keep one boot when attacking w/ Naalu flagship

            List<Unit> targets = units.Where(unit =>
                unit.ParticipatesInCombat(theater)
            ).ToList();
            //todo: make sure Nomad mechs properly handled

            //todo: implement intelligent capacity saving in space battles
            //todo: consider implementing priority queue
            targets.Sort(Unit.SortCombat(theater));
            bool mentak = theater == Theater.Space && source.faction == Faction.Mentak && source.HasFlagship();
                //todo: also mentak mechs


            while (hits > 0 && targets.Count > 0)
            {
                Unit lowPri = targets.First();
                if (lowPri.CanSustain(theater) && lowPri.damage == Damage.None && !mentak)
                {
                    // sustain and sort target list again (priority queue will make this better)
                    lowPri.SustainDamage();
                    hits -= (HasTech(Tech.NES) ? 2 : 1);
                    targets.Sort(Unit.SortCombat(theater));
                }
                else
                {
                    targets.Remove(lowPri); //(priority queue will make this better too)
                    lowPri.DestroyUnit(battle, this);
                    hits--;
                }
            }
        }

        public bool HasTech(Tech tech)
        {
            return techs.HasTech(tech);
        }

        public void AddUnit(UnitType type)
        {
            units.Add(Unit.CreateUnit(type, techs, faction));
        }

        public static List<Faction> GetAllFactions()
        {
            List<Faction> factions = new List<Faction>();
            foreach (Faction faction in Enum.GetValues(typeof(Faction)))
            {
                factions.Add(faction);
            }
            return factions;
        }
    }
}
