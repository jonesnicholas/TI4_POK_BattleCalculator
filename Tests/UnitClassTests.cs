using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TI4BattleSim.Units;

namespace TI4BattleSim
{
    [TestClass]
    public class UnitClassTests
    {
        // todo: create tests for all types
        [TestMethod]
        public void CreateGenericUnitList_Verify()
        {
            Dictionary<UnitType, int> counts = new Dictionary<UnitType, int>();

            List<Unit> menu = Unit.CreateGenericUnitList(counts);

            // will test that types get created correctly elwhere, right here primarily checking counts.
            foreach (UnitType type in counts.Keys)
            {
                Assert.AreEqual(counts[type], menu.Count(u => u.type == type));
            }

            //verify that all units are distinct objects
            for (int i = 0; i < menu.Count; i ++)
            {
                for (int j = i+1; j < menu.Count; j ++)
                {
                    Assert.AreNotSame(menu[i], menu[j]);
                }
            }
        }

        [TestMethod]
        public void CreateUnitList_Verify_Counts()
        {
            List<UnitType> types = Unit.GetAllTypes();
            List<Unit> unitMenu = new List<Unit>();
            foreach (UnitType type in types)
            {
                for (int i = 0; i < (int)type+1; i++)
                {
                    unitMenu.Add(Unit.CreateUnit(type));
                }
            }

            List<Unit> unitList = Unit.CreateUnitList(unitMenu);

            foreach (UnitType type in types)
            {
                Assert.AreEqual((int)type+1, unitList.Count(unit => unit.type == type));
            }

            //todo: add test methods to verify that upgraded ships are being made correctly, and that factions are adequately being applied
        }

        [TestMethod]
        public void SustainDamage_Base_Verify()
        {
            // Verifies that all possible units act properly when Sustain damage is called
            List<bool> upgraded = new List<bool>() { true, false };
            List<Faction> facts = Player.GetAllFactions();
            List<Damage> damg = new List<Damage>() { Damage.None, Damage.RecentlyDamaged, Damage.Damaged };
            List<UnitType> types = Unit.GetAllTypes();
            foreach (bool upg in upgraded)
            {
                TechModel model = new TechModel();
                if (upg)
                {
                    model.upgrades = Unit.GetAllTypes();
                }

                foreach (Faction faction in facts)
                {
                    foreach(UnitType type in types)
                    {
                        Unit unit = Unit.CreateUnit(type, model, faction);
                        Assert.AreEqual(Damage.None, unit.damage);
                        unit.SustainDamage();
                        Assert.AreEqual(Damage.RecentlyDamaged, unit.damage);
                        Assert.ThrowsException<Exception>(unit.SustainDamage);
                        unit.damage = Damage.Damaged;
                        Assert.ThrowsException<Exception>(unit.SustainDamage);
                    }
                }
            }
        }

        public static List<Unit> OneOfEverything()
        {
            List<Unit> oneOfAll = new List<Unit>();
            foreach (UnitType type in Unit.GetAllTypes())
            {
                oneOfAll.Add(Unit.CreateUnit(type));
            }
            return oneOfAll;
        }
    }
}
