using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    public class HLSLSmoothstepCurveFunction : HLSLFunction
    {

        private const string k_Body =
@"float SmoothstepCurve(float x)
{
    return clamp(x * x * (3 - 2 * x), 0, 1);
}";
        private const string k_Name = "SmoothstepCurve";
        public HLSLSmoothstepCurveFunction(StringBuilder decl) : base(decl)
        {
            Name = k_Name;
        }

        public string Execute(string x)
        {
            Declare();
            return $"{Name}({x})";
        }

        protected override string InternalDeclare()
        {
            return k_Body;
        }
    }
}
