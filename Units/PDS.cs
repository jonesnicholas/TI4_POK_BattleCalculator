using System;
using System.Collections.Generic;
using System.Text;

namespace TI4BattleSim.Units
{
    public class PDS : Unit
    {
        public PDS(TechModel techs, Faction faction = Faction.None)
        {
            type = UnitType.PDS;
            theater = Theater.Hybrid;
            upgraded = techs != null && techs.upgrades.Contains(type);
            if (upgraded)
            {
                spaceCannon = new CombatModule(1, 5);
            }
            else
            {
                spaceCannon = new CombatModule(1, 6);
            }




            //todo: faction specific
        }
    }
}
