using System.Collections.Generic;
using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    public interface IExtendableInputPortNode : INodeModel
    {
        IEnumerable<IGraphElementModel> RemovePort();
        void AddPort();
    }

}
