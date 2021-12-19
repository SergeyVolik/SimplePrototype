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
                    Input, Max, Min
                };

            InputType = TypeHandle.Float;
            NoEmbeddedConstant = false;
        }

        const string Input = "Input";
        const string Max = "Max";
        const string Min = "Min";
        public IPortModel InputPort => InputsById[Input];
        public IPortModel MaxPort => InputsById[Max];
        public IPortModel MinPort => InputsById[Min];

        public override PortCapacity GetPortCapacity(IPortModel portModel)
        {
            PortCapacity cap = PortCapacity.Single;

            if (portModel.Direction == PortDirection.Output)
                cap = PortCapacity.Multi;
            return cap;
        }

        public override float Evaluate()
        {
            return MathUtils.Normalization01WithClamp01(GetParameterValue(0), GetParameterValue(1), GetParameterValue(2));
        }
    }
}
