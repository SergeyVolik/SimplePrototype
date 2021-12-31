using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;
using static SerV112.UtilityAIEditor.ToolSettingsWindowStateComponent;

namespace SerV112.UtilityAIEditor
{
   

    public class SetGPUPrecisionCommand : ModelCommand<AIGraphAssetModel, GPUPrecision>
    {
        const string k_UndoStringSingular = "Changed build model";
        const string k_UndoStringPlural = "Changed build model";

        public SetGPUPrecisionCommand(GPUPrecision value, params AIGraphAssetModel[] nodes)
            : base(k_UndoStringSingular, k_UndoStringPlural, value, nodes)
        {
        }

        public static void DefaultHandler(GraphToolState state, SetGPUPrecisionCommand command)
        {
            state.PushUndo(command);

            var aiState = state as AIState;
            using (var graphUpdater = aiState.ToolSettingsState.UpdateScope)
            {
                foreach (var nodeModel in command.Models)
                {
                    nodeModel.GPUPrecision = command.Value;
                    graphUpdater.MarkChangeGPUPrecision(nodeModel.GPUPrecision);
                }

               
            }
        }
    }
}
