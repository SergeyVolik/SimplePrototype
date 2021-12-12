using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace SerV112.UtilityAIEditor
{
    class AddPortNodeCommand : ModelCommand<IExtendableInputPortNode>
    {
        const string k_UndoStringSingular = "Add Port";

        readonly PortDirection m_PortDirection;
        readonly PortOrientation m_PortOrientation;

        public AddPortNodeCommand(params IExtendableInputPortNode[] nodes)
            : base(k_UndoStringSingular, k_UndoStringSingular, nodes)
        {
            //m_PortDirection = direction;
            //m_PortOrientation = orientation;
        }

        public static void DefaultHandler(GraphToolState state, AddPortNodeCommand command)
        {
            if (!command.Models.Any())
                return;

            state.PushUndo(command);

            using (var updater = state.GraphViewState.UpdateScope)
            {
                foreach (var nodeModel in command.Models)
                {
                    nodeModel.AddPort();
                    
                }

               updater.MarkChanged(command.Models);
            }
        }
    }
}
