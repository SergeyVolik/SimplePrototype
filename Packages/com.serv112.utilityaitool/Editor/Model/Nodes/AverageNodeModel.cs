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
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "Average")]
    public class AverageNodeModel : ExtendableInputPortNode
    {

        public override float Evaluate()
        {
            if (InputPorts.Count == 0)
                return 0;

            float sum = 0;
            for (int i = 0; i < InputPorts.Count; i++)
            {
                sum += GetParameterValue(i);
            }

            sum /= InputPorts.Count;

            return sum;
        }
    }
}
