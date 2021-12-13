using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    public interface ISteepnessable : IGraphElementModel
    {
        float Steepness { get; set; }
        float SteepnessMax { get; }
        float SteepnessMin { get; }
    }
}
