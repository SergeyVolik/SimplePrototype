
using SerV112.UtilityAI.Math;

namespace SerV112.UtilityAIEditor
{


    public struct ExponentialCurve : ICurve
    {
        private float m_Exponent;
        private float m_Offset;
        public ExponentialCurve(float exponent = 1, float offset = 0)
        {
            m_Exponent = exponent;
            m_Offset = offset;
        }
        public float Evaluate(float x)
        {
            return UtilityAIMath.ExponentialCurve(x, m_Exponent, m_Offset);
        }
    }
}
