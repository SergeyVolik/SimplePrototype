using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    public class HLSLLinearCurveFunction : HLSLFunction
    {

        private const string k_Body =
@"float LinearCurve(float x, float slope, float offset)
{
    return clamp((x / slope) - offset, 0, 1);
}";
        private const string k_Name = "LinearCurve";
        public HLSLLinearCurveFunction(StringBuilder decl) : base(decl)
        {
            Name = k_Name;
        }

        public string Execute(string x, string slope, string offset)
        {
            Declare();
            return $"{Name}({x}, {slope}, {offset})";
        }

        protected override string InternalDeclare()
        {
            return k_Body;
        }
    }
}
