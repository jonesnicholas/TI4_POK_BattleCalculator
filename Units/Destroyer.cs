using System;
using System.Collections.Generic;
using System.Text;

namespace TI4BattleSim.Units
{
    public class Destroyer : Unit
    {
        public Destroyer(TechModel techs, Faction faction = Faction.None)
        {
            type = UnitType.Destroyer;
            theater = Theater.Space;
            upgraded = techs != null && techs.upgrades.Contains(type);
            antiFighter = new CombatModule(
                upgraded ? 3 : 2,
                upgraded ? 6 : 9);
            spaceCombat = new CombatModule(1, upgraded ? 8 : 9);

            if (faction == Faction.Argent)
            {
                spaceCombat = new CombatModule(1, upgraded ? 7 : 8);
                capacity = 1;
                if (upgraded)
                {
                    antiFighter.doRoll = (battle, owner, target, num, toHit) =>
                    {
                        int hits = 0;
                        for (int i = 0; i < num; i++)
                        {
                            if (battle.random.Next(1, 11) >= toHit)
                                hits++;
                        //todo: destroy space-based infantry on 9/10
                    }
                        return hits;
                    };
                }
            }
        }
    }
}
