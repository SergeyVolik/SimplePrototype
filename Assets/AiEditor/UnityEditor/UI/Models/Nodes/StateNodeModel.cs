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
    public class StateNodeModel : MathNode, INameable
    {

        [SerializeField, HideInInspector]
        string m_Name = "State";

        public const string DefaultTooltip = "StateNodeModel";

        public string Name { get => m_Name; set => m_Name = value; }

        public override string Title { get => base.Title; set => base.Title = m_Name + " (State)"; }
        public override string DisplayTitle => Title;


        protected override void OnDefineNode()
        {


            base.OnDefineNode();

            
            AddInputPort("Input", PortType.Data, AIStencil.NormalizedFloat, options: PortModelOptions.Default);
            AddOutputPort("Out", PortType.Data, AIStencil.NormalizedFloat, options: PortModelOptions.Default);

            
        }


        public override float Evaluate()
        {
            var port = this.GetInputPorts().FirstOrDefault();

            return port.GetValue();
        }
    }
}
