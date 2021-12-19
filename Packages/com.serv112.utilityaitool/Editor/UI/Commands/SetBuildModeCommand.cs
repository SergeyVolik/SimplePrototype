using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;
using static SerV112.UtilityAIEditor.ToolSettingsWindowStateComponent;

namespace SerV112.UtilityAIEditor
{
   

    public class SetBuildModeCommand : ModelCommand<AIGraphAssetModel, BuildMode>
    {
        const string k_UndoStringSingular = "Changed build model";
        const string k_UndoStringPlural = "Changed build model";

        public SetBuildModeCommand(BuildMode value, params AIGraphAssetModel[] nodes)
            : base(k_UndoStringSingular, k_UndoStringPlural, value, nodes)
        {
        }

        public static void DefaultHandler(GraphToolState state, SetBuildModeCommand command)
        {
            state.PushUndo(command);

            var aiState = state as AIState;
            using (var graphUpdater = aiState.ToolSettingsState.UpdateScope)
            {
                foreach (var nodeModel in command.Models)
                {
                    nodeModel.BuildMode = command.Value;
                    graphUpdater.MarkChangeBuildMode(nodeModel.BuildMode);
                }

               
            }
        }
    }
}
