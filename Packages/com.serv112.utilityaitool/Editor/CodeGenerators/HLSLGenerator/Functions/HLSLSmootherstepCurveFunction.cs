using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    public class HLSLSmootherstepCurveFunction : HLSLFunction
    {

        private const string k_Name = "SmootherstepCurve";
        public HLSLSmootherstepCurveFunction(StringBuilder decl, GPUPrecision pres) : base(decl, pres)
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
            var builder = new StringBuilder();
            builder.AppendLine($"{m_GPUPrecision} SmootherstepCurve({m_GPUPrecision} x)");
            builder.AppendLine("{");
            builder.AppendLine($"    return clamp(x * x * x * (x * (6 * x - 15) + 10), 0, 1);");
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}
