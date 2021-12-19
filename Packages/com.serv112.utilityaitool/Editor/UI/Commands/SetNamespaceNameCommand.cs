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

            var aiState = state as AIState;
            using (var graphUpdater = aiState.ToolSettingsState.UpdateScope)
            {
                foreach (var nodeModel in command.Models)
                {
                    nodeModel.Namespace = command.Value;
                    graphUpdater.MarkChangedNamespace(nodeModel.Namespace);

                }

               
            }


        }
    }
}
