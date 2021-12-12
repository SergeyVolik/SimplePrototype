using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace SerV112.UtilityAIEditor
{
   

    public class SetStepnessCommand : ModelCommand<ISteepnessable, float>
    {
        const string k_UndoStringSingular = "Set Group Actions Node Name";
        const string k_UndoStringPlural = "Set Group Actions Nodes Names";

        public SetStepnessCommand(float value, params ISteepnessable[] nodes)
            : base(k_UndoStringSingular, k_UndoStringPlural, value, nodes)
        {
        }

        public static void DefaultHandler(GraphToolState state, SetStepnessCommand command)
        {
            state.PushUndo(command);

            using (var graphUpdater = state.GraphViewState.UpdateScope)
            {
                foreach (var nodeModel in command.Models)
                {
                    if (command.Value < nodeModel.SteepnessMax && command.Value > nodeModel.SteepnessMin)
                    {

                        nodeModel.Steepness = command.Value;

                    }
                    else if (command.Value > nodeModel.SteepnessMax)
                    {
                        nodeModel.Steepness = nodeModel.SteepnessMax;
                    }
                    else
                    {
                        nodeModel.Steepness = nodeModel.SteepnessMin;
                    }
                    graphUpdater.MarkChanged(nodeModel);
                }
            }
        }
    }
}
