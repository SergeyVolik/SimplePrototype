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
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "Value01")]
    public class Value01NodeModel : NormalizedFunctionNodeModel
    {
        [SerializeField]
        public float Value01;

        public Value01NodeModel()
        {
            NoEmbeddedConstant = true;
        }


        public override float Evaluate()
        {
            return Value01;
        }
    }
}
