using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{

    public interface INamespaceField : IGraphElementModel, IHasTitle
    {
        string Namespace { get; set; }
    }
}
