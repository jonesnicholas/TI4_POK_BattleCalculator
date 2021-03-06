﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TI4BattleSim
{
    class Scenarios
    {
        public static List<double> ArborecMechSim(int simCount = 1000)
        {
            // 3 infantry + 5 dreads
            // vs
            // 1 infantry + 1 Arborec Mech
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Infantry, 3);
            attackerCounts.Add(UnitType.Dreadnought, 5);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            Player attackerModel = new Player(attackerUnits, Faction.None, techModel);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Infantry, 1);
            defenderCounts.Add(UnitType.Mech, 1);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.Arborec);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Ground, random: new Random(0));
        }

        public static List<double> ArborecMechVsWarSunSim(int simCount = 1000)
        {
            // 1 war sun + 1 dreadnought + 1 infantry
            // vs
            // 1 infantry + 1 Arborec mech
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Infantry, 1);
            attackerCounts.Add(UnitType.Warsun, 1);
            attackerCounts.Add(UnitType.Dreadnought, 1);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            Player attackerModel = new Player(attackerUnits, Faction.Arborec, techModel);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Infantry, 1);
            defenderCounts.Add(UnitType.Mech, 1);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Ground, random: new Random(0));
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

        public static List<double> BaseFactionMechSim(Faction faction = Faction.None, int simCount = 1000)
        {
            // 1 faction mech, no options (e.g. no +2 Nekro/Naalu)
            // vs
            // 1 generic mech
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Mech, 1);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            Player attackerModel = new Player(attackerUnits, faction);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Mech, 1);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Ground, random: new Random(0));
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

        public static List<double> FighterSim(int simCount = 1000)
        {
            // 10 upgraded fighters
            //      vs
            // 10 fighters
            // todo: capacity / fleet supply considerations
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Fighter, 10);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            techModel.upgrades.Add(UnitType.Fighter);
            Player attackerModel = new Player(attackerUnits, Faction.None, techModel);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Fighter, 10);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Space, random: new Random(0));
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

        public static List<double> MentakMechSim(int simCount = 1000)
        {
            // 1 Mentak Mech
            // vs
            // 2 generic Mechs
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Mech, 1);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            Player attackerModel = new Player(attackerUnits, Faction.Mentak, techModel);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Mech, 2);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Ground, random: new Random(0));
        }

        public static List<double> MixedBombardSim(int simCount = 1000)
        {
            // 1 dreadnaught + 1 infantry
            // vs
            // 1 infantry
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Warsun, 1);
            attackerCounts.Add(UnitType.Mech, 1);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            Player attackerModel = new Player(attackerUnits, Faction.None, techModel);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Infantry, 2);
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

        public static List<double> MixedSpaceCannonDefenseSim(int simCount = 1000)
        {
            // 1 mech + 1 infantry
            // vs
            // 1 mech + 2 pds
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Mech, 1);
            attackerCounts.Add(UnitType.Infantry, 1);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            Player attackerModel = new Player(attackerUnits, Faction.None, techModel);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Mech, 1);
            defenderCounts.Add(UnitType.PDS, 2);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Ground, random: new Random(0));
        }

        //TODO: Scenari for space cannon defence w/ "external" pds
        public static List<double> NRAMechGroundSim(int simCount = 1000)
        {
            // 1 NRA Mech
            // vs
            // 1 generic Mechs
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Mech, 1);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            Player attackerModel = new Player(attackerUnits, Faction.NRA, techModel);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Mech, 1);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Ground, random: new Random(0));
        }

        public static List<double> NRAMechSpaceSim(int simCount = 1000)
        {
            // 1 NRA Mech
            // vs
            // 1 cruiser
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Mech, 1);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            Player attackerModel = new Player(attackerUnits, Faction.NRA, techModel);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Cruiser, 1);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Space, random: new Random(0));
        }

        public static List<double> PlanetaryShieldPDSSim(int simCount = 1000)
        {
            // 2 infantry + 5 dreads
            // vs
            // 1 infantry + 1 pds
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Infantry, 2);
            attackerCounts.Add(UnitType.Dreadnought, 5);
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

        public static List<double> SpaceCannonOffenseSim(int simCount = 1000)
        {
            // 2 cruiser + 2 fighters + 1pds
            // vs
            // 2 cruiser + 2 fighters + 2pds
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Cruiser, 2);
            attackerCounts.Add(UnitType.Fighter, 2);
            attackerCounts.Add(UnitType.PDS, 1);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            Player attackerModel = new Player(attackerUnits, Faction.None, techModel);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Cruiser, 2);
            defenderCounts.Add(UnitType.Fighter, 2);
            defenderCounts.Add(UnitType.PDS, 2);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Space, random: new Random(0));
        }

        public static List<double> SpaceCannonOffenseThirdPartySim(int simCount = 1000)
        {
            // 2 dread + 2 fighters + 1pds
            // vs
            // 2 cruiser + 2 fighters + 1 pds + 1 third party PDS
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Cruiser, 2);
            attackerCounts.Add(UnitType.Fighter, 2);
            attackerCounts.Add(UnitType.PDS, 1);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            Player attackerModel = new Player(attackerUnits, Faction.None, techModel);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Cruiser, 2);
            defenderCounts.Add(UnitType.Fighter, 2);
            defenderCounts.Add(UnitType.PDS, 1);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            Dictionary<UnitType, int> thirdPartyCounts = new Dictionary<UnitType, int>();
            thirdPartyCounts.Add(UnitType.PDS, 1);
            List<Unit> thirdPartyUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player thirdPartyModel = new Player(defenderUnits, Faction.None);

            List<Player> othersModel = new List<Player>() { thirdPartyModel };

            return Arena.runCrucible(simCount, attackerModel, defenderModel, othersModel, theater: Theater.Space, random: new Random(0));
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

        public static List<double> UpgradedSpaceCannonDefenseSim(int simCount = 1000)
        {
            // 2 infantry 
            // vs
            // 1 infantry + 2 pds
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Infantry, 2);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            Player attackerModel = new Player(attackerUnits, Faction.None);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Infantry, 1);
            defenderCounts.Add(UnitType.PDS, 2);
            TechModel techModel = new TechModel();
            techModel.upgrades.Add(UnitType.PDS);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None, techModel);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Ground, random: new Random(0));
        }

        public static List<double> UpgradedSpaceCannonOffenseSim(int simCount = 1000)
        {
            // 2 cruiser + 2 fighters
            // vs
            // 1 cruiser + 1 fighter + 4 pds2
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Cruiser, 2);
            attackerCounts.Add(UnitType.Fighter, 2);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            Player attackerModel = new Player(attackerUnits, Faction.None);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Cruiser, 1);
            defenderCounts.Add(UnitType.Fighter, 1);
            defenderCounts.Add(UnitType.PDS, 4);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            TechModel techModel = new TechModel();
            techModel.upgrades.Add(UnitType.PDS);
            Player defenderModel = new Player(defenderUnits, Faction.None, techModel);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Space, random: new Random(0));
        }

        public static List<double> WarSunBreaksPlanetaryShieldSim(int simCount = 1000)
        {
            // 1 war sun + 1 dreadnought + 2 infantry
            // vs
            // 1 pds + 3 infantry
            Dictionary<UnitType, int> attackerCounts = new Dictionary<UnitType, int>();
            attackerCounts.Add(UnitType.Infantry, 1);
            attackerCounts.Add(UnitType.Warsun, 1);
            attackerCounts.Add(UnitType.Dreadnought, 1);
            List<Unit> attackerUnits = Unit.CreateGenericUnitList(attackerCounts);
            TechModel techModel = new TechModel();
            Player attackerModel = new Player(attackerUnits, Faction.None, techModel);

            Dictionary<UnitType, int> defenderCounts = new Dictionary<UnitType, int>();
            defenderCounts.Add(UnitType.Infantry, 3);
            defenderCounts.Add(UnitType.PDS, 1);
            List<Unit> defenderUnits = Unit.CreateGenericUnitList(defenderCounts);
            Player defenderModel = new Player(defenderUnits, Faction.None);

            return Arena.runCrucible(simCount, attackerModel, defenderModel, theater: Theater.Ground, random: new Random(0));
        }
    }
}
