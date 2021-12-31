using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    public abstract class HLSLFunction : IHLSLDeclaration
    {
        public bool IsDeclared => m_IsDeclared;
        private bool m_IsDeclared;
        public string Name { get; protected set; }
        StringBuilder m_Delaration;
        protected GPUPrecision m_GPUPrecision;
        public HLSLFunction(StringBuilder delaration, GPUPrecision pres)
        {
            m_GPUPrecision = pres;
            m_Delaration = delaration;
        }
        public void Declare()
        {
            if (m_IsDeclared)
                return;

            m_IsDeclared = true;

            m_Delaration.AppendLine(InternalDeclare());
        }

        protected abstract string InternalDeclare();


    }
}
