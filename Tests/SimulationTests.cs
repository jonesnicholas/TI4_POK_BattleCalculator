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
        public void SpaceCannonOffenseScenario()
        {
            // verifies that space cannon offense occurs as expected
            List<double> sims = Scenarios.SpaceCannonOffenseSim();
            List<double> predicted = new List<double>() { 0.347, 0.6059, 0.0471 };
            AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void SpaceCannonOffenseThirdPartyScenario()
        {
            // verifies that space cannon offense occurs as expected with "third parties"
            List<double> sims = Scenarios.SpaceCannonOffenseThirdPartySim();
            List<double> predicted = new List<double>() { 0.347, 0.6059, 0.0471 };
            AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void ArgentDestroyersScenario()
        {
            //verifies argent destroyers function as expected
            //todo: verify capacity concerns + mech as well
            List<double> sims = Scenarios.ArgentFlightDestroyerSim();
            List<double> predicted = new List<double>() { 0.6762, 0.2909, 0.0329 };
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

        [TestMethod]
        public void BombardScenario()
        {
            //verifies that units bombard as expected
            List<double> sims = Scenarios.BombardSim();
            List<double> predicted = new List<double>() { 0.5088, 0.4912, 0.00 };
            AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void MixedBombardScenario()
        {
            //verifies that bombard occurs as expected when mechs are defending alongside infantry
            List<double> sims = Scenarios.BombardSim();
            List<double> predicted = new List<double>() { 0.5144, 0.4856, 0.00 };
            AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void SpaceCannonDefenseScenario()
        {
            // verifies space cannon defense occurs as expected
            List<double> sims = Scenarios.SpaceCannonDefenseSim();
            List<double> predicted = new List<double>() { 0.638, 0.362, 0.00 };
            AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void MixedSpaceCannonDefenseScenario()
        {
            // verifies space cannon defense occurs as expected when mechs descending
            List<double> sims = Scenarios.MixedSpaceCannonDefenseSim();
            List<double> predicted = new List<double>() { 0.5204, 0.4763, 0.00 };
            AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void PlanetaryShieldPDSScenario()
        {
            // verifies pds blocks generic bombardment
            List<double> sims = Scenarios.PlanetaryShieldPDSSim();
            List<double> predicted = new List<double>() { 0.638, 0.362, 0.00 };
            AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void WarsunBreakPlanetaryShieldScenario()
        {
            // verifies war suns "break" planetary shield
            List<double> sims = Scenarios.WarSunBreaksPlanetaryShieldSim();
            List<double> predicted = new List<double>() { 0.4184, 0.5816, 0.00 };
            AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void GenericMechScenario()
        {
            //verifies that mechs perform as expected
            List<double> sims = Scenarios.GenericMechSim();
            List<double> predicted = new List<double>() { 0.407, 0.593, 0.00 };
            AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void MixedGenericGroundCombatScenario()
        {
            //verifies that mechs perform as expected
            List<double> sims = Scenarios.MixedGenericGroundCombatSim();
            List<double> predicted = new List<double>() { 0.4668, 0.5332, 0.00 };
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
