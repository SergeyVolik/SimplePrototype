
using SerV112.UtilityAI.Math;

namespace SerV112.UtilityAIEditor
{


    public struct CosineCurve : ICurve
    {
        private float m_Steepness;
        private float m_Offset;
        private float m_OffsetX;
        public CosineCurve(float steepness = 0.5f, float offset = 0, float offsetX = 0)
        {
            m_OffsetX = offsetX;
            m_Steepness = steepness;
            m_Offset = offset;
        }
        public float Evaluate(float x)
        {
            return UtilityAIMath.CosineCurve(x, m_Steepness, m_Offset, m_OffsetX);
        }
    }
}
