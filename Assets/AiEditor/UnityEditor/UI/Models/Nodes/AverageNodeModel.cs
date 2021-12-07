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
    public class AverageNodeModel : NodeModel, IExtendableInputPortNode
    {
        [SerializeField, HideInInspector]
        int m_ScoreInputCount = 2;

        public int ScoreInputCount => m_ScoreInputCount;

        private int GetNumbderConectedPorts()
        {
            var result = Ports.Where(e => e.Direction == PortDirection.Input && e.GetConnectedEdges().ToList().Count > 0).Count();
            return result;
        }

        protected override void OnDefineNode()
        {
            base.OnDefineNode();


            for (var i = 0; i < m_ScoreInputCount; i++)
                AddInputPort($"Input{i}", PortType.Data, AIStencil.NormalizedFloat, options: PortModelOptions.NoEmbeddedConstant);

            AddOutputPort("Output", PortType.Data, AIStencil.NormalizedFloat, options: PortModelOptions.NoEmbeddedConstant);

            
        }


        //public override void OnConnection(IPortModel selfConnectedPortModel, IPortModel otherConnectedPortModel)
        //{
        //    if (selfConnectedPortModel.DataTypeHandle == AIStencil.NormalizedFloat && selfConnectedPortModel.Direction == PortDirection.Input && m_ScoreInputCount == GetNumbderConectedPorts() + 1)
        //    {
        //        m_ScoreInputCount++;

        //        DefineNode();
        //    }
        //}

        ///// <inheritdoc />
        //public override void OnDisconnection(IPortModel selfConnectedPortModel, IPortModel otherConnectedPortModel)
        //{
        //    if (selfConnectedPortModel.DataTypeHandle == AIStencil.NormalizedFloat && m_ScoreInputCount > 2 && selfConnectedPortModel.Direction == PortDirection.Input)
        //    {
        //        m_ScoreInputCount--;

        //        DefineNode();
        //    }
        //}

        public override PortCapacity GetPortCapacity(IPortModel portModel)
        {
            PortCapacity cap = PortCapacity.Single;
            return cap;//Stencil?.GetPortCapacity(portModel, out cap) ?? false ? cap : portModel?.GetDefaultCapacity() ?? PortCapacity.Multi;
        }

        public void AddPort()
        {
            m_ScoreInputCount++;
            DefineNode();

        }
        public IEnumerable<IGraphElementModel> RemovePort()
        {
            m_ScoreInputCount--;

            var ports = Ports.Where(e => e.Direction == PortDirection.Input && e.DataTypeHandle == AIStencil.NormalizedFloat).ToList();
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
