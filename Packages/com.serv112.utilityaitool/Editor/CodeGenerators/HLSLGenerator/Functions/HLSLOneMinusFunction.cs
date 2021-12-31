using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    public class HLSLOneMinusFunction : HLSLFunction
    {

        private const string k_Name = "OneMinus";
        public HLSLOneMinusFunction(StringBuilder decl, GPUPrecision pres) : base(decl, pres)
        {
            Name = k_Name;
             
        }

        public string Execute(string value)
        {
            Declare();
            return $"{Name}({value})";
        }

        protected override string InternalDeclare()
        {
            var strinBuilder = new StringBuilder();
            strinBuilder.AppendLine($"{m_GPUPrecision} OneMinus({m_GPUPrecision} value)");
            strinBuilder.AppendLine("{");
            strinBuilder.AppendLine($"   return 1 - value;");
            strinBuilder.AppendLine("}");
            return strinBuilder.ToString();
        }
    }
}
