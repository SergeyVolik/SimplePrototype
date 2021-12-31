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
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "Math/OneMinus")]
    public class OneMinusNodeModel : NormalizedFunctionNodeModel
    {

        public OneMinusNodeModel()
        {
            m_ParameterNames = new string[]
                {
                    "Input"
                };

            //InputType = TypeHandle.Float;
            NoEmbeddedConstant = true;
        }

        public IPortModel InputPort => this.InputsById["Input"];


        public override float Evaluate()
        {
            return 1 - GetParameterValue(0);
        }
    }
}
