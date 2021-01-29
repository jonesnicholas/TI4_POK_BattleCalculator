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
        public void DacxiveAnimators_Default()
        {
            // verifies that Dacxive gives +1 infantry to the winner of a ground combat
            //TODO: Need to get countings of remaining units
        }

        [TestMethod]
        public void DacxiveAnimators_NoCombat()
        {
            // verifies that Dacxive doesn't give +1 infantry if there was no combat
            //TODO: Need to get countings of remaining units
        }

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
    }
}
