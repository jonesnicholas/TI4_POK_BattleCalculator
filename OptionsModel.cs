using System;
using System.Collections.Generic;
using System.Text;

namespace TI4BattleSim
{
    public class OptionsModel
    {
        public bool riskDirectHit = false;

        internal static OptionsModel CopyOptionsModel(OptionsModel model)
        {
            OptionsModel output = new OptionsModel();
            output.riskDirectHit = model.riskDirectHit;
            return output;
        }
    }
}
