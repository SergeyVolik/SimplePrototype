using System.Text;

namespace SerV112.UtilityAIEditor
{
    public class HLSL_PI : HLSLConstant, HLSLConstantGet
    {
        public HLSL_PI(StringBuilder decl) : base("float", "PI", "3.14159265359", decl)
        {

        }
        public string Get()
        {
            Declare();

            return Name;
        }
    }
}
