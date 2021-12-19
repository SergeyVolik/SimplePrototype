using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    public class HLSLLogWithBaseFunction : HLSLFunction
    {

        private const string k_Body =
@"float logWithBase(float base, float x)
{
    return log(x) / log(base);
}";
        private const string k_Name = "logWithBase";
        public HLSLLogWithBaseFunction(StringBuilder decl) : base(decl)
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
            return k_Body;
        }
    }
}
