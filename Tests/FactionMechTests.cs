using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TI4BattleSim
{
    [TestClass]
    public class FactionMechTests
    {
        [TestMethod]
        public void ArborecMechScenario()
        {
            //verifies that Arborec Mech correctly applies Planetary Shield
            List<double> sims = Scenarios.ArborecMechSim();
            List<double> predicted = new List<double>() { 0.4052, 0.5948, 0.00 };
            SimulationTests.AssertWithinTolerances(predicted, sims);

            //verifies that planetary shield still gets cracked by war suns
            sims = Scenarios.ArborecMechVsWarSunSim();
            predicted = new List<double>() { 0.7906, 0.2094, 0.00 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }

        //TODO: Barony Mech Deploying
        //TODO: L1 Mech cross-bombarding

        [TestMethod]
        public void MentakMechScenario()
        {
            //verifies that Mentak Mechs correctly prevent other units from sustaining.
            List<double> sims = Scenarios.MentakMechSim();
            List<double> predicted = Scenarios.PreDamagedMechSim();
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }

        //TODO: Naalu +2 Option
        //TODO: Nekro +2 Option
        //TODO: Sardakk Valkyrie & +1
        //TODO: JolNar -1 and infantry buff
        //TODO: Xxcha Space Cannon (offense & defense)
        //TODO: Yin Indoctrinate options

        //TODO: Argent Flight mech capacity interactions
        //TODO: Empyrian Mech Canceling (?)

        [TestMethod]
        public void NRAMechScenario()
        {
            List<double> sims = Scenarios.NRAMechGroundSim();
            List<double> predicted = new List<double>() { 0.7062, 0.2938, 0.00 };
            SimulationTests.AssertWithinTolerances(predicted, sims);

            sims = Scenarios.NRAMechSpaceSim();
            predicted = new List<double>() { 0.4334, 0.2781, 0.2884 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }

        //TODO: Nomad Mech in space
        //TODO: Titans aggresive sleeper option

        [TestMethod]
        public void VerifyMechBaseCombatStats()
        {
            // all mechs that don't have special combat capability should be equal to "generic" mechs
            List<double> predicted = new List<double>() { 0.4076, 0.5924, 0.00 };
            List<Faction> combatMechs = new List<Faction>() { Faction.Mentak, Faction.NRA, Faction.Sardakk, Faction.Jolnar };

            foreach (Faction faction in Enum.GetValues(typeof(Faction)))
            {
                if (combatMechs.Contains(faction))
                    continue;
                List<double> sims = Scenarios.BaseFactionMechSim(faction);
                SimulationTests.AssertWithinTolerances(predicted, sims, tolerance: 0.02);
            }
        }

    }
}
