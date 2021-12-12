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
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "Utility Curves/Linear Curve")]
    public class LinearCurveNodeModel : CurveNodeModel, IOffsetable, ISlopeable
    {

        [SerializeField, HideInInspector]
        float m_Slope = 1;

        [SerializeField, HideInInspector]
        float m_Offset = 0;

        public float Slope { get => m_Slope; set => m_Slope = value; }
        public const float k_SlopeMax = 3;
        public const float k_SlopeMin = -3;
        public float Offset { get => m_Offset; set => m_Offset = value; }
        public float OffsetMax => k_OffsetMax;
        public float OffsetMin => k_OffsetMin;

        public float SlopeMax => k_SlopeMax;

        public float SlopeMin => k_SlopeMin;

        public const float k_OffsetMax = .4f;
        public const float k_OffsetMin = -.4f;

        public override float Evaluate()
        {
            return CurveUtils.LinearCurve(GetParameterValue(0), m_Slope, m_Offset);
        }
    }
}
