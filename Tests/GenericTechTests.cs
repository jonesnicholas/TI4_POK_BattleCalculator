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
    }
}
