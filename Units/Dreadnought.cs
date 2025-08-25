using System;
using System.Collections.Generic;
using System.Text;

namespace TI4BattleSim.Units
{
    public class Dreadnought : Unit
    {
        public Dreadnought(TechModel techs, Faction faction = Faction.None)
        {
            type = UnitType.Dreadnought;
            theater = Theater.Hybrid;
            upgraded = techs != null && techs.upgrades.Contains(type);
            capacity = 1;
            bombard = new CombatModule(1, 5);
            spaceCombat = new CombatModule(1, 5, sustain: true);

            if (faction == Faction.Sardakk)
            {
                bombard = new CombatModule(2, 4);
                if (upgraded)
                {
                    //todo: implement Exotrireme Suicide destroy option
                }
            }

            if (faction == Faction.L1)
            {
                capacity = 2;
                bombard = new CombatModule(1, upgraded ? 4 : 5);
                spaceCombat = new CombatModule(1, upgraded ? 4 : 5);
            }

            if (faction == Faction.Veldyr)
            {
                spaceCannon = new CombatModule(1, upgraded ? 5 : 8);
            }
        }
    }
}
