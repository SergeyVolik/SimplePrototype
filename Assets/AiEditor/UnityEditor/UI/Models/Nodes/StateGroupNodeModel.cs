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
    public class StateGroupNodeModel : NodeModel, IExtendableInputPortNode
    {


        [SerializeField, HideInInspector]
        int m_VerticalInputCount = 2;

        [SerializeField, HideInInspector]
        string m_ActionGroupName = "Default Name";

        [SerializeField, HideInInspector]
        string m_FileNamespace = "";
        public int VerticalInputCount => m_VerticalInputCount;

        public string FileNamespace { get => m_FileNamespace; set => m_FileNamespace = value; }
        public string ActionGroupName { get => m_ActionGroupName; set => m_ActionGroupName = value; }

        public const string InspectorLabelNameText = "Name";
        public const string InspectorLabelNamespaceText = "Namespace";
        protected override void OnDefineNode()
        {
            base.OnDefineNode();


            Title = "Actions Group";

            for (var i = 0; i < m_VerticalInputCount; i++)
                this.AddExecutionInputPort("Action " + (i + 1), orientation: PortOrientation.Vertical);

        }

        public void AddPort(PortOrientation orientation, PortDirection direction)
        {

            m_VerticalInputCount++;

            DefineNode();
        }

        public void RemovePort(PortOrientation orientation, PortDirection direction)
        {

            m_VerticalInputCount--;

            DefineNode();
        }

        //public override void OnConnection(IPortModel selfConnectedPortModel, IPortModel otherConnectedPortModel)
        //{
        //    if (selfConnectedPortModel.DataTypeHandle == TypeHandle.ExecutionFlow)
        //    {
        //        var edges = selfConnectedPortModel.GetConnectedEdges().ToList();
        //        if(edges.Count > 0)
        //            selfConnectedPortModel.GraphModel.DeleteEdges(edges.AsReadOnly());

        //        Debug.Log($"ExecutionFlow connected {edges.Count}");
        //    }
        //}

        ///// <inheritdoc />
        //public override void OnDisconnection(IPortModel selfConnectedPortModel, IPortModel otherConnectedPortModel)
        //{
        //}

        public override PortCapacity GetPortCapacity(IPortModel portModel)
        {
            PortCapacity cap = PortCapacity.Single;
            return cap;//Stencil?.GetPortCapacity(portModel, out cap) ?? false ? cap : portModel?.GetDefaultCapacity() ?? PortCapacity.Multi;
        }
    }
}
