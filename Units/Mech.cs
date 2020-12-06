using System;
using System.Collections.Generic;
using System.Text;

namespace TI4BattleSim.Units
{
    public class Mech : Unit
    {
        public Mech(TechModel techs, Faction faction = Faction.None)
        {
            type = UnitType.Mech;
            theater = Theater.Ground;
            upgraded = false;
            groundCombat = new CombatModule(1, 6, sustain: true);




            //todo: LOTS faction specific
        }
    }
}
