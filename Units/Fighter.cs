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
            if (upgraded)
            {
                spaceCombat = new CombatModule(1, 8);
            }
            else
            {
                spaceCombat = new CombatModule(1, 9);
            }



            //todo: faction specific
        }
    }
}
