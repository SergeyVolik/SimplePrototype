using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace SerV112.UtilityAIEditor
{
   

    public class SetLogBaseCommand : ModelCommand<ILogBase, float>
    {
        const string k_UndoStringSingular = "Set Slope";
        const string k_UndoStringPlural = "Set Slopes";

        public SetLogBaseCommand(float value, params ILogBase[] nodes)
            : base(k_UndoStringSingular, k_UndoStringPlural, value, nodes)
        {
        }

        public static void DefaultHandler(GraphToolState state, SetLogBaseCommand command)
        {
            state.PushUndo(command);

            using (var graphUpdater = state.GraphViewState.UpdateScope)
            {
                foreach (var nodeModel in command.Models)
                {
                    if (command.Value < nodeModel.LogBaseMax && command.Value > nodeModel.LogBaseMin)
                    {

                        nodeModel.LogBase = command.Value;

                    }
                    else if (command.Value > nodeModel.LogBaseMax)
                    {
                        nodeModel.LogBase = nodeModel.LogBaseMax;
                    }
                    else
                    {
                        nodeModel.LogBase = nodeModel.LogBaseMin;
                    }
                    graphUpdater.MarkChanged(nodeModel);
                }
            }
        }
    }
}
