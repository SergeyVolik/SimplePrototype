using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace SerV112.UtilityAIEditor
{
   

    public class SetNormalizeMinValueCommand : ModelCommand<INormalizeable, float>
    {
        const string k_UndoStringSingular = "Set NormalizeMax";
        const string k_UndoStringPlural = "Set NormalizeMaxes";

        public SetNormalizeMinValueCommand(float value, params INormalizeable[] nodes)
            : base(k_UndoStringSingular, k_UndoStringPlural, value, nodes)
        {
        }

        public static void DefaultHandler(GraphToolState state, SetNormalizeMinValueCommand command)
        {
            state.PushUndo(command);

            using (var graphUpdater = state.GraphViewState.UpdateScope)
            {
                foreach (var nodeModel in command.Models)
                {

                    if (nodeModel.MaxNormalizationValue > command.Value)
                    {
                        nodeModel.MinNormalizationValue = command.Value;
                    }
                    graphUpdater.MarkChanged(nodeModel);
                }
            }
        }
    }
}
