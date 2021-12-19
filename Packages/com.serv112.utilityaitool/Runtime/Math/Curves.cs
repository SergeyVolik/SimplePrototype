using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace SerV112.UtilityAIRuntime
{
    public static partial class MathEx
    {
        public static float normalizeclamp01(this float x, float min, float max)
        {
            return math.clamp((x - min) / (max - min), 0, 1);

        }

        public static float normalize01(this float x, float min, float max)
        {
            return (x - min) / (max - min);
        }
    }

    public static class CurveUtils
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
            return math.clamp((x / slope) - offset, 0, 1);
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
            return math.clamp(1 - ((1 - math.pow(x, exponent)) / 1) + offset, 0, 1);
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
            return math.clamp(math.sin(x * math.PI * steepness) + offset, 0, 1);
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
            return math.clamp(1 - math.cos(x * math.PI * steepness) + offset, 0, 1);
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
            float expPow = -k * ((4 * (math.E) * (x - x0)) - (2 * math.E));
            return math.clamp(1 / (1 + math.pow(math.E, expPow)), 0, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="logBase"> 0 <= exponent <= 1 </param>

        /// <returns></returns>
        public static float LogitCurve(float x, float logBase = 0.5f)
        {

            return math.clamp((Mathf.Log(x / (1 - x), logBase) + (2 * math.E)) / (4 * math.E), 0, 1);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>

        /// <returns></returns>
        public static float SmoothstepCurve(float x)
        {
            return math.clamp(x * x * (3 - 2 * x), 0, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="logBase"> 0 <= exponent <= 1 </param>

        /// <returns></returns>
        public static float SmootherstepCurve(float x)
        {
            return math.clamp(x * x * x * (x * (6 * x - 15) + 10), 0, 1);
        }
    }
}
