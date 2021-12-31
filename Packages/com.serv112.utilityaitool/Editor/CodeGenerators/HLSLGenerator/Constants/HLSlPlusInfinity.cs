using System.Text;
using UnityEngine;

namespace SerV112.UtilityAIEditor
{
    public class HLSlPlusInfinity : HLSLConstant, HLSLConstantGet
    {
        public HLSlPlusInfinity(StringBuilder decl, GPUPrecision pres) : base($"{pres}", "infinity", $"3.402823466e+38", decl)
        {

        }
        public string Get()
        {
            Declare();

            return Name;
        }
    }
}
