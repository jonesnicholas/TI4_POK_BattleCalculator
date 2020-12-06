using System;
using System.Collections.Generic;
using System.Text;

namespace TI4BattleSim.Units
{
    public class Fighter : Unit
    {
        public Fighter(TechModel techs, Faction faction = Faction.None)
        {
            type = UnitType.Fighter;
            theater = Theater.Space;
            upgraded = techs != null && techs.upgrades.Contains(type);
            spaceCombat = new CombatModule(1, upgraded ? 8 : 9);

            if (faction == Faction.Naalu)
            {
                spaceCombat = new CombatModule(1, upgraded ? 7 : 8);
            }
        }
    }
}
