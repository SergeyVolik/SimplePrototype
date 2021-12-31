using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    public class HLSLAverageFunction : HLSLFunction
    {

        int m_elements;
        public HLSLAverageFunction(int elements, StringBuilder decl, GPUPrecision pres) : base(decl, pres)
        {

            m_elements = elements;
            Name = $"Average{elements}";
            
        }

        public string Execute(string x)
        {
            Declare();
            return $"{Name}({x})";
        }

        protected override string InternalDeclare()
        {
            var builder = new StringBuilder();

            builder.AppendLine($"{m_GPUPrecision} Average{m_elements}({m_GPUPrecision} values[{m_elements}])");
            builder.AppendLine("{");
            builder.AppendLine($"   {m_GPUPrecision} average = 0;");
            builder.AppendLine($"   for(int i = 0; i < {m_elements}; i++)");
            builder.AppendLine("    {");
            builder.AppendLine($"        average = average + values[i];");
            builder.AppendLine("    }");
            builder.AppendLine($"   return average / {m_elements};");
            builder.AppendLine("}");

            return builder.ToString();
        }
    }
}
