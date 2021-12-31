using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    public class HLSLLogisticCurveFunction : HLSLFunction
    {

        private const string k_Name = "LogisticCurve";
        HLSL_E m_E;
        public HLSLLogisticCurveFunction(StringBuilder decl, HLSL_E e, GPUPrecision pres) : base(decl, pres)
        {
            m_E = e;
            Name = k_Name;
           
        }

        public string Execute(string x, string k, string x0)
        {
            Declare();
            return $"{Name}({x}, {k}, {x0})";
        }

        protected override string InternalDeclare()
        {
            StringBuilder buildr = new StringBuilder();
            buildr.AppendLine($"{m_GPUPrecision} LogisticCurve({m_GPUPrecision} x, {m_GPUPrecision} k, {m_GPUPrecision} x0)");
            buildr.AppendLine("{");
            buildr.AppendLine($"    {m_GPUPrecision} expPow = -k * ((4 * ({m_E.Get()}) * (x - x0)) - (2 * {m_E.Get()}));");
            buildr.AppendLine($"    return clamp(1 / (1 + pow(abs({m_E.Get()}), expPow)), 0, 1);");
            buildr.AppendLine("}");
            return buildr.ToString();
        }
    }
}
