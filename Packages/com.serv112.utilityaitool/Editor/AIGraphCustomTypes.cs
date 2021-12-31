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
        public float Min;
        public float Max;
    }


    [Serializable]
    public struct AIGroup
    {
    }
    [Serializable]
    public struct AIAction
    {
    }
    [Serializable]
    public struct AIBucket
    {
    }
    public class AIGraphCustomTypes
    {
        //public static TypeHandle NormalizedFloat { get; } = TypeHandleHelpers.GenerateCustomTypeHandle(typeof(NormalizedFloat), "NormalizedFloat");
        public static TypeHandle NormalizedFloat { get; } = TypeHandleHelpers.GenerateTypeHandle(typeof(NormalizedFloat));
        public static TypeHandle AIGroup { get; } = TypeHandleHelpers.GenerateCustomTypeHandle(typeof(AIGroup), "AIGroup");
        public static TypeHandle AIAction { get; } = TypeHandleHelpers.GenerateCustomTypeHandle(typeof(AIAction), "AIAction");
        public static TypeHandle AIBucket { get; } = TypeHandleHelpers.GenerateCustomTypeHandle(typeof(AIBucket), "AIBucket");
    }
}
