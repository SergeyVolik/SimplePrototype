using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    public class HLSLLogWithBaseFunction : HLSLFunction
    {

        
        private const string k_Name = "logWithBase";
        public HLSLLogWithBaseFunction(StringBuilder decl, GPUPrecision pres) : base(decl, pres)
        {
         
            Name = k_Name;

        }

        public string Execute(string x, string @base)
        {
            Declare();
            return $"{Name}({@base}, {x})";
        }

        protected override string InternalDeclare()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"{m_GPUPrecision} logWithBase({m_GPUPrecision} base, {m_GPUPrecision} x)");
            builder.AppendLine("{");
            builder.AppendLine("    return log(x) / log(base);");
            builder.AppendLine("}");

            return builder.ToString();
        }
    }
}
