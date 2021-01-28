using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TI4BattleSim
{
    [TestClass]
    public class OptionsModelTests
    {
        [TestMethod]
        public void RiskDirectHitScenario()
        {
            //verifies risk direct hit works correctly
            List<double> sims = Scenarios.RiskDirectHitsSim();
            List<double> predicted = new List<double>() { 0.5821, 0.3702, 0.0477 };
            SimulationTests.AssertWithinTolerances(predicted, sims);
        }

        public static void VerifyOptionsMatch(OptionsModel a, OptionsModel b)
        {
            Assert.AreNotSame(a, b);
            Assert.AreEqual(a.riskDirectHit, b.riskDirectHit);

        }

        public static void VerifyOptionsDefault(OptionsModel options)
        {
            Assert.AreEqual(false, options.riskDirectHit);
        }
    }
}
