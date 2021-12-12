using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    public interface ILogBase : IGraphElementModel
    {
        float LogBase { get; set; }
        float LogBaseMax { get; }
        float LogBaseMin { get; }
    }
}
