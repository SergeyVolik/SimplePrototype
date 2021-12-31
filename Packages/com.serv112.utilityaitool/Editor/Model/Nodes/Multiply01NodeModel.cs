using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    [Serializable]
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "Multiply01")]
    public class Multiply01NodeModel : ExtendableInputPortNode
    {

        public override float Evaluate()
        {
            if (InputPorts.Count == 0)
                return 0;

            float value = GetParameterValue(0);
            for (int i = 1; i < InputPorts.Count; i++)
            {
                value *= GetParameterValue(i);
            }


            return value;
        }
    }
}
