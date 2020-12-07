﻿using System;
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
            int ApdsHits = attacker.DoSpaceCannonOffense(this, defender);
            int DpdsHits = defender.DoSpaceCannonOffense(this, attacker);
            attacker.AssignHits(this, DpdsHits, defender, Theater.Space);
            defender.AssignHits(this, ApdsHits, attacker, Theater.Space);

            foreach (Player player in others)
            {
                int otherHits = player.DoSpaceCannonOffense(this, attacker);
                attacker.AssignHits(this, otherHits, player, Theater.Space);
            }
        }

        public void AntiFighterBarrage()
        {
            //todo: find all commanders and promissory notes referencing anti-fighter barrage

            int AafbHits = attacker.DoAntiFighterBarrage(this, defender);
            int DafbHits = defender.DoAntiFighterBarrage(this, attacker);

            attacker.AssignHits(this, DafbHits, defender, Theater.Space, HitType.AFB);
            defender.AssignHits(this, AafbHits, attacker, Theater.Space, HitType.AFB);
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
            while (winner == Winner.None)
            {
                SimulateCombatRound(Theater.Ground);
                EvaluateWinner(Theater.Ground);
            }
            return winner;
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
            attacker.AssignHits(this, spaceCannonHits, defender, Theater.Ground);
        }

        private void Bombardment()
        {
            int bombardHits = attacker.DoBombardment(this, defender);
            //todo: implement X89
            defender.AssignHits(this, bombardHits, attacker, Theater.Ground);
        }

        public void SimulateCombatRound(Theater theater)
        {
            attacker.DoStartOfCombatRound(theater);
            defender.DoStartOfCombatRound(theater);

            int attackerHits = attacker.DoCombatRolls(this, defender, theater);
            int defenderHits = defender.DoCombatRolls(this, attacker, theater);

            attacker.AssignHits(this, defenderHits, defender, theater);
            defender.AssignHits(this, attackerHits, attacker, theater);
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
