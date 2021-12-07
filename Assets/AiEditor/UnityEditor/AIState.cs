using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;


namespace SerV112.UtilityAIEditor
{
    public class AIState : GraphToolState
    {
        /// <inheritdoc />
        public AIState(Hash128 graphViewEditorWindowGUID, Preferences preferences)
            : base(graphViewEditorWindowGUID, preferences)
        {
            this.SetInitialSearcherSize(SearcherService.Usage.k_CreateNode, new Vector2(375, 300), 2.0f);
        }

        /// <inheritdoc />
        public override void RegisterCommandHandlers(Dispatcher dispatcher)
        {
            base.RegisterCommandHandlers(dispatcher);

            if (!(dispatcher is CommandDispatcher commandDispatcher))
                return;

            commandDispatcher.RegisterCommandHandler<BuildAIEditorCommand>(BuildAIEditorCommand.DefaultHandler);

            commandDispatcher.RegisterCommandHandler<SetNamespaceNameCommand>(SetNamespaceNameCommand.DefaultHandler);
            commandDispatcher.RegisterCommandHandler<SetStateNameCommand>(SetStateNameCommand.DefaultHandler);

            
            commandDispatcher.RegisterCommandHandler<SetOffsetCommand>(SetOffsetCommand.DefaultHandler);
            commandDispatcher.RegisterCommandHandler<SetSlopeCommand>(SetSlopeCommand.DefaultHandler);
            commandDispatcher.RegisterCommandHandler<SetStepnessCommand>(SetStepnessCommand.DefaultHandler);
            commandDispatcher.RegisterCommandHandler<SetExponentCommand>(SetExponentCommand.DefaultHandler);
            commandDispatcher.RegisterCommandHandler<SetLogBaseCommand>(SetLogBaseCommand.DefaultHandler);

            commandDispatcher.RegisterCommandHandler<SetNormalizeMaxValueCommand>(SetNormalizeMaxValueCommand.DefaultHandler);
            commandDispatcher.RegisterCommandHandler<SetNormalizeMinValueCommand>(SetNormalizeMinValueCommand.DefaultHandler);

            commandDispatcher.RegisterCommandHandler<AddPortNodeCommand>(AddPortNodeCommand.DefaultHandler);
            commandDispatcher.RegisterCommandHandler<RemovePortNodeCommand>(RemovePortNodeCommand.DefaultHandler);
        }
    }

}

