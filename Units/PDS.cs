using System;
using System.Collections.Generic;
using System.Text;

namespace TI4BattleSim.Units
{
    public class PDS : Unit
    {
        public PDS(TechModel techs, Faction faction = Faction.None)
        {
            type = UnitType.PDS;
            theater = Theater.Hybrid;
            upgraded = techs != null && techs.upgrades.Contains(type);
            hasPlanetaryShield = true;
            spaceCannon = new CombatModule(1, upgraded ? 5 : 6);

            if (faction == Faction.Titans)
            {
                groundCombat = new CombatModule(1, upgraded ? 6 : 7, sustain: true);
            }

            // todo: make sure Nekro can properly yoink this, and other faction specific upgrades.
        }

        public override bool SafeSustain()
        {
            // currently, all pds types that can participate in ground combat can also safely sustain damage
            return (ParticipatesInCombat(Theater.Ground));
        }
    }
}
