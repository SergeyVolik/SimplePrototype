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
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "Utility Curves/Sine Curve")]
    public class SineCurveNodeModel : CurveNodeModel, IOffsetableY, IOffsetableX, ISteepnessable
    {

        [SerializeField, HideInInspector]
        float m_Stepness = 0.5f;

        [SerializeField, HideInInspector]
        float m_OffsetY = 0;
       

        public float Steepness { get => m_Stepness; set => m_Stepness = value; }

        public const float K_SteepnessMax = 1;
        public const float k_SteepnessMin = 0;
        public float OffsetY { get => m_OffsetY; set => m_OffsetY = value; }
        public float OffsetYMax => k_OffsetYMax; 
        public float OffsetYMin  => k_OffsetYMin;

        [SerializeField, HideInInspector]
        float m_OffsetX = 0;

        public float OffsetX { get => m_OffsetX; set => m_OffsetX = value; }
        public float OffsetXMax => k_OffsetXMax;
        public float OffsetXMin => k_OffsetXMin;

        public const float k_OffsetXMax = 1f;
        public const float k_OffsetXMin = -1f;
        public float SteepnessMax => K_SteepnessMax;

        public float SteepnessMin => k_SteepnessMin;

      

        public const float k_OffsetYMax = 1f;
        public const float k_OffsetYMin = -1f;

        public override float Evaluate()
        {
            return UtilityAIMath.SineCurve(GetParameterValue(0), m_Stepness, m_OffsetY, m_OffsetX);
        }

    }
}
