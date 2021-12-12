
namespace SerV112.UtilityAIEditor
{


    public struct SineCurve : ICurve
    {
        private float m_Steepness;
        private float m_Offset;
        public SineCurve(float steepness = 0.5f, float offset = 0)
        {
            m_Steepness = steepness;
            m_Offset = offset;
        }
        public float Evaluate(float x)
        {
            return CurveUtils.SineCurve(x, m_Steepness, m_Offset);
        }
    }
}
