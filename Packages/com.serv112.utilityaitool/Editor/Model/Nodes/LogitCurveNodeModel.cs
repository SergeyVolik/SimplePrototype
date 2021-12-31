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
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "Utility Curves/Logit Curve")]
    public class LogitCurveNodeModel : CurveNodeModel, ILogBase
    {

        [SerializeField, HideInInspector]
        float m_LogBase = 2.5f;


        public float LogBase { get => m_LogBase; set => m_LogBase = value; }

        public float LogBaseMax => k_LogBaseMax;

        public float LogBaseMin => k_LogBaseMin;

        public const float k_LogBaseMax = 5f;
        public const float k_LogBaseMin = 0.1f;

        public override float Evaluate()
        {
            return UtilityAIMath.LogitCurve(GetParameterValue(0), m_LogBase);
        }

    }
}
