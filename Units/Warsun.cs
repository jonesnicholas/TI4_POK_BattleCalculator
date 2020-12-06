using System;
using System.Collections.Generic;
using System.Text;

namespace TI4BattleSim.Units
{
    public class Warsun : Unit
    {
        public Warsun(TechModel techs, Faction faction = Faction.None)
        {
            type = UnitType.Warsun;
            theater = Theater.Space;
            upgraded = techs != null && techs.upgrades.Contains(type);
            capacity = 1;
            bombard = new CombatModule(3, 3);
            spaceCombat = new CombatModule(3, 3, sustain: true);
        }
    }
}
