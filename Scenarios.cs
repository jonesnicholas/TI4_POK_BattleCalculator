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

        public static List<double> ArgentFlightSim(int simCount = 1000)
        {
            // 3 upgraded argent destroyers + 3 fighters
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

    }
}
