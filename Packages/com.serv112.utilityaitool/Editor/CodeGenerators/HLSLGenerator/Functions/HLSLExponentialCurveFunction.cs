using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    public class HLSLExponentialCurveFunction : HLSLFunction
    {

        private const string k_Name = "ExponentialCurve";
        public HLSLExponentialCurveFunction(StringBuilder decl, GPUPrecision pres) : base(decl, pres)
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
            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"{m_GPUPrecision} ExponentialCurve({m_GPUPrecision} x, {m_GPUPrecision} exponent, {m_GPUPrecision} offset)");
            builder.AppendLine("{");
            builder.AppendLine("    return clamp(1 - ((1 - pow(x, exponent)) / 1) + offset, 0, 1);");
            builder.AppendLine("}");

            return builder.ToString();
        }
    }
}
