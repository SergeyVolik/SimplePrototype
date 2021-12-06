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
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "State")]
    public class StateNodeModel : NodeModel, INameable
    {

        [SerializeField, HideInInspector]
        string m_Name = "Default Name";

        public string Name { get => m_Name; set => m_Name = value; }

        public override string Title { get => base.Title + " (State)"; set => base.Title = value; }
        protected override void OnDefineNode()
        {
            base.OnDefineNode();

            
            AddInputPort("Input", PortType.Data, TypeHandle.Float, options: PortModelOptions.Default);
            this.AddExecutionOutputPort("Output", orientation: PortOrientation.Vertical);
        }

        public override PortCapacity GetPortCapacity(IPortModel portModel)
        {
            PortCapacity cap = PortCapacity.Single;
            return cap;//Stencil?.GetPortCapacity(portModel, out cap) ?? false ? cap : portModel?.GetDefaultCapacity() ?? PortCapacity.Multi;
        }


    }
}
