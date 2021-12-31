using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

namespace SerV112.UtilityAIEditor
{

    public class Sin01NodePart : CurveNodePart
    {
        float offsetX = 0;
        public Sin01NodePart(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
           : base(name, model, ownerElement, parentClassName)
        {


            EditorApplication.update += CheckTween;
        }

        ~Sin01NodePart()
        {
            EditorApplication.update -= CheckTween;
        }
        void CheckTween()
        {
            if (stoped)
                Restart();
        }
        bool stoped = true;
        void Restart()
        {
            stoped = false;
            m_CurveView.experimental.animation.Start(0f, 100f, 100000, (b, val) =>
            {
                offsetX = val;

                if(val == 100f)
                    stoped = true;
                UpdatePartFromModel();
                

            }).Ease(Easing.Linear);

        }
        protected override void UpdateCurveFromModel()
        {
            if (!(m_Model is Sin01NodeModel model))
                return;

            m_CurveView.Curve = new SineCurve01(model.Steepness, offsetX);
        }
    }
}
