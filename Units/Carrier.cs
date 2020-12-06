using System;
using System.Collections.Generic;
using System.Text;

namespace TI4BattleSim.Units
{
    public class Carrier : Unit
    {
        public Carrier(TechModel techs, Faction faction = Faction.None)
        {
            type = UnitType.Carrier;
            theater = Theater.Space;
            upgraded = techs != null && techs.upgrades.Contains(type);
            spaceCombat = new CombatModule(1, 9);
            capacity = upgraded ? 6 : 4;

            if (faction == Faction.Sol)
            {
                capacity = upgraded ? 8 : 6;
                spaceCombat.canSustain = upgraded;
            }
        }
    }
}
