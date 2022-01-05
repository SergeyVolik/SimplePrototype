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
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "Max01")]
    public class Max01NodeModel : ExtendableInputPortNode
    {
        protected override void OnDefineNode()
        {
            m_MinInputPorts = 2;

            base.OnDefineNode();
        }
        public override float Evaluate()
        {
            throw new NotImplementedException();
        }
    }
}
