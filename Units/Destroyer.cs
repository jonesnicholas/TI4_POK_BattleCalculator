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
            if (upgraded)
            {
                spaceCombat = new CombatModule(1, 8);
                antiFighter = new CombatModule(3, 6);
            }
            else
            {
                spaceCombat = new CombatModule(1, 9);
                antiFighter = new CombatModule(2, 9);
            }

            //todo: faction specific
            if (faction == Faction.Argent && upgraded)
            {
                spaceCombat = new CombatModule(1, upgraded ? 7 : 8);
                capacity = 1;
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
