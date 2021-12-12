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
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "Average")]
    public class AverageNodeModel : NormalizedFunctionNodeModel, IExtendableInputPortNode
    {
        [SerializeField, HideInInspector]
        int m_ScoreInputCount = 2;

        public int ScoreInputCount => m_ScoreInputCount;


        List<string> inputPortNames = new List<string>();
        const string InputPortName = "Input";
        public AverageNodeModel()
        {
            inputPortNames.Add($"{InputPortName}{inputPortNames.Count}");
            inputPortNames.Add($"{InputPortName}{inputPortNames.Count}");
            m_ParameterNames = inputPortNames.ToArray();
        }


        public override PortCapacity GetPortCapacity(IPortModel portModel)
        {
            PortCapacity cap = PortCapacity.Single;
            return cap;
        }

        public void AddPort()
        {
            m_ScoreInputCount++;
            inputPortNames.Add($"{InputPortName}{inputPortNames.Count}");
            m_ParameterNames = inputPortNames.ToArray();
            DefineNode();

        }
        public IEnumerable<IGraphElementModel> RemovePort()
        {
            var last = inputPortNames[inputPortNames.Count - 1];
            inputPortNames.Remove(last);
            m_ParameterNames = inputPortNames.ToArray();
            m_ScoreInputCount--;

            var ports = this.GetInputPorts().ToList();
            IEnumerable<IGraphElementModel> edgesToRemove = null;

            if (ports.Count > 0)
            {
                edgesToRemove = ports[ports.Count - 1].GetConnectedEdges().ToList();
            }

            DefineNode();

            return edgesToRemove;

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
