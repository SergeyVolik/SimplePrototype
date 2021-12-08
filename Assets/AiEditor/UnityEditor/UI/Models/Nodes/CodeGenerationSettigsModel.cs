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
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "CodeGen/CodeGenerationSettigs")]
    public class CodeGenerationSettigsModel : NodeModel, INamespaceField, IExtendableInputPortNode
    {

        [SerializeField, HideInInspector]
        string m_Namespace = "MyCompanyName.MyProjectName"; // $"{Application.productName}.{Application.companyName}";

        public string Namespace { get => m_Namespace; set => m_Namespace = value; }

        public override string Title { get => base.Title; set => base.Title =  $"CodeGenerationSettigs ({value})"; }
        public override string DisplayTitle => Title;
        protected override void OnDefineNode()
        {
            base.OnDefineNode();

            for (int i = 0; i < m_VerticalInputCount; i++)
            {
                this.AddExecutionInputPort($"CodePart{i+1}", orientation: PortOrientation.Horizontal);
            }
            

        }

        public int VerticalInputCount => m_VerticalInputCount;
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
        public override PortCapacity GetPortCapacity(IPortModel portModel)
        {
            PortCapacity cap = PortCapacity.Single;
            return cap;//Stencil?.GetPortCapacity(portModel, out cap) ?? false ? cap : portModel?.GetDefaultCapacity() ?? PortCapacity.Multi;
        }


    }
}
