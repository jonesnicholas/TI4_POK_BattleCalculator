using System;
using System.Collections.Generic;
using System.Text;

namespace TI4BattleSim.Units
{
    public class Infantry : Unit
    {
        public Infantry(TechModel techs, Faction faction = Faction.None)
        {
            type = UnitType.Infantry;
            theater = Theater.Ground;
            upgraded = techs != null && techs.upgrades.Contains(type);
            groundCombat = new CombatModule(1, upgraded ? 7 : 8);

            if (faction == Faction.Sol)
            {
                groundCombat = new CombatModule(1, upgraded ? 6 : 7);
            }
        }
    }
}
