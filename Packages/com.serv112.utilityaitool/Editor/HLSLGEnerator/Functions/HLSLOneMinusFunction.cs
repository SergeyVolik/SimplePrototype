using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    public class HLSLOneMinusFunction : HLSLFunction
    {

        private const string k_Body =
@"float OneMinus(float value)
{
    return 1 - value;
}";
        private const string k_Name = "OneMinus";
        public HLSLOneMinusFunction(StringBuilder decl) : base(decl)
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
            return k_Body;
        }
    }
}
