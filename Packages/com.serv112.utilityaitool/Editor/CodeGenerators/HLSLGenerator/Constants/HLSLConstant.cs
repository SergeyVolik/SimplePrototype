using System.Text;

namespace SerV112.UtilityAIEditor
{
    public abstract class HLSLConstant : IHLSLDeclaration
    {
        public string Name => m_Name;

        public bool IsDeclared => m_IsDeclared;

        private string m_Name;
        private string m_Value;
        private string m_Type;
        private bool m_IsDeclared;
        StringBuilder m_DeclarationCode;
        public HLSLConstant(string type, string name, string value, StringBuilder devl) 
        {
            m_DeclarationCode = devl;
            m_Type = type;
            m_Name = name;
            m_Value = value;
        }

        public void Declare()
        {
            if (m_IsDeclared)
                return;

            m_IsDeclared = true;

            m_DeclarationCode.AppendLine($"const {m_Type} {m_Name} = {m_Value};");
        }
    }

    public interface HLSLConstantGet
    {
        string Get();
    }


   
}
