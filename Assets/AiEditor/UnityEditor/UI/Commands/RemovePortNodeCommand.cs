using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace SerV112.UtilityAIEditor
{
    class RemovePortNodeCommand : ModelCommand<IExtendableInputPortNode>
    {
        const string k_UndoStringSingular = "Remove Port";

        readonly PortDirection m_PortDirection;
        readonly PortOrientation m_PortOrientation;

        public RemovePortNodeCommand(params IExtendableInputPortNode[] nodes)
            : base(k_UndoStringSingular, k_UndoStringSingular, nodes)
        {

        }

        public static void DefaultHandler(GraphToolState state, RemovePortNodeCommand command)
        {
            if (!command.Models.Any())
                return;

            state.PushUndo(command);

            using (var updater = state.GraphViewState.UpdateScope)
            {
                foreach (var nodeModel in command.Models)
                {

                    //state.GraphViewState.GraphModel.DeleteEdges(nodeModel.GetConnectedEdges().ToList());
                    var eges = nodeModel.RemovePort();
                    
                    var deletedModels = state.GraphViewState.GraphModel.DeleteElements(eges.ToList()).ToList();
                    
                    updater.MarkDeleted(deletedModels);
                }
                updater.MarkChanged(command.Models);
            }
        }
    }
}
