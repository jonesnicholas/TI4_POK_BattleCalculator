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

    /// <summary>
    /// Enumeration of tasks a player may resolve at the Start of a Combat
    /// </summary>
    public enum StartTask
    {
        AssaultCannon, MagenOmega, MentakPrefire
    }

    public class Player
    {
        public List<Unit> units;
        public Faction faction; //todo: may need to use class instead of enum for agents
        public TechModel techs;
        public List<Faction> commanders;
        public OptionsModel options;
        public bool isActive;

        private List<StartTask> startTasks;

        public Player()
        {
            units = new List<Unit>();
            faction = Faction.None;
            techs = new TechModel();
            commanders = new List<Faction>();
            options = new OptionsModel();
            isActive = false;
        }

        public static Player CopyPlayer(Player player)
        {
            Player copy = new Player(player.units, player.faction, player.techs, player.commanders, player.options, player.isActive);
            return copy;
        }

        public Player(List<Unit> unitMenu, Faction fct, TechModel techModel = null, List<Faction> cmds = null, OptionsModel optionModel = null, bool activePlayer = false)
        {
            faction = fct;
            techs = techModel == null ? new TechModel() : TechModel.CopyTechModel(techModel); //todo: verify that these are making copies, not references
            commanders = cmds == null ? new List<Faction>() : new List<Faction>(cmds);
            options = optionModel == null ? new OptionsModel() : OptionsModel.CopyOptionsModel(optionModel);
            units = Unit.CreateUnitList(unitMenu, techs, faction);
            isActive = activePlayer;
        }

        public bool HasFlagship(Faction fctTest = Faction.None)
        {
            if (units == null)
                return false;
            
            bool hasOne = units.Any(unit => unit.type == UnitType.Flagship);
            if (!hasOne)
                return false;
            if (fctTest != Faction.None)
                return faction == fctTest;
            return true;
        }

        public bool CanMagenDefense()
        {
            return HasTech(Tech.MagenDefenseGrid) && units.Any(unit => unit.hasPlanetaryShield);
        }

        public bool CanMagenOmega()
        {
            return HasTech(Tech.MagenOmega) && units.Any(unit => unit.type == UnitType.PDS || unit.type == UnitType.SpaceDock);
        }

        public int DoAntiFighterBarrage(Battle battle, Player target)
        {
            if (units.Count(unit => unit.HasAFB) == 0)
                return 0;

            Unit highRoller = units.Where(unit => unit.HasAFB).OrderBy(unit => unit.antiFighter.ToHit).First();
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
            // anything that was 'recently damaged' just becomes damaged
            foreach (Unit unit in units)
            {
                if (unit.damage == Damage.RecentlyDamaged)
                    unit.damage = Damage.Damaged;
            }

            if (faction == Faction.Barony && theater == Theater.Space)
            {
                foreach (Unit flagship in units.Where(unit => unit.type == UnitType.Flagship))
                {
                    flagship.damage = Damage.None;
                }
            }
        }

        public void PrepStartOfCombat(Theater theater)
        {
            if (theater == Theater.Space)
                PrepStartOfSpaceCombat();
            if (theater == Theater.Ground)
                PrepStartOfGroundCombat();
        }

        private void PrepStartOfSpaceCombat()
        {
            List<StartTask> tasks = new List<StartTask>();
            if (faction == Faction.Mentak)
                tasks.Add(StartTask.MentakPrefire);
            if (techs.HasTech(Tech.AssaultCannon) && 
                units.Count(unit => unit.ParticipatesInCombat(Theater.Space) && unit.type != UnitType.Fighter) >= 3)
            {
                tasks.Add(StartTask.AssaultCannon);
            }
            startTasks = tasks;
        }

        internal void AssignPDSHits(Battle battle, int hits, Player source, Theater theater, bool isGraviton = false)
        {
            // TODO: Properly handle graviton
            // TODO: Properly handle capacity saving
            // 1) assign to 'safe sustains first'
            AssignToSustains(ref hits, source, theater, safe: true, inCombat: false);

            // 2) assign to risky sustains if desired.
            if (options.riskDirectHit)
            {
                AssignToSustains(ref hits, source, theater, safe: false, inCombat: false);
            }

            // 3) Get 'Assignment profile' to determine number of unsafe sustains needed, and sustain on them
            List<Unit> targetProfile = GetAssignmentProfile(battle, hits, source, theater);
            int asus = targetProfile.Count(unit => unit.CanSustain(theater) && unit.damage == Damage.None);
            hits -= asus;
            AssignToSustains(ref asus, source, theater, safe: false, inCombat: false);

            // 4) assing remaining hits to weakest ships
            AssignHits(battle, ref hits, source, theater);
        }

        private void PrepStartOfGroundCombat()
        {
            List<StartTask> tasks = new List<StartTask>();
            if (techs.HasTech(Tech.MagenOmega) && 
                units.Any(
                    unit => unit.type == UnitType.PDS || 
                    unit.type == UnitType.SpaceDock && unit.theater == Theater.Ground))
            {
                tasks.Add(StartTask.MagenOmega);
            }
            startTasks = tasks;
        }

        public bool DoStartOfCombat(Battle battle, Player opponent)
        {
            if (startTasks == null || startTasks.Count == 0)
                return false;

            //todo: intelligently strategize order
            //todo: properly handle tasks becoming invalid due to state changes
            StartTask task = startTasks.First();
            switch (task)
            {
                case StartTask.AssaultCannon:
                    // need to recheck num of ships to ensure this is still valid
                    int nfShips = units.Count(unit => unit.ParticipatesInCombat(Theater.Space) && unit.type != UnitType.Fighter);
                    opponent.AssignDestroys(battle, 1, this, Theater.Space);
                    break;
                case StartTask.MentakPrefire:
                    int hits = units
                        .Where(unit => unit.type == UnitType.Destroyer || unit.type == UnitType.Cruiser)
                        .OrderByDescending(unit => unit.spaceCombat.ToHit).Take(2)
                        .Sum(unit => unit.spaceCombat.doCombat(battle, this, opponent));
                    opponent.AssignHits(battle, ref hits, this, Theater.Space);
                    break;
                case StartTask.MagenOmega:
                    // check for structure happens before adding task
                    int hit = 1;
                    opponent.AssignHits(battle, ref hit, this, Theater.Ground);
                    break;

            };
            return true;
        }

        internal void AssignBombardHits(Battle battle, int bombardHits, Player attacker)
        {
            // TODO: Implement X89
            if (bombardHits <= 0)
                return;
            AssignToSustains(ref bombardHits, attacker, Theater.Ground, inCombat: false);
            AssignDestroys(battle, bombardHits, attacker, Theater.Ground);
        }

        private void AssignDestroys(Battle battle, int destroys, Player source, Theater theater)
        {
            List<Unit> targets = units
                .Where(unit => unit.ParticipatesInCombat(theater))
                .OrderBy(unit => unit.TheaterEffectiveness(theater))
                .ToList();
            // todo: try to avoid wasting on things that can still sustain
            while (targets.Count > 0 && destroys > 0)
            {
                Unit target = targets.First();
                targets.Remove(target);
                units.Remove(target);
                destroys--;
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

        internal void DuraniumArmor(Theater theater)
        {
            //If has duranium, repair unit
            if (techs.HasTech(Tech.Duranium))
            {
                List<Unit> candidates =
                    units.Where(unit => unit.ParticipatesInCombat(theater) && unit.damage == Damage.Damaged).ToList();

                if (faction == Faction.Barony && HasFlagship())
                {
                    //don't waste time repairing Barony's flagship
                    candidates.RemoveAll(unit => unit.type == UnitType.Flagship);
                }
                //todo: verify this properly handles the special case of NRA fighters pre-damaged in space
                candidates.OrderBy(unit => theater == Theater.Space ? unit.spaceCombat.effectiveness : unit.groundCombat.effectiveness);

                // todo: titans pds too
                if (candidates.Any(unit => unit.type == UnitType.Mech || (unit.type == UnitType.Dreadnought && unit.upgraded)))
                {
                    //repair safe sustains first
                    candidates.First(unit => unit.type == UnitType.Mech || (unit.type == UnitType.Dreadnought && unit.upgraded)).Repair();
                    return;
                }
                if (candidates.Count > 0)
                    candidates.First().Repair();
            }
        }

        public int DoSpaceCannonOffense(Battle battle, Player target)
        {
            if (target.faction == Faction.Argent && target.HasFlagship())
                return 0;
            if (units.Count(unit => unit.HasSpaceCannon) == 0)
                return 0;

            Unit highRoller = units.Where(unit => unit.HasSpaceCannon).OrderBy(unit => unit.spaceCannon.ToHit).First();

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
            if (units.Count(unit => unit.theater != Theater.Space && unit.HasSpaceCannon) == 0)
                return 0;

            Unit highRoller = units.Where(unit => unit.theater != Theater.Space && unit.HasSpaceCannon).OrderBy(unit => unit.spaceCannon.ToHit).First();
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
            if (target.units.Any(unit => unit.hasPlanetaryShield) && !units.Any(unit => unit.bypassPlanetaryShield))
                return 0;
            if (units.Count(unit => unit.HasBombard) == 0)
                return 0;


            Unit highRoller = units.Where(unit => unit.HasBombard).OrderBy(unit => unit.bombard.ToHit).First();
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

        public void AssignAFBHits(Battle battle, int hits, Player source)
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

        public int AssignToSustains(ref int hits, Player source, Theater theater, bool safe = false, bool inCombat = true)
        {
            if (hits <= 0)
                return 0;

            bool nonSustainable = 
                (theater == Theater.Space && source.HasFlagship(Faction.Mentak)) ||
                (theater == Theater.Ground && source.faction == Faction.Mentak && source.units.Any(unit => unit.type == UnitType.Mech));

            if (nonSustainable)
                return 0;

            // todo: properly handle nomad mech in space
            List<Unit> candidates = units.Where(unit => unit.ParticipatesInCombat(theater) && unit.CanSustain(theater) && unit.damage == Damage.None).ToList();
            if (safe)
            {
                candidates = candidates.Where(unit => unit.SafeSustain()).ToList();
            }

            int bonusHits = 0;
            //todo: properly handle risky sustains on things w/ capacity

            //todo: handle decision space for barony dealing w/ odd numbers of hits + NES

            candidates = candidates.OrderBy(unit => unit.TheaterEffectiveness(theater)).ToList();
            while (candidates.Count > 0 && hits > 0)
            {
                Unit candidate = candidates.First();
                candidate.SustainDamage();
                if (candidate.type == UnitType.Mech && faction == Faction.Sardakk)
                    bonusHits++;
                hits -= techs.HasTech(Tech.NES) ? 2 : 1;
                candidates.Remove(candidate);
            }
            return bonusHits;
        }

        public List<Unit> GetAssignmentProfile(Battle battle, int hits, Player source, Theater theater)
        {
            //todo: need to properly handle holding on to things with capacity
            List<Unit> candidates =
                units.Where(unit => unit.ParticipatesInCombat(theater))
                .OrderBy(unit => unit.damage)
                .OrderBy(unit => unit.TheaterEffectiveness(theater)).ToList();

            return candidates.Take(Math.Min(hits, candidates.Count)).ToList();
        }

        public void AssignHits(Battle battle, ref int hits, Player source, Theater theater, bool forceDestroy = false)
        {
            //todo: need to properly handle holding on to things with capacity
            List<Unit> targets = units
                .Where(unit => unit.ParticipatesInCombat(theater))
                .OrderBy(unit => unit.TheaterEffectiveness(theater))
                .ToList();

            bool nonSustainable =
                (theater == Theater.Space && source.HasFlagship(Faction.Mentak)) ||
                (theater == Theater.Ground && source.faction == Faction.Mentak && source.units.Any(unit => unit.type == UnitType.Mech));

            while (targets.Count > 0 && hits > 0)
            {
                Unit target = targets.First();
                if (!forceDestroy && target.damage == Damage.None && target.CanSustain(theater) && !nonSustainable)
                {
                    // Allowing this for now, but
                    // TODO: Make sure that this isn't called for things that can still potentially sustain
                    // damn Mentak screwing everything up...
                    if (source.faction != Faction.Mentak)
                    {
                        throw new Exception("tried to assign destroy to something that should still be able to sustain");
                    }
                }
                targets.Remove(target);
                units.Remove(target);
                hits--;
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
