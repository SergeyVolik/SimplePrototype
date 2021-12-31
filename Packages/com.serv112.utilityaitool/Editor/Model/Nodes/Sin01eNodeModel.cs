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
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "Utility Curves/Sin01")]
    public class Sin01NodeModel : CurveNodeModel, ISteepnessable
    {

        [SerializeField, HideInInspector]
        float m_Stepness = 0.5f;

        public float Steepness { get => m_Stepness; set => m_Stepness = value; }

        public const float K_SteepnessMax = 5;
        public const float k_SteepnessMin = 0;

        public float SteepnessMax => K_SteepnessMax;

        public float SteepnessMin => k_SteepnessMin;
       

        public override float Evaluate()
        {
            return UtilityAIMath.SineCurve(GetParameterValue(0), m_Stepness, 0.5f, 0);
        }

    }
}
