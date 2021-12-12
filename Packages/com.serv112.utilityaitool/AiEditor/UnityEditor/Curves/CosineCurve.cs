
namespace SerV112.UtilityAIEditor
{


    public struct CosineCurve : ICurve
    {
        private float m_Steepness;
        private float m_Offset;
        public CosineCurve(float steepness = 0.5f, float offset = 0)
        {
            m_Steepness = steepness;
            m_Offset = offset;
        }
        public float Evaluate(float x)
        {
            return CurveUtils.CosineCurve(x, m_Steepness, m_Offset);
        }
    }
}
