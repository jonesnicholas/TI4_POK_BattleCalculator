using System;
using System.Collections.Generic;
using System.Text;

namespace TI4BattleSim.Units
{
    public class Cruiser : Unit
    {
        public Cruiser(TechModel techs, Faction faction = Faction.None)
        {
            type = UnitType.Cruiser;
            theater = Theater.Space;
            upgraded = techs != null && techs.upgrades.Contains(type);
            capacity = upgraded ? 1 : 0;
            spaceCombat = new CombatModule(1, upgraded ? 6 : 7);

            if (faction == Faction.Titans)
            {
                capacity++;
                spaceCombat.canSustain = upgraded;
            }
        }
    }
}
