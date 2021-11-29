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
        public static bool PointInBounds(Vector3 newPoint1, float thickness, Rect contentRect)
        {
            if (-thickness <= newPoint1.x && newPoint1.x <= contentRect.width + thickness && -thickness <= newPoint1.y && newPoint1.y <= contentRect.height + thickness)
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
    }
}
