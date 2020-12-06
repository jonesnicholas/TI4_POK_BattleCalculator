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

            AntiFighterBarrage();

            EvaluateWinner(Theater.Space);
            if (winner != Winner.None)
                return winner;
            //crs

            while (winner == Winner.None)
            {
                SimulateCombatRound(Theater.Space);
                EvaluateWinner(Theater.Space);
            }

            return winner;
        }

        public void SpaceCannonOffense()
        {
            //pds
            //todo: fully implement graviton
            //todo: verify that xxcha & argent flagships, and xxcha mech are correctly included
            int ApdsHits = attacker.DoSpaceCannonOffense(this, defender);
            int DpdsHits = defender.DoSpaceCannonOffense(this, attacker);
            attacker.AssignHits(DpdsHits, defender, Theater.Space);
            defender.AssignHits(ApdsHits, attacker, Theater.Space);

            foreach (Player player in others)
            {
                int otherHits = player.DoSpaceCannonOffense(this, attacker);
                attacker.AssignHits(otherHits, player, Theater.Space);
            }
        }

        public void AntiFighterBarrage()
        {
            //todo: find all commanders and promissory notes referencing anti-fighter barrage

            int AafbHits = attacker.DoAntiFighterBarrage(this, defender);
            int DafbHits = defender.DoAntiFighterBarrage(this, attacker);

            attacker.AssignHits(DafbHits, defender, Theater.Space, HitType.AFB);
            defender.AssignHits(AafbHits, attacker, Theater.Space, HitType.AFB);
        }

        public Winner SimulateGroundCombat()
        {
            winner = Winner.None;
            //bombard
            //pds
            EvaluateWinner(Theater.Ground);
            if (winner != Winner.None)
                return winner;
            //gcr
            while (winner == Winner.None)
            {
                SimulateCombatRound(Theater.Ground);
                EvaluateWinner(Theater.Ground);
            }
            return winner;
        }

        public void SimulateCombatRound(Theater theater)
        {
            attacker.DoStartOfCombatRound(theater);
            defender.DoStartOfCombatRound(theater);

            int attackerHits = attacker.DoCombatRolls(this, defender, theater);
            int defenderHits = defender.DoCombatRolls(this, attacker, theater);

            attacker.AssignHits(defenderHits, defender, theater);
            defender.AssignHits(attackerHits, attacker, theater);
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
                winner = Winner.Defender; //todo: properly handle Naalu Flagship case.
        }
    }
}
