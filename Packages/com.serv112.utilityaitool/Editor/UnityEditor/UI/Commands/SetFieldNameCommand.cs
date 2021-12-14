using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace SerV112.UtilityAIEditor
{
   

    public class SetFieldNameCommand : ModelCommand<IFieldName, string>
    {
        const string k_UndoStringSingular = "Set Group Actions Node Name";
        const string k_UndoStringPlural = "Set Group Actions Nodes Names";

        public SetFieldNameCommand(string value, params IFieldName[] nodes)
            : base(k_UndoStringSingular, k_UndoStringPlural, value, nodes)
        {
        }

        public static void DefaultHandler(GraphToolState state, SetFieldNameCommand command)
        {
            state.PushUndo(command);

            using (var graphUpdater = state.GraphViewState.UpdateScope)
            {
                foreach (var nodeModel in command.Models)
                {
                    nodeModel.FieldName = command.Value;

                    graphUpdater.MarkChanged(nodeModel);
                }
            }
        }
    }
}
