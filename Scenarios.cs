using System;
using System.Collections.Generic;
using System.Text;

namespace TI4BattleSim
{
    class Scenarios
    {
        public static List<double> DestroyerSim(int simCount = 1000)
        {
            // 3 upgraded destroyers + 3 fighters
            //      vs
            // 3 destroyers + 3 fighters
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Destroyer, 3);
            attackerCounts.Add(UnitType.Fighter, 3);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            techModel.upgrades.Add(UnitType.Destroyer);
            Player attackerModel = new Player(attackerUnits, Faction.None, techModel);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Destroyer, 3);
            defenderCounts.Add(UnitType.Fighter, 3);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Space, random: new Random(0));
        }

        public static List<double> DreadnoughtSim(int simCount = 1000)
        {
            // 3 upgraded destroyers + 3 fighters
            //      vs
            // 3 unupgraded destroyers + 3 fighters
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Dreadnought, 3);
            attackerCounts.Add(UnitType.Fighter, 3);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            techModel.upgrades.Add(UnitType.Dreadnought);
            Player attackerModel = new Player(attackerUnits, Faction.None, techModel);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Dreadnought, 3);
            defenderCounts.Add(UnitType.Fighter, 3);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Space, random: new Random(0));
        }

        public static List<double> ArgentFlightDestroyerSim(int simCount = 1000)
        {
            // 3 upgraded argent destroyers + 3 fighters w/ Argent Commander
            //      vs
            // 3 generic dreads + 3 fighters
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Destroyer, 3);
            attackerCounts.Add(UnitType.Fighter, 3);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            techModel.upgrades.Add(UnitType.Destroyer);
            List<Faction> acmds = new List<Faction>() { Faction.Argent };
            Player attackerModel = new Player(attackerUnits, Faction.Argent, techModel, acmds);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Dreadnought, 3);
            defenderCounts.Add(UnitType.Fighter, 3);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Space, random: new Random(0));
        }

        public static List<double> CruiserSim(int simCount = 1000)
        {
            // 3 upgraded cruisers + 3 fighters
            // vs
            // 3 cruisers + 3 fighters
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Cruiser, 3);
            attackerCounts.Add(UnitType.Fighter, 3);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            techModel.upgrades.Add(UnitType.Cruiser);
            Player attackerModel = new Player(attackerUnits, Faction.None, techModel);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Cruiser, 3);
            defenderCounts.Add(UnitType.Fighter, 3);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Space, random: new Random(0));
        }

        public static List<double> InfantrySim(int simCount = 1000)
        {
            // 3 upgraded infantry
            // vs
            // 3 infantry
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Infantry, 3);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            techModel.upgrades.Add(UnitType.Infantry);
            Player attackerModel = new Player(attackerUnits, Faction.None, techModel);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Infantry, 3);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Ground, random: new Random(0));
        }

        public static List<double> GenericMechSim(int simCount = 1000)
        {
            // 1 mech
            // vs
            // 1 mech
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Mech, 1);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            Player attackerModel = new Player(attackerUnits, Faction.None, techModel);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Mech, 1);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Ground, random: new Random(0));
        }

        public static List<double> MixedGenericGroundCombatSim(int simCount = 1000)
        {
            // 1 mech + 2 infantry
            // vs
            // 1 mech + 2 infantry
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Mech, 1);
            attackerCounts.Add(UnitType.Infantry, 2);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            Player attackerModel = new Player(attackerUnits, Faction.None, techModel);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Mech, 1);
            defenderCounts.Add(UnitType.Infantry, 2);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Ground, random: new Random(0));
        }

        public static List<double> TitansCruiserSim(int simCount = 1000)
        {
            // 3 upgraded titan cruisers + 3 fighters w/o Titans Agent
            // vs
            // 3 generic cruisers + 3 fighters
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Cruiser, 3);
            attackerCounts.Add(UnitType.Fighter, 3);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            techModel.upgrades.Add(UnitType.Cruiser);
            Player attackerModel = new Player(attackerUnits, Faction.Titans, techModel);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Cruiser, 3);
            defenderCounts.Add(UnitType.Fighter, 3);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Space, random: new Random(0));
        }

        public static List<double> RiskDirectHitsSim(int simCount = 1000)
        {
            // 3 upgraded dreads + 3 fighters, risking direct hit
            //      vs
            // 3 unupgraded dreads + 3 fighters
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Dreadnought, 3);
            attackerCounts.Add(UnitType.Fighter, 3);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            OptionsModel options = new OptionsModel();
            options.riskDirectHit = true;
            Player attackerModel = new Player(attackerUnits, Faction.None, optionModel: options);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Dreadnought, 3);
            defenderCounts.Add(UnitType.Fighter, 3);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Space, random: new Random(0));
        }

        public static List<double> Churn(int simCount = 1000)
        {
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Dreadnought, 2);
            attackerCounts.Add(UnitType.Fighter, 2);
            attackerCounts.Add(UnitType.Carrier, 1);
            attackerCounts.Add(UnitType.Cruiser, 1);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            Player attackerModel = new Player(attackerUnits, Faction.Titans);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Destroyer, 3);
            defenderCounts.Add(UnitType.Fighter, 3);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            TechModel techModel = new TechModel();
            techModel.upgrades.Add(UnitType.Destroyer);
            List<Faction> dcmds = new List<Faction>() { Faction.Argent };
            Player defenderModel = new Player(defenderUnits, Faction.Argent, techModel, dcmds);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Space, random: new Random(0));
        }

        public static List<double> BombardSim(int simCount = 1000)
        {
            // 1 dreadnaught + 1 infantry
            // vs
            // 1 infantry
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Dreadnought, 1);
            attackerCounts.Add(UnitType.Warsun, 1);
            attackerCounts.Add(UnitType.Infantry, 1);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            Player attackerModel = new Player(attackerUnits, Faction.None, techModel);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Infantry, 4);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Ground, random: new Random(0));
        }

        public static List<double> SpaceCannonDefenseSim(int simCount = 1000)
        {
            // 2 infantry 
            // vs
            // 1 infantry + 1 pds
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Infantry, 2);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            Player attackerModel = new Player(attackerUnits, Faction.None, techModel);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Infantry, 1);
            defenderCounts.Add(UnitType.PDS, 1);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Ground, random: new Random(0));
        }
    }
}
