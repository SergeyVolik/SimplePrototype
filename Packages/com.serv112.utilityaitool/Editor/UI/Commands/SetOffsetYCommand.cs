using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace SerV112.UtilityAIEditor
{
   

    public class SetOffsetYCommand : ModelCommand<IOffsetableY, float>
    {
        const string k_UndoStringSingular = "Set offset";
        const string k_UndoStringPlural = "Set offsets";

        public SetOffsetYCommand(float value, params IOffsetableY[] nodes)
            : base(k_UndoStringSingular, k_UndoStringPlural, value, nodes)
        {
        }

        public static void DefaultHandler(GraphToolState state, SetOffsetYCommand command)
        {
            state.PushUndo(command);

            using (var graphUpdater = state.GraphViewState.UpdateScope)
            {
                foreach (var nodeModel in command.Models)
                {
                    if (command.Value < nodeModel.OffsetYMax && command.Value > nodeModel.OffsetYMin)
                    {

                        nodeModel.OffsetY = command.Value;

                    }
                    else if (command.Value > nodeModel.OffsetYMax)
                    {
                        nodeModel.OffsetY = nodeModel.OffsetYMax;
                    }
                    else
                    {
                        nodeModel.OffsetY = nodeModel.OffsetYMin;
                    }
                    graphUpdater.MarkChanged(nodeModel);
                }
            }
        }
    }
}
