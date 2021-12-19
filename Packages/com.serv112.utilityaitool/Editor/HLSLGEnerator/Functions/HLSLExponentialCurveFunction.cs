using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    public class HLSLExponentialCurveFunction : HLSLFunction
    {

        private const string k_Body =
@"float ExponentialCurve(float x, float exponent, float offset)
{
    return clamp(1 - ((1 - pow(x, exponent)) / 1) + offset, 0, 1);
}";
        private const string k_Name = "ExponentialCurve";
        public HLSLExponentialCurveFunction(StringBuilder decl) : base(decl)
        {
            Name = k_Name;
        }

        public string Execute(string x, string exponent, string offset)
        {
            Declare();
            return $"{Name}({x}, {exponent}, {offset})";
        }

        protected override string InternalDeclare()
        {
            return k_Body;
        }
    }
}
