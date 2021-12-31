using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace SerV112.UtilityAIEditor
{
   

    public class SetOffsetXCommand : ModelCommand<IOffsetableX, float>
    {
        const string k_UndoStringSingular = "Set offset";
        const string k_UndoStringPlural = "Set offsets";

        public SetOffsetXCommand(float value, params IOffsetableX[] nodes)
            : base(k_UndoStringSingular, k_UndoStringPlural, value, nodes)
        {
        }

        public static void DefaultHandler(GraphToolState state, SetOffsetXCommand command)
        {
            state.PushUndo(command);

            using (var graphUpdater = state.GraphViewState.UpdateScope)
            {
                foreach (var nodeModel in command.Models)
                {
                    if (command.Value < nodeModel.OffsetXMax && command.Value > nodeModel.OffsetXMin)
                    {

                        nodeModel.OffsetX = command.Value;

                    }
                    else if (command.Value > nodeModel.OffsetXMax)
                    {
                        nodeModel.OffsetX = nodeModel.OffsetXMax;
                    }
                    else
                    {
                        nodeModel.OffsetX = nodeModel.OffsetXMin;
                    }
                    graphUpdater.MarkChanged(nodeModel);
                }
            }
        }
    }
}
