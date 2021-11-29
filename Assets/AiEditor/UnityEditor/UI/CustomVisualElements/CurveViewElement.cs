using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace SerV112.UtilityAIEditor
{


    class CurveViewElement : VisualElement
    {

        public ICurve Curve;
        public CurveViewElement(ICurve curve)
        {
            Curve = curve;
            generateVisualContent += OnGenerateVisualContent;
            style.width = 200;
            style.height = 200;

        }
       
        void OnGenerateVisualContent(MeshGenerationContext mgc)
        {

            Rect r = contentRect;
            if (r.width < 0.01f || r.height < 0.01f)
                return; // Skip rendering when too small.

            VisualElementMeshUtils.CreateCurveMesh(r, 2f, Curve, mgc);
        }
    }
}
