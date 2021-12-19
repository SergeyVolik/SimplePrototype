using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    public class HLSLLogitCurveFunction : HLSLFunction
    {

        private const string k_Name = "LogitCurve";
        HLSLLogWithBaseFunction m_LogFunc;
        public HLSLLogitCurveFunction(HLSLLogWithBaseFunction log, StringBuilder decl) : base(decl)
        {
            m_LogFunc = log;
            Name = k_Name;
           
        }

        public string Execute(string x, string LogBase)
        {
            Declare();
            return $"{Name}({x}, {LogBase})";
        }

        protected override string InternalDeclare()
        {
            var strinBuilder = new StringBuilder();
            strinBuilder.AppendLine("float LogitCurve(float x, float logBase)");
            strinBuilder.AppendLine("{");
            strinBuilder.AppendLine($"   return clamp(({m_LogFunc.Execute("x / (1 - x)", "logBase")} + (2 * E)) / (4 * E), 0, 1);");
            strinBuilder.AppendLine("}");
            return strinBuilder.ToString();
        }
    }
}
