using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    public class HLSLNormalizeFunction : HLSLFunction
    {

        private const string k_Name = "Normalize";
        public HLSLNormalizeFunction(StringBuilder decl, GPUPrecision pres) : base(decl, pres)
        {
            Name = k_Name;
        }

        public string Execute(string value, string min, string max)
        {
            Declare();
            return $"{Name}({value}, {min}, {max})";
        }

        protected override string InternalDeclare()
        {
            var strinBuilder = new StringBuilder();
            strinBuilder.AppendLine($"{m_GPUPrecision} Normalize({m_GPUPrecision} value, {m_GPUPrecision} min, {m_GPUPrecision} max)");
            strinBuilder.AppendLine("{");
            strinBuilder.AppendLine($"   return clamp((value - min) / (max - min), 0, 1);");
            strinBuilder.AppendLine("}");
            return strinBuilder.ToString();
        }
    }
}
