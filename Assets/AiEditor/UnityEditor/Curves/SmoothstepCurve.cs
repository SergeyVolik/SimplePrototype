
namespace SerV112.UtilityAIEditor
{


    public struct SmoothstepCurve : ICurve
    {
        public float Evaluate(float x)
        {
            return CurveUtils.SmoothstepCurve(x);
        }
    }
}
