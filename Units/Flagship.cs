using System;
using System.Collections.Generic;
using System.Text;

namespace TI4BattleSim.Units
{
    public class Flagship : Unit
    {
        public Flagship(TechModel techs, Faction faction = Faction.None)
        {
            type = UnitType.Flagship;
            theater = Theater.Space;
            upgraded = techs != null && techs.upgrades.Contains(type);


            //todo: LOTS faction specific
        }
    }
}
