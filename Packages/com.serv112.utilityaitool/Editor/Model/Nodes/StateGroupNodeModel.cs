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
    public class StateGroupNodeModel : NormalizedFunctionNodeModel, IScriptName, IExtendableInputPortNode, IFieldName
    {

       

        [SerializeField, HideInInspector]
        string m_EnumName = "Default";

        [SerializeField, HideInInspector]
        string m_FieldName = "Default";

        public override string Title { get => base.Title; set => base.Title = m_EnumName + " (StateGroup)"; }
        public override string DisplayTitle => Title;
        public int VerticalInputCount => m_VerticalInputCount;

        public string Name { get => m_EnumName; set => m_EnumName = value; }
        public string FieldName { get => m_FieldName; set => m_FieldName = value; }

        public const string InspectorLabelNameText = "Name";

        List<string> InputPorts = new List<string>();
        public StateGroupNodeModel()
        {
            InputType = AIGraphCustomTypes.AIAction;
            OutputType = AIGraphCustomTypes.AIGroup;
            InputPorts.Add($"State{InputPorts.Count}");
            m_ParameterNames = InputPorts.ToArray();

        }

        [SerializeField, HideInInspector]
        int m_VerticalInputCount = 1;
        public void AddPort()
        {
            m_VerticalInputCount++;
            InputPorts.Add($"State{InputPorts.Count}");
            m_ParameterNames = InputPorts.ToArray();
            DefineNode();

        }
        public IEnumerable<IGraphElementModel> RemovePort()
        {
            m_VerticalInputCount--;
            InputPorts.Remove(InputPorts[InputPorts.Count-1]);
            m_ParameterNames = InputPorts.ToArray();
            var ports = this.GetInputPorts().ToList();
            IEnumerable<IGraphElementModel> edgesToRemove = null;

            if (ports.Count > 0)
            {
                edgesToRemove = ports[ports.Count - 1].GetConnectedEdges().ToList();
            }

            DefineNode();

            return edgesToRemove;

        }



        public override void OnConnection(IPortModel selfConnectedPortModel, IPortModel otherConnectedPortModel)
        {
           
        }

        /// <inheritdoc />
        public override void OnDisconnection(IPortModel selfConnectedPortModel, IPortModel otherConnectedPortModel)
        {

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
