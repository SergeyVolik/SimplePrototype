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
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "StateGroup")]
    public class StateGroupNodeModel : ExtendableInputPortNode, IScriptName, IFieldName
    {

       

        [SerializeField, HideInInspector]
        string m_EnumName = "Default";

        [SerializeField, HideInInspector]
        string m_FieldName = "Default";

        public override string Title { get => base.Title; set => base.Title = m_EnumName + " (StateGroup)"; }
        public override string DisplayTitle => Title;

        public string Name { get => m_EnumName; set => m_EnumName = value; }
        public string FieldName { get => m_FieldName; set => m_FieldName = value; }

        public const string InspectorLabelNameText = "Name";

        protected override void OnDefineNode()
        {
            m_MinInputPorts = 2;

            base.OnDefineNode();
        }
        public StateGroupNodeModel()
        {

            InputType = AIGraphCustomTypes.AIAction;
            OutputType = AIGraphCustomTypes.AIGroup;

        }

      
        public override float Evaluate()
        {
            var ports = this.GetInputPorts().ToList();

            float max = float.NegativeInfinity;
            int maxIndex = 0;
            for (int i = 0; i < ports.Count; i++)
            {
                var value = ports[i].GetValue();

               
                if (value > max)
                {
                    max = value;
                    maxIndex = i;
                }
            }

            return maxIndex;
        }
    }
}
