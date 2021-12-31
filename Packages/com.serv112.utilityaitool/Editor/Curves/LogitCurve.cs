
using SerV112.UtilityAI.Math;

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
            return UtilityAIMath.LogitCurve(x, m_LogBase);
        }
    }
}
