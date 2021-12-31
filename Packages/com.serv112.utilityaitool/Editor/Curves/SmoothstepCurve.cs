
using SerV112.UtilityAI.Math;

namespace SerV112.UtilityAIEditor
{


    public struct SmoothstepCurve : ICurve
    {
        public float Evaluate(float x)
        {
            return UtilityAIMath.SmoothstepCurve(x);
        }
    }
}
