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

            sims = Scenarios.Antimass_SpaceCannonOffenseSim(hasTech: false);
            predicted = new List<double>() { 0.5018, 0.4309, 0.0674 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void AntimassDeflectors_SpaceCannonDefense()
        {
            // verifies that Antimass modifies Space Cannon Defense correctly
            List<double> sims = Scenarios.Antimass_SpaceCannonDefenseSim();
            List<double> predicted = new List<double>() { 0.5121, 0.4879, 0.00 };
            SimulationTests.AssertWithinTolerances(predicted, sims);

            sims = Scenarios.Antimass_SpaceCannonDefenseSim(hasTech: false);
            predicted = new List<double>() { 0.4243, 0.5757, 0.00 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void PlasmaScoring_Bombard()
        {
            //verifies that plasma scoring triggers on bombard, and assigns to "best" bombardier
            List<double> sims = Scenarios.PlasmaScoring_BombardSim();
            List<double> predicted = new List<double>() { 0.44, 0.56, 0.00 };
            SimulationTests.AssertWithinTolerances(predicted, sims);

            sims = Scenarios.PlasmaScoring_BombardSim(hasTech: false);
            predicted = new List<double>() { 0.1691, 0.8309, 0.00 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void PlasmaScoring_SpaceCannonOffense()
        {
            //verifies that plasma scoring triggers on bombard, and assigns to "best" bombardier
            List<double> sims = Scenarios.PlasmaScoring_SpaceCannonOffenseSim();
            List<double> predicted = new List<double>() { 0.44, 0.4752, 0.0833 };
            SimulationTests.AssertWithinTolerances(predicted, sims);

            sims = Scenarios.PlasmaScoring_SpaceCannonOffenseSim(hasTech: false);
            predicted = new List<double>() { 0.607, 0.3095, 0.0835 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void PlasmaScoring_SpaceCannonDefense()
        {
            //verifies that plasma scoring triggers on bombard, and assigns to "best" bombardier
            List<double> sims = Scenarios.PlasmaScoring_SpaceCannonDefenseSim();
            List<double> predicted = new List<double>() { 0.4245, 0.5755, 0.00 };
            SimulationTests.AssertWithinTolerances(predicted, sims);

            sims = Scenarios.PlasmaScoring_SpaceCannonDefenseSim(hasTech: false);
            predicted = new List<double>() { 0.641, 0.359, 0.00 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }

        [TestMethod]
        public void Magen_GroundCombat()
        {
            //verifies that magen defense grid (classic) operates correctly.
            List<double> sims = Scenarios.Magen_GroundCombat();
            List<double> predicted = new List<double>() { 0.5524, 0.4476, 0.00 };
            SimulationTests.AssertWithinTolerances(predicted, sims);

            sims = Scenarios.Magen_GroundCombat(hasTech: false);
            predicted = new List<double>() { 0.8297, 0.1703, 0.00 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }

        /*[TestMethod]
        public void MagenOmega_GroundCombat()
        {
            //verifies that magen omega operates correctly
            List<double> sims = Scenarios.MagenOmega_GroundCombat();
            List<double> predicted = new List<double>() { 0.5524, 0.4476, 0.00 };
            SimulationTests.AssertWithinTolerances(predicted, sims);

            sims = Scenarios.MagenOmega_GroundCombat(hasTech: false);
            predicted = new List<double>() { 0.5524, 0.4476, 0.00 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }*/
    }
}
