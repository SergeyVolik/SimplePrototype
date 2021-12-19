
namespace SerV112.UtilityAIEditor
{


    public struct LogitCurve : ICurve
    {
        private float m_LogBase;
        public LogitCurve(float logBase = 0.5f)
        {
            m_LogBase = logBase;

        }
        public float Evaluate(float x)
        {
            return CurveUtils.LogitCurve(x, m_LogBase);
        }
    }
}
