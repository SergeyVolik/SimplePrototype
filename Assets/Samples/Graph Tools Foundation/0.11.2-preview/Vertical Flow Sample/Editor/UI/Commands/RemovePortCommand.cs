using System.Linq;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace UnityEditor.GraphToolsFoundation.Overdrive.Samples.Vertical
{
    class RemovePortCommand : ModelCommand<VerticalNodeModel>
    {
        const string k_UndoStringSingular = "Remove Port";

        readonly PortDirection m_PortDirection;
        readonly PortOrientation m_PortOrientation;

        public RemovePortCommand(PortDirection direction, PortOrientation orientation, params VerticalNodeModel[] nodes)
            : base(k_UndoStringSingular, k_UndoStringSingular, nodes)
        {
            m_PortDirection = direction;
            m_PortOrientation = orientation;
        }

        public static void DefaultHandler(GraphToolState state, RemovePortCommand command)
        {
            if (!command.Models.Any() || command.m_PortDirection == PortDirection.None)
                return;

            state.PushUndo(command);

            using (var updater = state.GraphViewState.UpdateScope)
            {
                foreach (var nodeModel in command.Models)
                    nodeModel.RemovePort(command.m_PortOrientation, command.m_PortDirection);

                updater.MarkChanged(command.Models);
            }
        }
    }
}
