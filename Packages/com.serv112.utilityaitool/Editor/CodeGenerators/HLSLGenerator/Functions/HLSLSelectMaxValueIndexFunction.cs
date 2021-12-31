using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    public class HLSLSelectMaxValueIndexFunction : HLSLFunction
    {

        int m_elements;
        HLSLMinusInfinity m_MinusInf;
        public HLSLSelectMaxValueIndexFunction(int elements, HLSLMinusInfinity minus_inf, StringBuilder decl, GPUPrecision pres) : base(decl, pres)
        {
            m_MinusInf = minus_inf;
            m_elements = elements;
            Name = $"SelectMaxValueIndex{elements}";
            
        }

        public string Execute(string x)
        {
            Declare();
            return $"{Name}({x})";
        }

        protected override string InternalDeclare()
        {
            var builder = new StringBuilder();

            builder.AppendLine($"int SelectMaxValueIndex{m_elements}({m_GPUPrecision} values[{m_elements}])");
            builder.AppendLine("{");
            builder.AppendLine($"    {m_GPUPrecision} max = {m_MinusInf.Get()};");
            builder.AppendLine("    int result = 0;");
            builder.AppendLine($"   for (int i = 0; i < {m_elements}; i++)");
            builder.AppendLine("    {");
            builder.AppendLine($"       if (max < values[i])");
            builder.AppendLine("        {");
            builder.AppendLine($"           max = values[i];");
            builder.AppendLine($"           result = i;");
            builder.AppendLine("        }");
            builder.AppendLine("    }");
            builder.AppendLine("    return result;");
            builder.AppendLine("}");

            return builder.ToString();
        }
    }
}
