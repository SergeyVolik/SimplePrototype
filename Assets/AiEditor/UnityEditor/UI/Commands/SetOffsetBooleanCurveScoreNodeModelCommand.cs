using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace SerV112.UtilityAIEditor
{
   

    public class SetOffsetBooleanCurveScoreNodeModelCommand : ModelCommand<BooleanCurveScoreNodeModel, float>
    {
        const string k_UndoStringSingular = "Set Group Actions Node Name";
        const string k_UndoStringPlural = "Set Group Actions Nodes Names";

        public SetOffsetBooleanCurveScoreNodeModelCommand(float value, params BooleanCurveScoreNodeModel[] nodes)
            : base(k_UndoStringSingular, k_UndoStringPlural, value, nodes)
        {
        }

        public static void DefaultHandler(GraphToolState state, SetOffsetBooleanCurveScoreNodeModelCommand command)
        {
            state.PushUndo(command);

            using (var graphUpdater = state.GraphViewState.UpdateScope)
            {
                foreach (var nodeModel in command.Models)
                {
                    if (command.Value < BooleanCurveScoreNodeModel.OffsetMax && command.Value > BooleanCurveScoreNodeModel.OffsetMin)
                    {

                        nodeModel.Offset = command.Value;

                    }
                    else if (command.Value > BooleanCurveScoreNodeModel.OffsetMax)
                    {
                        nodeModel.Offset = BooleanCurveScoreNodeModel.OffsetMax;
                    }
                    else
                    {
                        nodeModel.Offset = BooleanCurveScoreNodeModel.OffsetMin;
                    }
                    graphUpdater.MarkChanged(nodeModel);
                }
            }
        }
    }
}
