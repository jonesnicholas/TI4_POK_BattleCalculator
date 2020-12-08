using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TI4BattleSim
{
    public enum Tech
    {
        Daaxive, X89O, Antimass, Graviton, L4Disruptors, ImpulseCore, PlasmaScoring, MagenDefenseGrid, MagenOmega, Duranium,
        AssaultCannon, NES, DimensionalSplicer, Valkyrie, Supercharge,
    }
    // ToDo: Implement the following
    //      X89 Omega
    //      Graviton
    //      Impusle Core
    //      Magen Defense Grid (Not Omega)
    //      Magen Defense Grid Omega
    //      Duranium Armor
    //      AssaultCannon
    //      Dimensional Splicer
    //      Supercharge

    public class TechModel
    {
        public List<UnitType> upgrades;
        public List<Tech> techs;

        public TechModel()
        {
            upgrades = new List<UnitType>();
            techs = new List<Tech>();
        }

        public static TechModel CopyTechModel(TechModel model)
        {
            TechModel copy = new TechModel();
            copy.upgrades = new List<UnitType>(model.upgrades);
            copy.techs = new List<Tech>(model.techs);
            return copy;
        }

        public bool HasTech(Tech tech)
        {
            return techs.Contains(tech);
        }
    }
}
