using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    [Serializable]
    public struct NormalizedFloat
    {
        public float Value;
    }
    public class AIGraphCustomTypes
    {
        public static TypeHandle Namespace { get; } = TypeHandleHelpers.GenerateTypeHandle(typeof(string));
        public static TypeHandle NormalizedFloat { get; } = TypeHandleHelpers.GenerateCustomTypeHandle(typeof(NormalizedFloat), "NormalizedFloat");
        public static TypeHandle AIAction { get; } = TypeHandleHelpers.GenerateCustomTypeHandle("AIAction");
    }
}
