using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TI4BattleSim
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void Constructor_Full_Verify()
        {
            List<Unit> menu = new List<Unit>();
            Faction fct = Faction.Argent;
            TechModel tech = new TechModel();
            List<Faction> cmds = new List<Faction>();
            bool act = true;

            Player player = new Player(menu, fct, tech, cmds, act);
            //assume unit creation failures check elsewhere, just verifying counts here
            Assert.AreNotSame(menu, player.units);
            foreach (UnitType unitType in Unit.GetAllTypes())
            {
                Assert.AreEqual(menu.Count(unit => unit.type == unitType), player.units.Count(unit => unit.type == unitType));
            }
            Assert.AreEqual(fct, player.faction);
            Assert.AreNotSame(tech, player.techs);
            TechModelTests.VerifyTechsMatch(tech, player.techs);
            Assert.AreNotSame(cmds, player.commanders);
            Assert.AreEqual(cmds.Count, player.commanders.Count);
            Assert.IsTrue(cmds.All(player.commanders.Contains));
            Assert.IsTrue(player.commanders.All(cmds.Contains));
            Assert.AreEqual(act, player.isActive);

            Player nullPlayer = new Player(menu, fct);
            Assert.IsNotNull(nullPlayer.techs);
            TechModelTests.VerifyTechsEmpty(nullPlayer.techs);
            Assert.IsNotNull(nullPlayer.commanders);
            Assert.AreEqual(0, nullPlayer.commanders.Count);
            Assert.AreEqual(false, nullPlayer.isActive);
        }

        [TestMethod]
        public void Constructor_Empty_Verify()
        {
            Player player = new Player();
            Assert.IsNotNull(player.units);
            Assert.AreEqual(0, player.units.Count);
            Assert.AreEqual(Faction.None, player.faction);
            Assert.IsNotNull(player.techs);
            TechModelTests.VerifyTechsEmpty(player.techs);
            Assert.IsNotNull(player.commanders);
            Assert.AreEqual(0, player.commanders.Count);
            Assert.IsFalse(player.isActive);
        }

        [TestMethod]
        public void Copy_Verify()
        {
            List<Unit> menu = new List<Unit>();
            Faction fct = Faction.Argent;
            TechModel tech = new TechModel();
            List<Faction> cmds = new List<Faction>();
            bool act = true;

            Player player = new Player(menu, fct, tech, cmds, act);
            Player copy = Player.CopyPlayer(player);
            CheckMatchingPlayers(player, copy);
        }

        public void CheckMatchingPlayers(Player a, Player b)
        {
            Assert.AreNotSame(a, b);
            //assume unit creation failures check elsewhere, just verifying counts here
            Assert.AreNotSame(a.units, b.units);
            foreach (UnitType unitType in Unit.GetAllTypes())
            {
                Assert.AreEqual(a.units.Count(unit => unit.type == unitType), b.units.Count(unit => unit.type == unitType));
            }
            Assert.AreEqual(a.faction, b.faction);
            Assert.AreNotSame(a.techs, b.techs);
            TechModelTests.VerifyTechsMatch(a.techs, b.techs);
            Assert.AreNotSame(a.commanders, b.commanders);
            Assert.AreEqual(a.commanders.Count, b.commanders.Count);
            Assert.IsTrue(a.commanders.All(b.commanders.Contains));
            Assert.IsTrue(b.commanders.All(a.commanders.Contains));
            Assert.AreEqual(a.isActive, b.isActive);
        }
    }
}
