using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace SerV112.UtilityAIEditor
{
   

    public class SetOffsetCommand : ModelCommand<IOffsetable, float>
    {
        const string k_UndoStringSingular = "Set offset";
        const string k_UndoStringPlural = "Set offsets";

        public SetOffsetCommand(float value, params IOffsetable[] nodes)
            : base(k_UndoStringSingular, k_UndoStringPlural, value, nodes)
        {
        }

        public static void DefaultHandler(GraphToolState state, SetOffsetCommand command)
        {
            state.PushUndo(command);

            using (var graphUpdater = state.GraphViewState.UpdateScope)
            {
                foreach (var nodeModel in command.Models)
                {
                    if (command.Value < nodeModel.OffsetMax && command.Value > nodeModel.OffsetMin)
                    {

                        nodeModel.Offset = command.Value;

                    }
                    else if (command.Value > nodeModel.OffsetMax)
                    {
                        nodeModel.Offset = nodeModel.OffsetMax;
                    }
                    else
                    {
                        nodeModel.Offset = nodeModel.OffsetMin;
                    }
                    graphUpdater.MarkChanged(nodeModel);
                }
            }
        }
    }
}
