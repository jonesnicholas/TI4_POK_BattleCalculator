using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TI4BattleSim
{
    [TestClass]
    public class GenericTechTests
    {
        [TestMethod]
        public void AntimassDeflectors_SpaceCannonOffense()
        {
            // verifies that Antimass modifies Space Cannon Offense correctly
            List<double> sims = Scenarios.Antimass_SpaceCannonOffenseSim();
            List<double> predicted = new List<double>() { 0.6197, 0.3188, 0.0615 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void AntimassDeflectors_SpaceCannonDefense()
        {
            // verifies that Antimass modifies Space Cannon Defense correctly
            List<double> sims = Scenarios.Antimass_SpaceCannonDefenseSim();
            List<double> predicted = new List<double>() { 0.5121, 0.4879, 0.00 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void PlasmaScoring_Bombard()
        {
            //verifies that plasma scoring triggers on bombard, and assigns to "best" bombardier
            List<double> sims = Scenarios.PlasmaScoring_BombardSim();
            List<double> predicted = new List<double>() { 0.44, 0.56, 0.00 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void PlasmaScoring_SpaceCannonOffense()
        {
            //verifies that plasma scoring triggers on bombard, and assigns to "best" bombardier
            List<double> sims = Scenarios.PlasmaScoring_SpaceCannonOffenseSim();
            List<double> predicted = new List<double>() { 0.44, 0.4752, 0.0833 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void PlasmaScoring_SpaceCannonDefense()
        {
            //verifies that plasma scoring triggers on bombard, and assigns to "best" bombardier
            List<double> sims = Scenarios.PlasmaScoring_SpaceCannonDefenseSim();
            List<double> predicted = new List<double>() { 0.4245, 0.5755, 0.00 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }
    }
}
