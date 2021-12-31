using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    [Serializable]
    public abstract class NormalizedFunctionNodeModel : MathFunction
    {

        public NormalizedFunctionNodeModel()
        {
            InputType = AIGraphCustomTypes.NormalizedFloat;
            OutputType = AIGraphCustomTypes.NormalizedFloat;
        }

        public override PortCapacity GetPortCapacity(IPortModel portModel)
        {
            PortCapacity cap = PortCapacity.Single;

            if (portModel.Direction == PortDirection.Output)
                cap = PortCapacity.Multi;
            return cap;
        }

    }
}
