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
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "AIProcessor")]
    public class AIProcessorNodeModel : ExtendableInputPortNode, IExtendableInputPortNode, INameable
    {

        public const string InspectorName = "Name";


        [SerializeField, HideInInspector]
        private string m_Name = "AIProcessor";
        public string Name { get => m_Name; set => m_Name = value; }

        public override string Title { get => base.Title; set => base.Title = m_Name + " (AIProcessor)"; }
        public override string DisplayTitle => Title;

        List<string> inputPortNames = new List<string>();

        const string InputPortName = "StateGroup";
        public AIProcessorNodeModel()
        {
            InputType = AIGraphCustomTypes.AIGroup;
            DeadEndNode = true;
            inputPortNames.Add($"{InputPortName}{inputPortNames.Count}");
            m_ParameterNames = inputPortNames.ToArray();
           

        }

        protected override void OnDefineNode()
        {
            m_MinInputPorts = 1;

            base.OnDefineNode();
        }
        public override PortCapacity GetPortCapacity(IPortModel portModel)
        {
            PortCapacity cap = PortCapacity.Single;
            return cap;
        }

        public override float Evaluate()
        {
            if (inputPortNames.Count == 0)
                return 0;

            float sum = 0;
            for (int i = 0; i < inputPortNames.Count; i++)
            {
                sum += GetParameterValue(i);
            }

            sum /= inputPortNames.Count;

            return sum;
        }
    }
}
