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
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "StateNode")]
    public class StateNodeModel : NodeModel
    {

        [SerializeField, HideInInspector]
        string m_Name = "Default Name";

        public string Name { get => m_Name; set => m_Name = value; }
        protected override void OnDefineNode()
        {
            base.OnDefineNode();

            
            AddInputPort("Input", PortType.Data, TypeHandle.Float, options: PortModelOptions.NoEmbeddedConstant);
            this.AddExecutionOutputPort("Output", orientation: PortOrientation.Vertical);
        }

        public override PortCapacity GetPortCapacity(IPortModel portModel)
        {
            PortCapacity cap = PortCapacity.Single;
            return cap;//Stencil?.GetPortCapacity(portModel, out cap) ?? false ? cap : portModel?.GetDefaultCapacity() ?? PortCapacity.Multi;
        }


    }
}
