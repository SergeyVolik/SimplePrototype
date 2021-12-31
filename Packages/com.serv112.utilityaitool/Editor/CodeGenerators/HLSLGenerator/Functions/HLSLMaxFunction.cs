using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    public class HLSLMaxFunction : HLSLFunction
    {

        int m_elements;
        HLSLMinusInfinity m_MinusInf;
        public HLSLMaxFunction(int elements, HLSLMinusInfinity minus_inf, StringBuilder decl, GPUPrecision pres) : base(decl, pres)
        {
            m_MinusInf = minus_inf;
            m_elements = elements;
            Name = $"Max_{elements}";
            
        }

        public string Execute(string x)
        {
            Declare();
            return $"{Name}({x})";
        }

        protected override string InternalDeclare()
        {
            var builder = new StringBuilder();

            builder.AppendLine($"{m_GPUPrecision} {Name}({m_GPUPrecision} values[{m_elements}])");
            builder.AppendLine("{");
            builder.AppendLine($"    {m_GPUPrecision} max = {m_MinusInf.Get()};");
            builder.AppendLine($"   for (int i = 0; i < {m_elements}; i++)");
            builder.AppendLine("    {");
            builder.AppendLine($"       max = max(max, values[i]);");
            builder.AppendLine("    }");
            builder.AppendLine("    return max;");
            builder.AppendLine("}");

            return builder.ToString();
        }
    }
}
