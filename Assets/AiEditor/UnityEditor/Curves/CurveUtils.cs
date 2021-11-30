using System;
using UnityEngine;

namespace SerV112.UtilityAIEditor
{
    public class CurveUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="slope"> 0 <= slope <= 5 </param>
        /// <param name="offset"> -1 <= offset <= 1</param>
        /// <returns></returns>
        public static float LinearCurve(float x, float slope = 1, float offset = 0)
        {
            return (x / slope) - offset;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="exponent"> 1 <= exponent <= 20 </param>
        /// <param name="offset"> 0 <= offset <= 1</param>
        /// <returns></returns>
        public static float ExponentialCurve(float x, float exponent = 1, float offset = 0)
        {
            return 1 - ((1 - Mathf.Pow(x, exponent)) / 1) + offset;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="steepness"> 0 <= exponent <= 1 </param>
        /// <param name="offset"> -1 <= offset <= 1</param>
        /// <returns></returns>
        public static float SineCurve(float x, float steepness = 0.5f, float offset = 0)
        {
            return Mathf.Sin(x*Mathf.PI* steepness) + offset;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="steepness"> 0 <= exponent <= 1 </param>
        /// <param name="offset"> -1 <= offset <= 1</param>
        /// <returns></returns>
        public static float CosineCurve(float x, float steepness = 0.5f, float offset = 0)
        {
            return 1 - Mathf.Cos(x * Mathf.PI * steepness) + offset;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="k"> 1 <= logBase <= 3 </param>
        /// <param name="x0"> -1 <= offset <= 1</param>
        /// <returns></returns>
        public static float LogisticCurve(float x, float k = 1, float x0 = 0)
        {
            float expPow = -k * ((4 * ((float)Math.E) * (x - x0)) - (2 * (float)Math.E));
            return 1 / (float)(1 + Mathf.Pow((float)Math.E, expPow));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="logBase"> 0 <= exponent <= 1 </param>

        /// <returns></returns>
        public static float LogitCurve(float x, float logBase = 0.5f)
        {

            return (Mathf.Log(x/(1 - x), logBase) + (2 * ((float)Math.E))) / (4 * ((float)Math.E));
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>

        /// <returns></returns>
        public static float SmoothstepCurve(float x)
        {
            return x*x*(3-2*x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="logBase"> 0 <= exponent <= 1 </param>

        /// <returns></returns>
        public static float SmootherstepCurve(float x)
        {
            return x*x*x*(x*(6*x - 15) + 10);
        }
    }
}
