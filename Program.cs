using System;
using System.Collections.Generic;

namespace TI4BattleSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ArgentFlightSim();

            return;
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Mech, 2);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            techModel.upgrades.Add(UnitType.Destroyer);
            List<Faction> acmds = new List<Faction>() { Faction.Argent };
            Player attackerModel = new Player(attackerUnits, Faction.Argent, techModel, acmds);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Infantry, 3);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            Arena.runCrucible(100000, attackerModel, defenderModel, theater: Theater.Ground, random: new Random(0));
        }

        static void ArgentFlightSim()
        {
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

            Arena.runCrucible(100000, attackerModel, defenderModel, theater: Theater.Space, random: new Random(0));
        }
    }
}
