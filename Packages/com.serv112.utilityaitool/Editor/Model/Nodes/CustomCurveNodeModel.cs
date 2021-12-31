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
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "Utility Curves/Custom Curve")]
    public class CustomCurveNodeModel : CurveNodeModel
    {

        [SerializeField, HideInInspector]
        AnimationCurve m_CustomCurve = AnimationCurve.Linear(0, 0, 1, 1);

        public AnimationCurve CustomCurve { get => m_CustomCurve; set => m_CustomCurve = value; }

        public override float Evaluate()
        {
            return 0;
        }

    }
}
