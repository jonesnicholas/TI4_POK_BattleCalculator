using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TI4BattleSim.Units
{
    public class Flagship : Unit
    {
        Faction faction;
        public Flagship(TechModel techs, Faction fct = Faction.None)
        {
            faction = fct;
            if (faction == Faction.None)
            {
                throw new Exception("Generic faction cannot use flagships");
            }
            type = UnitType.Flagship;
            theater = Theater.Space;
            upgraded = techs != null && techs.upgrades.Contains(type);
            capacity = GetCapacity(faction, upgraded);

            int dice = (faction == Faction.Ghosts || faction == Faction.Winnu) ? 1 : 2;
            int toHit = GetHitDie(faction, upgraded);

            spaceCombat = new CombatModule(dice, toHit, sustain: true);
            antiFighter = GetAntiFighter(faction, upgraded);
            spaceCannon = GetSpaceCannon(faction);
            bombard = GetBombardment(faction);

            //todo: LOTS faction specific
            if (faction == Faction.Barony)
                bypassPlanetaryShield = true;

            if (faction == Faction.Winnu)
            {
                spaceCombat.doRoll = (battle, owner, target, num, toHit, dMod, hMod) =>
                {
                    int hits = 0;
                    int enemyNFShips = target.units.Count(unit => unit.ParticipatesInCombat(Theater.Space) && unit.type != UnitType.Fighter);
                    for (int i = 0; i < enemyNFShips; i++)
                    {
                        if (battle.random.Next(1, 11) + hMod >= toHit)
                            hits++;
                    }
                    return hits;
                };
            }

            if (faction == Faction.Jolnar)
            {
                spaceCombat.doRoll = (battle, owner, target, num, toHit, dMod, hMod) =>
                {
                    int hits = 0;
                    for (int i = 0; i < num + dMod; i++)
                    {
                        int roll = battle.random.Next(1, 11);
                        if (roll + hMod >= toHit)
                            hits++;
                        if (roll >= 9)
                        {
                            hits += 2;
                        }
                    }
                    return hits;
                };
            }


            //      Both Empyrian and Hacan need to add 'limit' option
            // TODO: Hacan
            // TODO: Empyrian
            // TODO: L1
            // TODO: Mahact
            // TODO: Mentak
            // TODO: Naalu
            // TODO: NRA
            // TODO: Nekro
            // TODO: Sardakk
        }

        public override void DestroyUnit(Battle battle, Player owner)
        {
            base.DestroyUnit(battle, owner);
            if (faction == Faction.Yin)
            {
                //Kaboom!
                foreach (Unit unit in battle.attacker.units)
                {
                    unit.DestroyUnit(battle, battle.attacker);
                }

                foreach (Unit unit in battle.defender.units)
                {
                    unit.DestroyUnit(battle, battle.attacker);
                }
            }
        }

        private static int GetHitDie(Faction faction, bool upgraded = false)
        {
            switch (faction)
            {
                case Faction.Barony:
                case Faction.Saar:
                case Faction.Muaat:
                case Faction.Empyrean:
                case Faction.Sol:
                case Faction.Ghosts:
                case Faction.L1:
                case Faction.Mahact:
                case Faction.Cabal:
                case Faction.Yssaril:
                    return 5;
                case Faction.Sardakk:
                case Faction.Jolnar:
                    return 6;
                case Faction.Arborec:
                case Faction.Argent:
                case Faction.Hacan:
                case Faction.Mentak:
                case Faction.Titans:
                case Faction.Winnu:
                case Faction.Xxcha:
                    return 7;
                case Faction.Naalu:
                case Faction.NRA:
                case Faction.Yin:
                    return 9;
                case Faction.Nomad:
                    return upgraded ? 5 : 7;
                case Faction.Nekro:
                    return upgraded ? 5 : 9;
                default:
                    throw new Exception($"faction {faction} flagship missing hit die");
            }
        }

        private static int GetCapacity(Faction faction, bool upgraded = false)
        {
            switch (faction)
            {
                case Faction.Argent:
                case Faction.Barony:
                case Faction.Saar:
                case Faction.Muaat:
                case Faction.Empyrean:
                case Faction.Ghosts:
                case Faction.Mahact:
                case Faction.Mentak:
                case Faction.Sardakk:
                case Faction.Titans:
                case Faction.Jolnar:
                case Faction.Cabal:
                case Faction.Winnu:
                case Faction.Xxcha:
                case Faction.Yin:
                case Faction.Yssaril:
                    return 3;
                case Faction.NRA:
                    return 4;
                case Faction.Arborec:
                case Faction.L1:
                    return 5;
                case Faction.Naalu:
                    return 6;
                case Faction.Sol:
                    return 12;
                case Faction.Nomad:
                case Faction.Nekro:
                    return upgraded ? 6 : 3;
                default:
                    throw new Exception($"Faction {faction} flagship missing capacity");
            }
        }

        private static CombatModule GetSpaceCannon(Faction faction)
        {
            switch (faction)
            {
                case Faction.Xxcha:
                    return new CombatModule(3, 5);
                case Faction.Arborec:
                case Faction.Saar:
                case Faction.Barony:
                case Faction.Muaat:
                case Faction.Hacan:
                case Faction.Sol:
                case Faction.Ghosts:
                case Faction.L1:
                case Faction.Mentak:
                case Faction.Naalu:
                case Faction.Nekro:
                case Faction.Sardakk:
                case Faction.Jolnar:
                case Faction.Winnu:
                case Faction.Yin:
                case Faction.Yssaril:
                case Faction.Argent:
                case Faction.Empyrean:
                case Faction.Mahact:
                case Faction.NRA:
                case Faction.Nomad:
                case Faction.Titans:
                case Faction.Cabal:
                    return CombatModule.NullModule();
                default:
                    throw new Exception($"Faction {faction} flagship has undefined SpaceCannon");
            }
        }

        private static CombatModule GetAntiFighter(Faction faction, bool upgraded = false)
        {
            switch (faction)
            {
                case Faction.Saar:
                    return new CombatModule(4, 6);
                case Faction.Nomad:
                    return new CombatModule(3, upgraded ? 5 : 8);
                case Faction.Nekro:
                    return upgraded ?
                        new CombatModule(3, upgraded ? 5 : 8) :
                        CombatModule.NullModule();
                case Faction.Arborec:
                case Faction.Argent:
                case Faction.Barony:
                case Faction.Muaat:
                case Faction.Hacan:
                case Faction.Empyrean:
                case Faction.Sol:
                case Faction.Ghosts:
                case Faction.L1:
                case Faction.Mahact:
                case Faction.Mentak:
                case Faction.Naalu:
                case Faction.NRA:
                case Faction.Sardakk:
                case Faction.Titans:
                case Faction.Jolnar:
                case Faction.Cabal:
                case Faction.Winnu:
                case Faction.Xxcha:
                case Faction.Yin:
                case Faction.Yssaril:
                    return CombatModule.NullModule();
                default:
                    throw new Exception($"Faction {faction} flagship has undefined AFB");
            }
        }

        private static CombatModule GetBombardment(Faction faction)
        {
            switch (faction)
            {
                case Faction.Barony:
                    return new CombatModule(3, 5);
                case Faction.Cabal:
                    return new CombatModule(1, 5);
                case Faction.Arborec:
                case Faction.Saar:
                case Faction.Muaat:
                case Faction.Hacan:
                case Faction.Sol:
                case Faction.Ghosts:
                case Faction.L1:
                case Faction.Mentak:
                case Faction.Naalu:
                case Faction.Nekro:
                case Faction.Sardakk:
                case Faction.Jolnar:
                case Faction.Winnu:
                case Faction.Xxcha:
                case Faction.Yin:
                case Faction.Yssaril:
                case Faction.Argent:
                case Faction.Empyrean:
                case Faction.Mahact:
                case Faction.NRA:
                case Faction.Nomad:
                case Faction.Titans:
                    return CombatModule.NullModule();
                default:
                    throw new Exception($"Faction {faction} flagship has undefined Bombard");
            }
        }

    }
}
