using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TI4BattleSim
{
    [TestClass]
    public class CombatModuleTests
    {
        [TestMethod]
        public void Constructor()
        {
            int combatVal = 7;
            int numDice = 2;
            CombatModule module = new CombatModule(numDice, combatVal);
            Assert.AreEqual(combatVal, module.ToHit);
            Assert.AreEqual(numDice, module.NumDice);
            Assert.IsFalse(module.canSustain);

            module = new CombatModule(numDice, combatVal, false);
            Assert.IsFalse(module.canSustain);
            module = new CombatModule(numDice, combatVal, true);
            Assert.IsTrue(module.canSustain);
        }

        [TestMethod]
        public void NoRoll()
        {
            int combatVal = 7;
            int numDice = 2;
            CombatModule module = new CombatModule(numDice, combatVal);
            module.doRoll = CombatModule.noRoll;
            Assert.AreEqual(CombatModule.noRoll, module.doRoll);

            Mock<Battle> battleMock = new Mock<Battle>();
            Mock<Random> randomMock = new Mock<Random>();
            randomMock.Setup(m => m.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(10);
            Mock<Player> ownerMock = new Mock<Player>();
            Mock<Player> targetMock = new Mock<Player>();

            int hits = module.doCombat(battleMock.Object, ownerMock.Object, targetMock.Object);
            Assert.AreEqual(0, hits);
            randomMock.Verify(mock => mock.Next(), Times.Never);
        }

        [TestMethod]
        public void DefaultRoll()
        {
            int combatVal = 7;
            int numDice = 100;
            CombatModule module = new CombatModule(numDice, combatVal);
            Assert.AreEqual(CombatModule.defaultRoll, module.doRoll);

            Random random = new Random();
            Queue<int> sequence = new Queue<int>();
            List<int> verification = new List<int>();
            for (int i = 0; i < numDice; i ++)
            {
                int roll = random.Next(1, 11);
                sequence.Enqueue(roll);
                verification.Add(roll);
            }

            Mock<Battle> battleMock = new Mock<Battle>();
            Mock<Random> randomMock = new Mock<Random>();
            randomMock.Setup(m => m.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(()=> sequence.Dequeue());
            battleMock.SetupAllProperties();
            battleMock.Object.random = randomMock.Object;
            Mock<Player> ownerMock = new Mock<Player>();
            Mock<Player> targetMock = new Mock<Player>();

            int hits = module.doCombat(battleMock.Object, ownerMock.Object, targetMock.Object);
            randomMock.Verify(mock => mock.Next(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(numDice));
            Assert.AreEqual(verification.Count(n => n >= combatVal), hits);
        }


    }
}
