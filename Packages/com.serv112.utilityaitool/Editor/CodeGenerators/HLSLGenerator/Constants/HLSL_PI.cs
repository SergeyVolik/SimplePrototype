using System.Text;
using UnityEngine;

namespace SerV112.UtilityAIEditor
{
    public class HLSL_PI : HLSLConstant, HLSLConstantGet
    {
        private const string PI = "myPI";
        private const string PIValue = "3.14159265358979323846";
        public HLSL_PI(StringBuilder decl, GPUPrecision pres) : base($"{pres}", PI, PIValue, decl)
        {

        }
        public string Get()
        {
            Declare();

            return Name;
        }
    }
}
