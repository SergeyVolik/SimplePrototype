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
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "Utility Curves/Smootherstep Curve")]
    public class SmootherstepCurveNodeModel : CurveNodeModel
    {

        public override float Evaluate()
        {
            return CurveUtils.SmootherstepCurve(GetParameterValue(0));
        }
    }
}
