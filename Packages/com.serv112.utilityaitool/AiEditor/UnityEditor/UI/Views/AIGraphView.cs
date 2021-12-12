
using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    public class AIGraphView : GraphView
    {
        public AIGraphView(GraphViewEditorWindow window, CommandDispatcher commandDispatcher, string graphViewName)
            : base(window, commandDispatcher, graphViewName) { }
    }
}
