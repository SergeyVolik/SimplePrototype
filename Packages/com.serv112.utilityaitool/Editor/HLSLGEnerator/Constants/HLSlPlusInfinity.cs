using System.Text;

namespace SerV112.UtilityAIEditor
{
    public class HLSlPlusInfinity : HLSLConstant, HLSLConstantGet
    {
        public HLSlPlusInfinity(StringBuilder decl) : base("float", "infinity", "(1. / 0.)", decl)
        {

        }
        public string Get()
        {
            Declare();

            return Name;
        }
    }
}
