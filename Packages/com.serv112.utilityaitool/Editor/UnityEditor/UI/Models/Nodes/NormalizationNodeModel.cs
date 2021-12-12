using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    [Serializable]
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "Math/Normalization")]
    public class NormalizationNodeModel : NormalizedFunctionNodeModel
    {

        public NormalizationNodeModel()
        {
            m_ParameterNames = new string[]
                {
                    "Input","Min", "Max"
                };

            InputType = TypeHandle.Float;
            NoEmbeddedConstant = false;
        }


        public override PortCapacity GetPortCapacity(IPortModel portModel)
        {
            PortCapacity cap = PortCapacity.Single;
            return cap;
        }

        public override float Evaluate()
        {
            return MathUtils.Normalization01WithClamp01(GetParameterValue(0), GetParameterValue(1), GetParameterValue(2));
        }
    }
}
