
namespace SerV112.UtilityAIEditor
{


    public struct LogisticCurve : ICurve
    {
        private float m_Steepness;
        private float m_Offset;
        public LogisticCurve(float steepness = 0.5f, float offset = 0)
        {
            m_Steepness = steepness;
            m_Offset = offset;
        }
        public float Evaluate(float x)
        {

            return CurveUtils.LogisticCurve(x, m_Steepness, m_Offset);
        }
    }
}
