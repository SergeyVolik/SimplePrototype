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
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "Utility Curves/Exponential Curve")]
    public class ExponentialCurveNodeModel : CurveNodeModel, IOffsetableY, IExponential
    {

        [SerializeField, HideInInspector]
        float m_Exponential = 1;

        [SerializeField, HideInInspector]
        float m_Offset = 0;

        public const float k_ExponentialMax = 100f;
        public const float k_ExponentialMin = 0.1f;
        public float OffsetY { get => m_Offset; set => m_Offset = value; }
        public float OffsetYMax => k_OffsetMax; 
        public float OffsetYMin  => k_OffsetMin;


        public float Exponent { get => m_Exponential; set => m_Exponential = value; }

        public float ExponentMax => k_ExponentialMax;

        public float ExponentMin => k_ExponentialMin;

        public const float k_OffsetMax = .4f;
        public const float k_OffsetMin = -.4f;

        public override float Evaluate()
        {
            return UtilityAIMath.ExponentialCurve(GetParameterValue(0), m_Exponential, m_Offset);
        }

    }
}
