
using SerV112.UtilityAI.Math;

namespace SerV112.UtilityAIEditor
{


    public struct SmootherstepCurve : ICurve
    {
        public float Evaluate(float x)
        {
            return UtilityAIMath.SmootherstepCurve(x);
        }
    }
}
