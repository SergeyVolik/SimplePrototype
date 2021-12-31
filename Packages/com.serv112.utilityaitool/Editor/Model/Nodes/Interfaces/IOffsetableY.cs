using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    public interface IOffsetableY : IGraphElementModel
    {
        float OffsetY { get; set; }
        float OffsetYMax { get; }
        float OffsetYMin { get; }
    }

    public interface IOffsetableX : IGraphElementModel
    {
        float OffsetX { get; set; }
        float OffsetXMax { get; }
        float OffsetXMin { get; }
    }
}
