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
            if (faction == Faction.Arborec)
                hasPlanetaryShield = true;
            if (faction == Faction.NRA) 
            {
                groundCombat = new CombatModule(2, 6, sustain: true);
                spaceCombat = new CombatModule(2, 8, sustain: false);
            }

            // Todo:
            //      Barony Deploy limit
            //      Empyrian Sabotage(?)
            //      L1 cross-planet bombard
            //      Mentak Mech
            //      Naalu +2 option
            //      Nekro +2 option
            //      Nomad space sustain
            //      Sardakk sustain hit
            //      JolNar no fragile infantry
            //      xxcha space cannon
            //      yin indoctrination option
        }
    }
}
