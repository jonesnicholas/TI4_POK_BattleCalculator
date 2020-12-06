using System;
using System.Collections.Generic;
using System.Text;

namespace TI4BattleSim.Units
{
    public class SpaceDock : Unit
    {
        public SpaceDock(TechModel techs, Faction faction = Faction.None)
        {
            type = UnitType.SpaceDock;
            theater = Theater.Hybrid;
            upgraded = techs != null && techs.upgrades.Contains(type);
            capacity = 3;




            //todo: faction specific
        }
    }
}
