using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace SerV112.UtilityAIEditor
{
   

    public class SetSlopeCommand : ModelCommand<ISlopeable, float>
    {
        const string k_UndoStringSingular = "Set Slope";
        const string k_UndoStringPlural = "Set Slopes";

        public SetSlopeCommand(float value, params ISlopeable[] nodes)
            : base(k_UndoStringSingular, k_UndoStringPlural, value, nodes)
        {
        }

        public static void DefaultHandler(GraphToolState state, SetSlopeCommand command)
        {
            state.PushUndo(command);

            using (var graphUpdater = state.GraphViewState.UpdateScope)
            {
                foreach (var nodeModel in command.Models)
                {
                    if (command.Value < nodeModel.SlopeMax && command.Value > nodeModel.SlopeMin)
                    {

                        nodeModel.Slope = command.Value;

                    }
                    else if (command.Value > nodeModel.SlopeMax)
                    {
                        nodeModel.Slope = nodeModel.SlopeMax;
                    }
                    else
                    {
                        nodeModel.Slope = nodeModel.SlopeMin;
                    }
                    graphUpdater.MarkChanged(nodeModel);
                }
            }
        }
    }
}
