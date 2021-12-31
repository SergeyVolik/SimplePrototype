using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    public class HLSLMultiplyFunction : HLSLFunction
    {

        int m_elements;
        public HLSLMultiplyFunction(int elements, StringBuilder decl, GPUPrecision pres) : base(decl, pres)
        {

            m_elements = elements;
            Name = $"Multiply01_{elements}";
            
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
            builder.AppendLine($"    {m_GPUPrecision} result = values[0];");
            builder.AppendLine($"    for(int i = 1; i < {m_elements}; i++)");
            builder.AppendLine("    {");
            builder.AppendLine($"        result = result * values[i];");
            builder.AppendLine("    }");
            builder.AppendLine($"    return result;");
            builder.AppendLine("}");

            return builder.ToString();
        }
    }
}
