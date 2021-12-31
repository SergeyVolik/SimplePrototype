using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{

    public class LinearCurveNodePart : CurveNodePart
    {

        public LinearCurveNodePart(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
           : base(name, model, ownerElement, parentClassName)
        {

        }

        protected override void UpdateCurveFromModel()
        {
            if (!(m_Model is LinearCurveNodeModel model))
                return;

            m_CurveView.Curve = new LinearCurve(model.Slope, model.OffsetY);
        }
    }
}
