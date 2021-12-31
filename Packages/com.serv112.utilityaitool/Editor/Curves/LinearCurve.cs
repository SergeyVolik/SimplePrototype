
using SerV112.UtilityAI.Math;

namespace SerV112.UtilityAIEditor
{


    public struct LinearCurve : ICurve
    {
        private float m_Slope;
        private float m_Offset;
        public LinearCurve(float slope = 1, float offset = 0)
        {
            m_Slope = slope;
            m_Offset = offset;
        }
        public float Evaluate(float x)
        {
            return UtilityAIMath.LinearCurve(x, m_Slope, m_Offset);
        }
    }
}
