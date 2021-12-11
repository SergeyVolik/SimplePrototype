using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;

namespace SerV112.UtilityAIEditor
{
    [Serializable]
    public abstract class MathNode : NodeModel
    {
        public float GetValue(IPortModel port)
        {
            return port.GetValue();
        }

        public override PortCapacity GetPortCapacity(IPortModel portModel)
        {
            PortCapacity cap = PortCapacity.Single;
            return cap;
        }
        public abstract float Evaluate();

    }
}

