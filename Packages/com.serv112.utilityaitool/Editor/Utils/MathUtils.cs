using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SerV112.UtilityAIEditor
{
    static class MathUtils
    {
        public static bool PointInBounds(Vector3 newPoint1, Rect contentRect)
        {
            if (0 <= newPoint1.x && newPoint1.x <= contentRect.width && 0 <= newPoint1.y && newPoint1.y <= contentRect.height)
            {
                return true;
            }

            return false;
        }


        public static Vector2 GetPerpendicular(Vector2 vector)
        {
            var bx = -vector.y / vector.x;
            return new Vector2(bx, 1);
        }

        public static float LinearInterpolaton(Vector2 p0, Vector2 p1, float x)
        {
            return p0.y + (x - p0.x) * (p1.y - p0.y) / (p1.x - p0.x);
        }

        public static float Normalization01WithClamp01(float x, float min, float max)
        {
            return Mathf.Clamp01(Normalization01(x, min, max));
           
        }

        public static float Normalization01(float x, float min, float max)
        {
            var resu = max - min;
            if (resu == 0)
                return 0;

            return (x - min) / resu;

        }
    }
}
