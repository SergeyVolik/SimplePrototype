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
        private List<string> InputPorts;

        public int NumberOfInputPorts => m_ParameterNames.Length;


        protected virtual string PortName { get; set; } = "Input";
        protected int m_MinInputPorts;
        [SerializeField]
        protected int m_InputPorts;

        protected override void OnDefineNode()
        {
            if (m_ParameterNames.Length < m_MinInputPorts)
            {
                InputPorts = m_ParameterNames.ToList();

                for (int i = 0; i < m_MinInputPorts; i++)
                {
                    InputPorts.Add($"{PortName}{InputPorts.Count}");
                }

                m_ParameterNames = InputPorts.ToArray();
            }

            

            base.OnDefineNode();
        }

        public void AddPort()
        {
            InputPorts = m_ParameterNames.ToList();
            InputPorts.Add($"{PortName}{InputPorts.Count}");
            m_ParameterNames = InputPorts.ToArray();
            DefineNode();

        }
        public IEnumerable<IGraphElementModel> RemovePort()
        {

            InputPorts = m_ParameterNames.ToList();
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

