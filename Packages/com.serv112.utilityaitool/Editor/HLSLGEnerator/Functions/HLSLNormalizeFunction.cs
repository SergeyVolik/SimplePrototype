using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    public class HLSLNormalizeFunction : HLSLFunction
    {

        private const string k_Body =
@"float Normalize(float value, float min, float max)
{
    return clamp((value - min) / (max - min), 0, 1);
}";
        private const string k_Name = "Normalize";
        public HLSLNormalizeFunction(StringBuilder decl) : base(decl)
        {
            Name = k_Name;
        }

        public string Execute(string value, string min, string max)
        {
            Declare();
            return $"{Name}({value}, {min}, {max})";
        }

        protected override string InternalDeclare()
        {
            return k_Body;
        }
    }
}
