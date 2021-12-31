using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace SerV112.UtilityAIEditor
{
   

    public class SetValue01Command : ModelCommand<Value01NodeModel, float>
    {
        const string k_UndoStringSingular = "Set Value01";
        const string k_UndoStringPlural = "Set Values01";

        public SetValue01Command(float value, params Value01NodeModel[] nodes)
            : base(k_UndoStringSingular, k_UndoStringPlural, value, nodes)
        {
        }

        public static void DefaultHandler(GraphToolState state, SetValue01Command command)
        {
            state.PushUndo(command);

            using (var graphUpdater = state.GraphViewState.UpdateScope)
            {
                foreach (var nodeModel in command.Models)
                {
                    nodeModel.Value01 = command.Value;
                    graphUpdater.MarkChanged(nodeModel);
                }
            }
        }
    }
}
