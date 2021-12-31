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
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "Utility Curves/Cosine Curve")]
    public class CosineCurveNodeModel : CurveNodeModel, IOffsetableY, ISteepnessable, IOffsetableX
    {

        [SerializeField, HideInInspector]
        float m_Stepness = 1;

        [SerializeField, HideInInspector]
        float m_Offset = -0.5f;

        public float Steepness { get => m_Stepness; set => m_Stepness = value; }

        public const float K_SteepnessMax = 4f;
        public const float k_SteepnessMin = 0;
        public float OffsetY { get => m_Offset; set => m_Offset = value; }
        public float OffsetYMax => k_OffsetMax; 
        public float OffsetYMin  => k_OffsetMin;

        public float SteepnessMax => K_SteepnessMax;

        public float SteepnessMin => k_SteepnessMin;

        public const float k_OffsetMax = 2f;
        public const float k_OffsetMin = -2f;

        [SerializeField, HideInInspector]
        float m_OffsetX = 0;

        public float OffsetX { get => m_OffsetX; set => m_OffsetX = value; }
        public float OffsetXMax => k_OffsetXMax;
        public float OffsetXMin => k_OffsetXMin;

        public const float k_OffsetXMax = 2f;
        public const float k_OffsetXMin = -2f;

        public override float Evaluate()
        {
            return UtilityAIMath.CosineCurve(GetParameterValue(0), m_Stepness, OffsetY, OffsetX);
        }

    }
}
