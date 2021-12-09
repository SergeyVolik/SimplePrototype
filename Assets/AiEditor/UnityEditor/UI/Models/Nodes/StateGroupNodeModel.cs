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
    public class StateGroupNodeModel : NodeModel, IScriptName, IExtendableInputPortNode
    {

       

        [SerializeField, HideInInspector]
        string m_EnumName = "Default";

        public override string Title { get => base.Title; set => base.Title = m_EnumName + " (StateGroup)"; }
        public override string DisplayTitle => Title;
        public int VerticalInputCount => m_VerticalInputCount;

        public string Name { get => m_EnumName; set => m_EnumName = value; }

        public const string InspectorLabelNameText = "Name";

        //public int GetNumbderConectedPorts()
        //{
        //    var result = Ports.Where(e => e.Direction == PortDirection.Input && e.DataTypeHandle == TypeHandle.ExecutionFlow &&  e.GetConnectedEdges().ToList().Count > 0).Count();
        //    return result;
        //}

        [SerializeField, HideInInspector]
        int m_VerticalInputCount = 1;
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
                this.AddExecutionInputPort("Action " + (i + 1), orientation: PortOrientation.Vertical);
            }


        }


        public override PortCapacity GetPortCapacity(IPortModel portModel)
        {
            PortCapacity cap = PortCapacity.Single;
            return cap;//Stencil?.GetPortCapacity(portModel, out cap) ?? false ? cap : portModel?.GetDefaultCapacity() ?? PortCapacity.Multi;
        }

        public void GenereteStateGroup(string path)
        {
            var ports = this.GetPorts(PortDirection.Input, PortType.Execution).ToList();
            List<string> @params = new List<string>();
            for (int i = 0; i < ports.Count; i++)
            {
                var edges = ports[i].GetConnectedEdges().ToList();

                if (edges.Count > 0)
                {
                    var edge = edges[0].FromPort.NodeModel as StateNodeModel;
                    @params.Add(edge.Name);
                }
            }

            var asset = this.GraphModel.AssetModel as AIGraphAssetModel;
            T4GenUtils.CreateEnum(path, this.Name, new CreateEnumSettings(this.Name, @params, asset.Namespace));
        }

       
    }
}
