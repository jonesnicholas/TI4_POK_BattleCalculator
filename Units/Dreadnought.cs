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


            //todo: faction specific
        }
    }
}
