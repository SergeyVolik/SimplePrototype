using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{

    public class ExponentialCurveNodePart : CurveNodePart
    {

        public ExponentialCurveNodePart(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
           : base(name, model, ownerElement, parentClassName)
        {

        }

        protected override void UpdateCurveFromModel()
        {
            if (!(m_Model is ExponentialCurveNodeModel model))
                return;

            m_CurveView.Curve = new ExponentialCurve(model.Exponent, model.OffsetY);
        }
    }
}
