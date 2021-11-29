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
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "Utility Curves/Boolean Curve Score")]
    public class BooleanCurveScoreNodeModel : NodeModel
    {

        [SerializeField, HideInInspector]
        float m_Slope = 0;

        [SerializeField, HideInInspector]
        float m_Offset = 0;

        public float Steepness { get => m_Slope; set => m_Slope = value; }
        public const float SteepnessMax = 3;
        public const float SteepnessMin = -3;
        public float Offset { get => m_Offset; set => m_Offset = value; }
        public const float OffsetMax = .4f;
        public const float OffsetMin = -.4f;



        protected override void OnDefineNode()
        {
            base.OnDefineNode();

            
            AddInputPort("Input", PortType.Data, TypeHandle.Float, options: PortModelOptions.NoEmbeddedConstant);
            AddOutputPort("Output", PortType.Data, TypeHandle.Float, options: PortModelOptions.NoEmbeddedConstant);
        }

        public override PortCapacity GetPortCapacity(IPortModel portModel)
        {
            PortCapacity cap = PortCapacity.Single;
            return cap;
        }


    }
}
