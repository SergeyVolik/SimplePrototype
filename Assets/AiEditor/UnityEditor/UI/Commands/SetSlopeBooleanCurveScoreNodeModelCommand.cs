using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace SerV112.UtilityAIEditor
{
   

    public class SetSlopeBooleanCurveScoreNodeModelCommand : ModelCommand<BooleanCurveScoreNodeModel, float>
    {
        const string k_UndoStringSingular = "Set Group Actions Node Name";
        const string k_UndoStringPlural = "Set Group Actions Nodes Names";

        public SetSlopeBooleanCurveScoreNodeModelCommand(float value, params BooleanCurveScoreNodeModel[] nodes)
            : base(k_UndoStringSingular, k_UndoStringPlural, value, nodes)
        {
        }

        public static void DefaultHandler(GraphToolState state, SetSlopeBooleanCurveScoreNodeModelCommand command)
        {
            state.PushUndo(command);

            using (var graphUpdater = state.GraphViewState.UpdateScope)
            {
                foreach (var nodeModel in command.Models)
                {
                    if (command.Value < BooleanCurveScoreNodeModel.SteepnessMax && command.Value > BooleanCurveScoreNodeModel.SteepnessMin)
                    {

                        nodeModel.Steepness = command.Value;

                    }
                    else if (command.Value > BooleanCurveScoreNodeModel.SteepnessMax)
                    {
                        nodeModel.Steepness = BooleanCurveScoreNodeModel.SteepnessMax;
                    }
                    else
                    {
                        nodeModel.Steepness = BooleanCurveScoreNodeModel.SteepnessMin;
                    }
                    graphUpdater.MarkChanged(nodeModel);
                }
            }
        }
    }
}
