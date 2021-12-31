using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;

namespace SerV112.UtilityAIEditor
{
    public abstract class ExtendableInputPortNode : NormalizedFunctionNodeModel, IExtendableInputPortNode
    {
        protected List<string> InputPorts = new List<string>();

        public int NumberOfInputPorts => m_VerticalInputCount;
        [SerializeField, HideInInspector]
        int m_VerticalInputCount = 2;

        protected virtual string PortName { get; set; } = "Input";

        public ExtendableInputPortNode()
        {
            for (int i = 0; i < NumberOfInputPorts; i++)
            {
                InputPorts.Add(PortName + i);
            }
            m_ParameterNames = InputPorts.ToArray();
        }
        public void AddPort()
        {
            m_VerticalInputCount++;
            InputPorts.Add($"{PortName}{InputPorts.Count}");
            m_ParameterNames = InputPorts.ToArray();
            DefineNode();

        }
        public IEnumerable<IGraphElementModel> RemovePort()
        {
            m_VerticalInputCount--;
            InputPorts.Remove(InputPorts[InputPorts.Count - 1]);
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
    }
}

