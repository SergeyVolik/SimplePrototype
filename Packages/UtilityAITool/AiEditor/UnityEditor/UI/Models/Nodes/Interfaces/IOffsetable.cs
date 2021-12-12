using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    public interface IOffsetable : IGraphElementModel
    {
        float Offset { get; set; }
        float OffsetMax { get; }
        float OffsetMin { get; }
    }
}
