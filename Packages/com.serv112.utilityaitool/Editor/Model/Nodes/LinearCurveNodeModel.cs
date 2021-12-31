using SerV112.UtilityAI.Math;
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
    public class LinearCurveNodeModel : CurveNodeModel, IOffsetableY, ISlopeable
    {

        [SerializeField, HideInInspector]
        float m_Slope = 1;

        [SerializeField, HideInInspector]
        float m_Offset = 0;

        public float Slope { get => m_Slope; set => m_Slope = value; }
        public const float k_SlopeMax = 3;
        public const float k_SlopeMin = 0;
        public float OffsetY { get => m_Offset; set => m_Offset = value; }
        public float OffsetYMax => k_OffsetMax;
        public float OffsetYMin => k_OffsetMin;

        public float SlopeMax => k_SlopeMax;

        public float SlopeMin => k_SlopeMin;

        public const float k_OffsetMax = 3f;
        public const float k_OffsetMin = -3f;

        public override float Evaluate()
        {
            return UtilityAIMath.LinearCurve(GetParameterValue(0), m_Slope, m_Offset);
        }
    }
}
