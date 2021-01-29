using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TI4BattleSim
{
    [TestClass]
    public class UpgradeTests
    {
        [TestMethod]
        public void UpgradedDestroyersScenario()
        {
            //verifies AFB working as expected
            //verifies destroyers upgrade as expected
            List<double> sims = Scenarios.DestroyerSim();
            List<double> predicted = new List<double>() { 0.9127, 0.0791, 0.0082 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void UpgradedCruisersScenario()
        {
            //verifies cruisers upgrade as expected
            //todo: verify capacity concerns as well
            List<double> sims = Scenarios.CruiserSim();
            List<double> predicted = new List<double>() { 0.6316, 0.3319, 0.0365 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void UpgradedDreadnoughtScenario()
        {
            //verifies dreads sustain as expected
            //verifies 'safe' sustain works as expected
            List<double> sims = Scenarios.DreadnoughtSim();
            List<double> predicted = new List<double>() { 0.5821, 0.3702, 0.0477 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void UpgradeFighterScenario()
        {
            //verifies fighters upgrade as expected
            //todo: possibly use this to check for how fighter upgrades change fleet supply / capacity?

            List<double> sims = Scenarios.FighterSim();
            List<double> predicted = new List<double>() { 0.8236, 0.171, 0.0053 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void UpgradedInfantryScenario()
        {
            //verifies that infantry upgrade as expected
            List<double> sims = Scenarios.InfantrySim();
            List<double> predicted = new List<double>() { 0.6288, 0.3712, 0.00 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void UpgradedPDSSpaceCannonDefenseScenario()
        {
            //verifies that pds upgrade as expected
            List<double> sims = Scenarios.UpgradedSpaceCannonDefenseSim();
            List<double> predicted = new List<double>() { 0.3387, 0.6613, 0.00 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }
    }
}
