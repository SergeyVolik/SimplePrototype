using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    public interface IExtendableInputPortNode
    {
        void RemovePort(PortOrientation orientation, PortDirection direction);
        void AddPort(PortOrientation orientation, PortDirection direction);
    }

}
