using System;
using System.Text;
using UnityEngine;

namespace SerV112.UtilityAIEditor
{
    public class HLSLMinusInfinity : HLSLConstant, HLSLConstantGet
    {
        public HLSLMinusInfinity(StringBuilder decl, GPUPrecision pres) : base($"{pres.ToString()}", "minus_infinity", $"-3.402823466e+38", decl)
        {

        }
        public string Get()
        {
            Declare();

            return Name;
        }
    }
}
