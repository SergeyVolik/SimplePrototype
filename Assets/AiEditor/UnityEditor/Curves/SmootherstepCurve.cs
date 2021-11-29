
namespace SerV112.UtilityAIEditor
{


    public struct SmootherstepCurve : ICurve
    {
        public float Evaluate(float x)
        {
            return CurveUtils.SmootherstepCurve(x);
        }
    }
}
