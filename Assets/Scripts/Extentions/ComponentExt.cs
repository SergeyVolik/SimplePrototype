using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public static class ComponentExt
    {

        public static bool TryGetCompontInParent<T>(this Component comp, out T value)
        {
            value = comp.GetComponentInParent<T>();

            if (value != null)
                return true;

            return false;
                
        }

        public static bool TryGetCompontInChildren<T>(this Component comp, out T value)
        {
            value = comp.GetComponentInChildren<T>();

            if (value != null)
                return true;

            return false;

        }

    }
}
