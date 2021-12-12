using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{

    public interface INameable : IGraphElementModel, IHasTitle
    {
        string Name { get; set; }
    }
}
