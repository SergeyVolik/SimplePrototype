using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    public class HLSLSmootherstepCurveFunction : HLSLFunction
    {

        private const string k_Body =
@"float SmootherstepCurve(float x)
{
    return clamp(x * x * x * (x * (6 * x - 15) + 10), 0, 1);
}";
        private const string k_Name = "SmootherstepCurve";
        public HLSLSmootherstepCurveFunction(StringBuilder decl) : base(decl)
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
