using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TI4BattleSim
{
    [TestClass]
    public class SimulationTests
    {

        [TestMethod]
        public void UpgradedDestroyersScenario()
        {
            //verifies AFB working as expected
            //verifies destroyers upgrade as expected
            List<double> sims = Scenarios.DestroyerSim();
            List<double> predicted = new List<double>() { 0.9127, 0.0791, 0.0082 };
            AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void UpgradedDreadnoughtScenario()
        {
            //verifies dreads sustain as expected
            //verifies 'safe' sustain works as expected
            List<double> sims = Scenarios.DreadnoughtSim();
            List<double> predicted = new List<double>() { 0.5821, 0.3702, 0.0477 };
            AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void ArgentDestroyersScenario()
        {
            //verifies argent destroyers function as expected
            List<double> sims = Scenarios.ArgentFlightDestroyerSim();
            List<double> predicted = new List<double>() { 0.6762, 0.2909, 0.0329 };
            AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void UpgradedCruisersScenario()
        {
            //verifies cruisers upgrade as expected
            //todo: verify capacity concerns as well
            List<double> sims = Scenarios.CruiserSim();
            List<double> predicted = new List<double>() { 0.6316, 0.3319, 0.0365 };
            AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void TitansCruisersScenario()
        {
            //verifies titan cruisers upgrade as expected
            //todo: verify capacity concerns as well
            List<double> sims = Scenarios.TitansCruiserSim();
            List<double> predicted = new List<double>() { 0.6316, 0.3319, 0.0365 };
            AssertWithinTolerances(predicted, sims);
        }

        public static void AssertWithinTolerances(List<double> predicted, List<double> simulated, double tolerance = 0.01)
        {
            Assert.AreEqual(3, predicted.Count);
            Assert.AreEqual(3, simulated.Count);
            for (int i = 0; i < 3; i++)
            {
                AssertAreRoughlyEqual(predicted[i], simulated[i], tolerance);
            }
        }

        public static bool AssertAreRoughlyEqual(double a, double b, double tolerance)
        {
            return Math.Abs(a - b) <= tolerance;
        }
    }
}
