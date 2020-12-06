using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TI4BattleSim
{
    public class TechModel
    {
        public List<UnitType> upgrades;

        public TechModel()
        {
            upgrades = new List<UnitType>();
        }

        public static TechModel CopyTechModel(TechModel model)
        {
            TechModel copy = new TechModel();
            copy.upgrades = new List<UnitType>(model.upgrades);
            return copy;
        }
    }
}
