using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TI4BattleSim
{
    [TestClass]
    public class TechModelTests
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        public static void VerifyTechsMatch(TechModel a, TechModel b)
        {
            Assert.AreNotSame(a, b);
            Assert.AreNotSame(a.upgrades, b.upgrades);
            Assert.AreEqual(a.upgrades.Count, b.upgrades.Count);
            Assert.IsTrue(a.upgrades.All(b.upgrades.Contains));
            Assert.IsTrue(b.upgrades.All(a.upgrades.Contains));
            Assert.IsTrue(a.techs.All(b.techs.Contains));
            Assert.IsTrue(b.techs.All(a.techs.Contains));
        }

        public static void VerifyTechsEmpty(TechModel tech)
        {
            Assert.AreEqual(0, tech.upgrades.Count);
        }
    }
}
