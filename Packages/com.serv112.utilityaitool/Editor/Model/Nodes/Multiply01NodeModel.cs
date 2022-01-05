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
        protected override void OnDefineNode()
        {
            m_MinInputPorts = 2;

            base.OnDefineNode();
        }
        public override float Evaluate()
        {
            if (m_ParameterNames.Length == 0)
                return 0;

            float value = GetParameterValue(0);
            for (int i = 1; i < m_ParameterNames.Length; i++)
            {
                value *= GetParameterValue(i);
            }


            return value;
        }
    }
}
