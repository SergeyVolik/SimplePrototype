using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace SerV112.UtilityAIEditor
{


    public class CurveViewElement : VisualElement
    {

        public ICurve Curve;

        private float m_StartX;
        private float m_StartY;
        public CurveViewElement(float startX, float endX)
        {

            generateVisualContent += OnGenerateVisualContent;
            style.width = 200;
            style.height = 200;
            m_StartX = startX;
            m_StartY = endX;
        }
       
        void OnGenerateVisualContent(MeshGenerationContext mgc)
        {

            Rect r = contentRect;
            if (r.width < 0.01f || r.height < 0.01f)
                return; // Skip rendering when too small.

            VisualElementMeshUtils.CreateCurveMesh(r, 0.5f, Curve, mgc, 200, m_StartX, m_StartY);
        }
    }
}
