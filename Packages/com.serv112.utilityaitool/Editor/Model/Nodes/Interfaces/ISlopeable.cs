using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    public interface ISlopeable : IGraphElementModel
    {
        float Slope { get; set; }
        float SlopeMax { get; }
        float SlopeMin { get; }
    }
}
