using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    public interface INormalizeable : IGraphElementModel
    {
        float MinNormalizationValue { get; set; }
        float MaxNormalizationValue { get; set; }
    }
}
