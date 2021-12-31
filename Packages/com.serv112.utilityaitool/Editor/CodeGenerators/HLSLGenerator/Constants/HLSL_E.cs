using System;
using System.Text;
using UnityEngine;

namespace SerV112.UtilityAIEditor
{
    public class HLSL_E : HLSLConstant, HLSLConstantGet
    {
        private const string E = "E";
        private const string EValue = "2.7182818284590451";
        public HLSL_E(StringBuilder decl, GPUPrecision pres) : base($"{pres}", E, EValue, decl) { }
        public string Get()
        {
            Declare();

            return Name;
        }
    }
}
