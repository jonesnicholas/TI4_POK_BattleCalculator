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
            if (upgraded)
            {
                spaceCombat = new CombatModule(1, 6);
                capacity = 1;
            }
            else
            {
                spaceCombat = new CombatModule(1, 7);
            }

            if (faction == Faction.Titans)
            {
                capacity++;
                spaceCombat.canSustain = upgraded;
            }
            //todo: faction specific
        }
    }
}
