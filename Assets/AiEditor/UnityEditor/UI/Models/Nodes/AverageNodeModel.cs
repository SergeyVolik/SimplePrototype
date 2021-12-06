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
    public class AverageNodeModel : NodeModel
    {
        [SerializeField, HideInInspector]
        int m_ScoreInputCount = 2;

        public int ScoreInputCount => m_ScoreInputCount;
        protected override void OnDefineNode()
        {
            base.OnDefineNode();


            for (var i = 0; i < m_ScoreInputCount; i++)
                AddInputPort($"Input{i}", PortType.Data, TypeHandle.Float, options: PortModelOptions.NoEmbeddedConstant);

            AddOutputPort("Output", PortType.Data, TypeHandle.Float, options: PortModelOptions.NoEmbeddedConstant);
        }


        public override void OnConnection(IPortModel selfConnectedPortModel, IPortModel otherConnectedPortModel)
        {
            if (selfConnectedPortModel.DataTypeHandle == TypeHandle.Float && selfConnectedPortModel.Direction == PortDirection.Input)
            {
                m_ScoreInputCount++;

                DefineNode();
            }
        }

        /// <inheritdoc />
        public override void OnDisconnection(IPortModel selfConnectedPortModel, IPortModel otherConnectedPortModel)
        {
            if (selfConnectedPortModel.DataTypeHandle == TypeHandle.Float && m_ScoreInputCount > 2 && selfConnectedPortModel.Direction == PortDirection.Input)
            {
                m_ScoreInputCount--;

                DefineNode();
            }
        }

        public override PortCapacity GetPortCapacity(IPortModel portModel)
        {
            PortCapacity cap = PortCapacity.Single;
            return cap;//Stencil?.GetPortCapacity(portModel, out cap) ?? false ? cap : portModel?.GetDefaultCapacity() ?? PortCapacity.Multi;
        }


    }
}
