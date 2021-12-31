
using SerV112.UtilityAI.Math;

namespace SerV112.UtilityAIEditor
{


    public struct SineCurve : ICurve
    {
        private float m_Steepness;
        private float m_OffsetY;
        private float m_OffsetX;
        public SineCurve(float steepness = 0.5f, float offsetY = 0, float offsetX = 0)
        {
            m_Steepness = steepness;
            m_OffsetY = offsetY;
            m_OffsetX = offsetX;
        }
        public float Evaluate(float x)
        {
            return UtilityAIMath.SineCurve(x, m_Steepness, m_OffsetY, m_OffsetX);
        }
    }

    public struct SineCurve01 : ICurve
    {
        private float m_Steepness;
        private float m_OffsetX;
        public SineCurve01(float steepness = 0.5f, float offsetX = 0)
        {
            m_OffsetX = offsetX;
            m_Steepness = steepness;

        }
        public float Evaluate(float x)
        {
            return UtilityAIMath.SineCurve01(x, m_Steepness, m_OffsetX);
        }
    }
}
