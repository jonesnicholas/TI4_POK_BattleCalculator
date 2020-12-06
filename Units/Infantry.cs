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
            if (upgraded)
            {
                groundCombat = new CombatModule(1, 7);
            }
            else
            {
                groundCombat = new CombatModule(1, 8);
            }



            //todo: faction specific
        }
    }
}
