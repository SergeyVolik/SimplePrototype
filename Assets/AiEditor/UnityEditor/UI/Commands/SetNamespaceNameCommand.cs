using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace SerV112.UtilityAIEditor
{
   

    public class SetNamespaceNameCommand : ModelCommand<INamespaceField, string>
    {
        const string k_UndoStringSingular = "Set Group Actions Node Name";
        const string k_UndoStringPlural = "Set Group Actions Nodes Names";

        public SetNamespaceNameCommand(string value, params INamespaceField[] nodes)
            : base(k_UndoStringSingular, k_UndoStringPlural, value, nodes)
        {
        }

        public static void DefaultHandler(GraphToolState state, SetNamespaceNameCommand command)
        {
            state.PushUndo(command);

            using (var graphUpdater = state.GraphViewState.UpdateScope)
            {
                foreach (var nodeModel in command.Models)
                {
                    nodeModel.Title = command.Value;
                    nodeModel.Namespace = command.Value;
                    
                    graphUpdater.MarkChanged(nodeModel);
                }
            }
        }
    }
}
