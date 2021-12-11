﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{

    [Serializable]
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "Utility Curves/Logistic Curve")]
    public class LogisticCurveNodeModel : CurveNodeModel, IOffsetable, ISteepnessable
    {
        public override string Tooltip { get => @"LogisticCurveNodeModel";
            set { } }

        [SerializeField, HideInInspector]
        float m_Stepness = 1;

        [SerializeField, HideInInspector]
        float m_Offset = 0;

        public float Steepness { get => m_Stepness; set => m_Stepness = value; }
        public const float k_SteepnessMax = 3;
        public const float k_SteepnessMin = -3;
        public float Offset { get => m_Offset; set => m_Offset = value; }
        public float OffsetMax => k_OffsetMax;
        public float OffsetMin => k_OffsetMin;

        public float SteepnessMax => k_SteepnessMax;

        public float SteepnessMin => k_SteepnessMin;

        public const float k_OffsetMax = .4f;
        public const float k_OffsetMin = -.4f;

        public override float Evaluate()
        {
            return CurveUtils.LogisticCurve(GetParameterValue(0), m_Stepness, m_Offset);
        }

    }
}
