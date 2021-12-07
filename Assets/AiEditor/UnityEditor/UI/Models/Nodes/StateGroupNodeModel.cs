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
    public class StateGroupNodeModel : NodeModel, INameable, IExtendableInputPortNode
    {

        [SerializeField, HideInInspector]
        int m_VerticalInputCount = 1;

        [SerializeField, HideInInspector]
        string m_ActionGroupName = "Default";

        public override string Title { get => base.Title + " (StateGroup)"; set => base.Title = value; }

        public int VerticalInputCount => m_VerticalInputCount;

        public string Name { get => m_ActionGroupName; set => m_ActionGroupName = value; }

        public const string InspectorLabelNameText = "Name";

        //public int GetNumbderConectedPorts()
        //{
        //    var result = Ports.Where(e => e.Direction == PortDirection.Input && e.DataTypeHandle == TypeHandle.ExecutionFlow &&  e.GetConnectedEdges().ToList().Count > 0).Count();
        //    return result;
        //}


        public void AddPort()
        {
            m_VerticalInputCount++;
            DefineNode();

        }
        public IEnumerable<IGraphElementModel> RemovePort()
        {
            m_VerticalInputCount--;

            var ports = Ports.Where(e => e.Direction == PortDirection.Input && e.DataTypeHandle == TypeHandle.ExecutionFlow).ToList();
            IEnumerable<IGraphElementModel> edgesToRemove = null;

            if (ports.Count > 0)
            {
                edgesToRemove = ports[ports.Count - 1].GetConnectedEdges().ToList();
            }

            DefineNode();

            return edgesToRemove;

        }
        protected override void OnDefineNode()
        {
            base.OnDefineNode();

            for (var i = 0; i < m_VerticalInputCount; i++)
            {
                //AddInputPort("Action " + (i + 1), PortType.Execution, AIStencil.AIAction, options: PortModelOptions.Default, orientation: PortOrientation.Vertical);
                this.AddExecutionInputPort("Action " + (i + 1), orientation: PortOrientation.Vertical);
            }

            AddInputPort("Namespace", PortType.Data, AIStencil.Namespace, options: PortModelOptions.Default);
        }

        public override void OnConnection(IPortModel selfConnectedPortModel, IPortModel otherConnectedPortModel)
        {
            //if (selfConnectedPortModel.DataTypeHandle == TypeHandle.ExecutionFlow)
            //{
            //    m_VerticalInputCount++;

            //    DefineNode();
            //}
        }

        /// <inheritdoc />
        public override void OnDisconnection(IPortModel selfConnectedPortModel, IPortModel otherConnectedPortModel)
        {
            //if (selfConnectedPortModel.DataTypeHandle == TypeHandle.ExecutionFlow)
            //{
            //    m_VerticalInputCount--;

            //    DefineNode();
            //    CommandDispatcher.Dispatch(new DisconnectNodeCommand(connectedNodes));


            //}

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
