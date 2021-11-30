using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    public interface IExponential : IGraphElementModel
    {
        float Exponent { get; set; }
        float ExponentMax { get; }
        float ExponentMin { get; }
    }
}
