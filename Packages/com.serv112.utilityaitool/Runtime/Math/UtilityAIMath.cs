using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace SerV112.UtilityAI.Math
{
    public static partial class UtilityAIMath
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float NormalizeClamp01(this float x, float min, float max)
        {
            return math.clamp((x - min) / (max - min), 0, 1);

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float OneMinus(this float value)
        {
            return 1 - value;

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Normalize01(this float x, float min, float max)
        {
            return (x - min) / (max - min);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Average(NativeArray<float> values)
        {
            float sum = 0;

            for (int i = 0; i < values.Length; i++)
            {
                sum += values[i];
            }

            return sum / values.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(NativeArray<float> values)
        {
            float Max = float.MinValue;
            
            for (int i = 0; i < values.Length; i++)           
                if (Max < values[i])
                    Max = values[i];
            
            return Max;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(float first, float second)
        {
            return first < second ? second : first;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(float first, float second)
        {
            return first > second ? second : first;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(NativeArray<float> values)
        {
            float Min = float.MaxValue;

            for (int i = 0; i < values.Length; i++)
                if (Min > values[i])
                    Min = values[i];

            return Min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Multiply(NativeArray<float> values)
        {
            float result = values[0];

            for (int i = 1; i < values.Length; i++)
                result *= values[i];

            return result;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SelectAnswerIndex(NativeArray<float> values)
        {
            float max = float.MinValue;
            int index = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] > max)
                {
                    max = values[i];
                    index = i;
                }
                
            }

            return index;
        }

    }

    public static partial class UtilityAIMath
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="slope"> 0 <= slope <= 5 </param>
        /// <param name="offset"> -1 <= offset <= 1</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SineCurve(float x, float steepness = 0.5f, float offsetY = 0, float offsetX = 0)
        {
            return math.clamp(math.sin(x * math.PI * steepness + offsetX) + offsetY, 0, 1);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SineCurve01(float x, float steepness = 0.5f, float offseX = 0)
        {
            return math.clamp(math.sin(x * math.PI * steepness + offseX) / 2 + 0.5f, 0, 1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="steepness"> 0 <= exponent <= 1 </param>
        /// <param name="offset"> -1 <= offset <= 1</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CosineCurve(float x, float steepness = 0.5f, float offset = 0, float offsetX = 0)
        {
            return math.clamp(1 - math.cos(x * math.PI * steepness + offsetX) / 2 + offset, 0, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="steepness"> 1 <= steepness <= 3 </param>
        /// <param name="offsetX"> -1 <= offsetX <= 1</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float LogisticCurve(float x, float steepness = 1, float offsetX = 0)
        {
            float expPow = -steepness * ((4 * (math.E) * (x - offsetX)) - (2 * math.E));
            return math.clamp(1 / (1 + math.pow(math.E, expPow)), 0, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="logBase"> 0 <= exponent <= 1 </param>

        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float LogitCurve(float x, float logBase = 0.5f)
        {

            return math.clamp((Mathf.Log(x / (1 - x), logBase) + (2 * math.E)) / (4 * math.E), 0, 1);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>

        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SmootherstepCurve(float x)
        {
            return math.clamp(x * x * x * (x * (6 * x - 15) + 10), 0, 1);
        }
    }
}
