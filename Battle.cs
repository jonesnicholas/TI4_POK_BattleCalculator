using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TI4BattleSim
{
    public enum Winner
    {
        None, Attacker, Defender, Draw
    };

    public enum Theater
    {
        Space, Ground, Hybrid
    };

    public class Battle
    {
        public Random random;
        public Player attacker;
        public Player defender;
        public List<Player> others;
        Winner winner;
        Theater theater;

        public Battle()
        {

        }

        public Battle(Player at, Player def, List<Player> ot = null, Theater zone = Theater.Hybrid, Random rand = null)
        {
            attacker = at;
            at.isActive = true;
            defender = def;
            others = ot ?? new List<Player>();
            winner = Winner.None;
            theater = zone;
            random = rand ?? new Random();
        }

        public Winner SimulateBattle()
        {
            switch (theater)
            {
                case Theater.Space:
                    SimulateSpaceCombat();
                    break;
                case Theater.Ground:
                    SimulateGroundCombat();
                    break;
                case Theater.Hybrid:
                    SimulateSpaceCombat();
                    if (winner == Winner.Attacker)
                    {
                        SimulateGroundCombat();
                    }
                    break;
            }

            return winner;
        }

        public Winner SimulateSpaceCombat()
        {
            winner = Winner.None;
            SpaceCannonOffense();

            EvaluateWinner(Theater.Space);
            if (winner != Winner.None)
                return winner;

            StartOfCombat(Theater.Space);
            AntiFighterBarrage();

            EvaluateWinner(Theater.Space);

            int combatRound = 0;
            while (winner == Winner.None)
            {
                combatRound++;
                StartOfCombatRound(Theater.Space);
                SimulateCombatRound(Theater.Space, combatRound);
                EvaluateWinner(Theater.Space);
            }

            return winner;
        }

        private void StartOfCombatRound(Theater theater)
        {
            // Nothing yet!
        }

        private void StartOfCombat(Theater theater)
        {
            attacker.PrepStartOfCombat(theater);
            defender.PrepStartOfCombat(theater);

            bool attackerCanRespond = true;
            bool defenderCanRespond = true;
            while (attackerCanRespond || defenderCanRespond)
            {
                if (attackerCanRespond)
                {
                    bool attackerDidSomething = attacker.DoStartOfCombat(this, defender);
                    attackerCanRespond = attackerDidSomething;
                    defenderCanRespond |= attackerDidSomething;
                }
                if (defenderCanRespond)
                {
                    bool defenderDidSomething = defender.DoStartOfCombat(this, attacker);
                    defenderCanRespond = defenderDidSomething;
                    attackerCanRespond |= defenderDidSomething;
                }
            }
        }

        public void SpaceCannonOffense()
        {
            //todo: fully implement graviton
            int ApdsHits = attacker.DoSpaceCannonOffense(this, defender);
            int DpdsHits = defender.DoSpaceCannonOffense(this, attacker);
            attacker.AssignPDSHits(this, DpdsHits, defender, Theater.Space);
            defender.AssignPDSHits(this, ApdsHits, attacker, Theater.Space);

            foreach (Player player in others)
            {
                int otherHits = player.DoSpaceCannonOffense(this, attacker);
                attacker.AssignPDSHits(this, otherHits, player, Theater.Space);
            }
        }

        public void AntiFighterBarrage()
        {
            int AafbHits = attacker.DoAntiFighterBarrage(this, defender);
            int DafbHits = defender.DoAntiFighterBarrage(this, attacker);

            attacker.AssignAFBHits(this, DafbHits, defender);
            defender.AssignAFBHits(this, AafbHits, attacker);
        }

        public Winner SimulateGroundCombat()
        {
            winner = Winner.None;
            //bombard
            Bombardment();
            CommitGroundForces();
            //pds
            SpaceCannonDefense();

            EvaluateWinner(Theater.Ground);
            //gcr
            int combatRound = 0;
            while (winner == Winner.None)
            {
                combatRound++;
                SimulateCombatRound(Theater.Ground, combatRound);
                EvaluateWinner(Theater.Ground);
            }
            EndOfGroundCombat();
            return winner;
        }

        private void EndOfGroundCombat()
        {
            Player victor = winner == Winner.Attacker ? attacker : defender;
            if (victor.HasTech(Tech.Daaxive))
            {
                victor.AddUnit(UnitType.Infantry);
            }
        }

        private void CommitGroundForces()
        {
            if (attacker.faction == Faction.Naalu && attacker.HasFlagship())
            {
                foreach (Unit unit in attacker.units.Where(unit => unit.type == UnitType.Fighter))
                {
                    unit.groundCombat = unit.spaceCombat;
                }
            }
        }

        private void SpaceCannonDefense()
        {
            int spaceCannonHits = defender.DoSpaceCannonDefense(this, attacker);
            attacker.AssignPDSHits(this, spaceCannonHits, defender, Theater.Ground);
        }

        private void Bombardment()
        {
            int bombardHits = attacker.DoBombardment(this, defender);
            //todo: implement X89
            defender.AssignBombardHits(this, bombardHits, attacker);
        }

        public void SimulateCombatRound(Theater theater, int round = 0)
        {
            attacker.DoStartOfCombatRound(theater);
            defender.DoStartOfCombatRound(theater);

            int attackerHits = attacker.DoCombatRolls(this, defender, theater);
            int defenderHits = defender.DoCombatRolls(this, attacker, theater);

            if (theater == Theater.Ground)
            {
                ValkyrieParticleWeave(ref attackerHits, ref defenderHits);
            }

            while (attackerHits > 0 || defenderHits > 0)
            {
                int aBonusHits = 0;
                int dBonusHits = 0;
                // bonus hits come from either 'reflective shielding' or sardakk mechs

                // 1) each player tries to safely sustain
                aBonusHits = attacker.AssignToSustains(ref defenderHits, defender, theater, safe: true);
                dBonusHits = defender.AssignToSustains(ref attackerHits, attacker, theater, safe: true);
                if (aBonusHits > 0 || dBonusHits > 0)
                {
                    attackerHits += aBonusHits;
                    defenderHits += dBonusHits;
                    continue;
                }
                //   b) players risking direct hit try risky sustains
                // 2) assign to risky sustains if desired.
                if (attacker.options.riskDirectHit)
                {
                    attacker.AssignToSustains(ref defenderHits, defender, theater, safe: false, inCombat: false);
                }
                if (defender.options.riskDirectHit)
                {
                    defender.AssignToSustains(ref attackerHits, attacker, theater, safe: false, inCombat: false);
                }

                // 3) each player determines their 'assignment profile'
                //      profile is made assuming all ships selected will be destroyed
                List<Unit> attackerProfile = attacker.GetAssignmentProfile(this, defenderHits, defender, theater);
                List<Unit> defenderProfile = defender.GetAssignmentProfile(this, attackerHits, attacker, theater);
                int asus = attackerProfile.Count(unit => unit.CanSustain(theater) && unit.damage == Damage.None);
                int dsus = defenderProfile.Count(unit => unit.CanSustain(theater) && unit.damage == Damage.None);
                attackerHits -= dsus;
                defenderHits -= asus;

                // 4) each player attempts risky sustains from their 'asignment profile'
                aBonusHits = attacker.AssignToSustains(ref asus, defender, theater, safe: false);
                dBonusHits = defender.AssignToSustains(ref dsus, attacker, theater, safe: false);

                //HACK: handles cases where sustains couldn't be done, generally due to Mentak Mech/Flagship
                //TODO: Figure out more intuitive way to do this
                attackerHits += dsus;
                defenderHits += asus;

                if (aBonusHits > 0 || dBonusHits > 0)
                {
                    attackerHits += aBonusHits;
                    defenderHits += dBonusHits;
                    continue;
                }

                // 5) no more sustains should be possible/need, blow things up from here
                attacker.AssignHits(this, ref defenderHits, defender, theater);
                defender.AssignHits(this, ref attackerHits, attacker, theater);
                break;
            }

            attacker.DuraniumArmor(theater);
            defender.DuraniumArmor(theater);
        }

        private void ValkyrieParticleWeave(ref int attackerHits, ref int defenderHits)
        {
            bool actValk = false;
            if (defenderHits > 0 && attacker.HasTech(Tech.Valkyrie))
            {
                attackerHits++;
                actValk = true;
            }
            if (attackerHits > 0 && defender.HasTech(Tech.Valkyrie))
                defenderHits++;
            if (!actValk && defenderHits > 0 && attacker.HasTech(Tech.Valkyrie))
                attackerHits++;
        }

        public void EvaluateWinner(Theater theater)
        {
            //todo: check for 'unresolvable' combats
            bool attackerAlive = attacker.units.Any(unit => unit.ParticipatesInCombat(theater));
            bool defenderAlive = defender.units.Any(unit => unit.ParticipatesInCombat(theater));
            if (attackerAlive && defenderAlive)
                winner = Winner.None;
            if (attackerAlive && !defenderAlive)
                winner = Winner.Attacker;
            if (!attackerAlive && defenderAlive)
                winner = Winner.Defender;
            if (!attackerAlive && !defenderAlive)
                winner = Winner.Draw;

            if (winner == Winner.Draw && theater == Theater.Ground)
                winner = Winner.Defender; 

            if (winner == Winner.Attacker && attacker.faction == Faction.Naalu && attacker.HasFlagship() &&
                !attacker.units.Any(unit => unit.type == UnitType.Infantry || unit.type == UnitType.Mech))
            {
                winner = Winner.Defender; //if Naalu can't get boots on the ground, they don't technically win
            }
        }
    }
}
