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
    public class CodeGenerationSettigsModel : NodeModel, INamespaceField
    {

        [SerializeField, HideInInspector]
        string m_Namespace = "defaultName"; // $"{Application.productName}.{Application.companyName}";

        public string Namespace { get => m_Namespace; set => m_Namespace = value; }
        protected override void OnDefineNode()
        {
            base.OnDefineNode();

            
            var port = AddInputPort("Namespace", PortType.Data, AIStencil.Namespace, options: PortModelOptions.NoEmbeddedConstant);
           
        }

        public override PortCapacity GetPortCapacity(IPortModel portModel)
        {
            PortCapacity cap = PortCapacity.Single;
            return cap;//Stencil?.GetPortCapacity(portModel, out cap) ?? false ? cap : portModel?.GetDefaultCapacity() ?? PortCapacity.Multi;
        }


    }
}
