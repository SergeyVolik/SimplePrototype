
using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{

    public class AIBoolPref : BoolPref
    {
        protected AIBoolPref(int id, string name, string[] obsoleteNames = null)
            : base(id, name, obsoleteNames)
        {
        }

        public static readonly AIBoolPref TestBoolPref1 = new AIBoolPref(1000, nameof(TestBoolPref1));
        public static readonly AIBoolPref TestBoolPref2 = new AIBoolPref(1001, nameof(TestBoolPref2));
    }

    public class AIIntPref : IntPref
    {
        protected AIIntPref(int id, string name, string[] obsoleteNames = null)
            : base(id, name, obsoleteNames)
        {
        }

        public static readonly AIIntPref TestIntPref2 = new AIIntPref(1000, nameof(TestIntPref2));
    }

    public class AIStringPref : StringPref
    {
        protected AIStringPref(int id, string name, string[] obsoleteNames = null)
            : base(id, name, obsoleteNames)
        {
        }

        public static readonly AIStringPref TestStringPref = new AIStringPref(1000, nameof(TestStringPref));
    }
}
