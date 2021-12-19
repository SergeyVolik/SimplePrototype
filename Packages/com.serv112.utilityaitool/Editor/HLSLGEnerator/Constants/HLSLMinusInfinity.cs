using System.Text;

namespace SerV112.UtilityAIEditor
{
    public class HLSLMinusInfinity : HLSLConstant, HLSLConstantGet
    {
        public HLSLMinusInfinity(StringBuilder decl) : base("float", "minus_infinity", "-(1. / 0.)", decl)
        {

        }
        public string Get()
        {
            Declare();

            return Name;
        }
    }
}
