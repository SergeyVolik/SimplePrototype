using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace SerV112.UtilityAIEditor
{
   

    public class SetExponentCommand : ModelCommand<IExponential, float>
    {
        const string k_UndoStringSingular = "Set Slope";
        const string k_UndoStringPlural = "Set Slopes";

        public SetExponentCommand(float value, params IExponential[] nodes)
            : base(k_UndoStringSingular, k_UndoStringPlural, value, nodes)
        {
        }

        public static void DefaultHandler(GraphToolState state, SetExponentCommand command)
        {
            state.PushUndo(command);

            using (var graphUpdater = state.GraphViewState.UpdateScope)
            {
                foreach (var nodeModel in command.Models)
                {
                    if (command.Value < nodeModel.ExponentMax && command.Value > nodeModel.ExponentMin)
                    {

                        nodeModel.Exponent = command.Value;

                    }
                    else if (command.Value > nodeModel.ExponentMax)
                    {
                        nodeModel.Exponent = nodeModel.ExponentMax;
                    }
                    else
                    {
                        nodeModel.Exponent = nodeModel.ExponentMin;
                    }
                    graphUpdater.MarkChanged(nodeModel);
                }
            }
        }
    }
}
