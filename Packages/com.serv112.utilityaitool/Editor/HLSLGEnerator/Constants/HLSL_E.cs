using System.Text;

namespace SerV112.UtilityAIEditor
{
    public class HLSL_E : HLSLConstant, HLSLConstantGet
    {
        public HLSL_E(StringBuilder decl) : base("float", "E", "2.7182818284590452353", decl)
        {

        }
        public string Get()
        {
            Declare();

            return Name;
        }
    }
}
