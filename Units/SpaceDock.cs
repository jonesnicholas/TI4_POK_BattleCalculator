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
            theater = Theater.Ground;
            upgraded = techs != null && techs.upgrades.Contains(type);
            capacity = 3;

            if (faction == Faction.Saar)
            {
                capacity = upgraded ? 5 : 4;
            }

            if (faction == Faction.Cabal)
            {
                capacity = upgraded ? 12 : 6;
            }
        }
    }
}
