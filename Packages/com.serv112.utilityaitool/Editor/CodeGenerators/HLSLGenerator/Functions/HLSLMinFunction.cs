using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    public class HLSLMinFunction : HLSLFunction
    {

        int m_elements;
        HLSlPlusInfinity m_PlusInf;
        public HLSLMinFunction(int elements, HLSlPlusInfinity plus_inf, StringBuilder decl, GPUPrecision pres) : base(decl, pres)
        {
            m_PlusInf = plus_inf;
            m_elements = elements;
            Name = $"Min_{elements}";
            
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
            builder.AppendLine($"    {m_GPUPrecision} min = {m_PlusInf.Get()};");
            builder.AppendLine($"   for (int i = 0; i < {m_elements}; i++)");
            builder.AppendLine("    {");
            builder.AppendLine($"       min = min(min, values[i]);");
            builder.AppendLine("    }");
            builder.AppendLine("    return min;");
            builder.AppendLine("}");

            return builder.ToString();
        }
    }
}
