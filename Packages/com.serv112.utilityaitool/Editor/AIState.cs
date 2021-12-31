using System.Collections.Generic;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;


namespace SerV112.UtilityAIEditor
{
    public class AIState : GraphToolState
    {
        ToolSettingsWindowStateComponent m_ToolSettingsState;

        public ToolSettingsWindowStateComponent ToolSettingsState =>
           m_ToolSettingsState ??= PersistedState.GetOrCreateAssetStateComponent<ToolSettingsWindowStateComponent>(nameof(ToolSettingsState));


        public override IEnumerable<IStateComponent> AllStateComponents
        {
            get
            {
                yield return BlackboardViewState;
                yield return WindowState;
                yield return GraphViewState;
                yield return SelectionState;
                yield return TracingStatusState;
                yield return TracingControlState;
                yield return TracingDataState;
                yield return GraphProcessingState;
                yield return ModelInspectorState;
                yield return ToolSettingsState;
            }
        }

        protected override void ResetStateCaches()
        {
            base.ResetStateCaches();
            m_ToolSettingsState = null;
        }
        /// <inheritdoc />
        public AIState(Hash128 graphViewEditorWindowGUID, Preferences preferences)
            : base(graphViewEditorWindowGUID, preferences)
        {
            this.SetInitialSearcherSize(SearcherService.Usage.k_CreateNode, new Vector2(375, 300), 2.0f);
        }

        protected override void SerializeForUndo(SerializedReferenceDictionary<string, string> stateComponentUndoData)
        {
            base.SerializeForUndo(stateComponentUndoData);

            stateComponentUndoData.Add(nameof(ToolSettingsState), StateComponentHelper.Serialize(ToolSettingsState));
        }

        /// <inheritdoc />
        protected override void DeserializeFromUndo(SerializedReferenceDictionary<string, string> stateComponentUndoData)
        {

            base.DeserializeFromUndo(stateComponentUndoData);



            if (stateComponentUndoData.TryGetValue(nameof(ToolSettingsState), out var serializedData))
            {
                var newWindowState = StateComponentHelper.Deserialize<ToolSettingsWindowStateComponent>(serializedData);
                PersistedState.SetAssetStateComponent(newWindowState);
            }
        }

        /// <inheritdoc />
        public override void RegisterCommandHandlers(Dispatcher dispatcher)
        {
            base.RegisterCommandHandlers(dispatcher);

            if (!(dispatcher is CommandDispatcher commandDispatcher))
                return;

            commandDispatcher.RegisterCommandHandler<BuildAIEditorCommand>(BuildAIEditorCommand.DefaultHandler);

            commandDispatcher.RegisterCommandHandler<SetNamespaceNameCommand>(SetNamespaceNameCommand.DefaultHandler);
            commandDispatcher.RegisterCommandHandler<SetNameCommand>(SetNameCommand.DefaultHandler);
            commandDispatcher.RegisterCommandHandler<SetFieldNameCommand>(SetFieldNameCommand.DefaultHandler);

            commandDispatcher.RegisterCommandHandler<SetOffsetYCommand>(SetOffsetYCommand.DefaultHandler);
            commandDispatcher.RegisterCommandHandler<SetSlopeCommand>(SetSlopeCommand.DefaultHandler);
            commandDispatcher.RegisterCommandHandler<SetStepnessCommand>(SetStepnessCommand.DefaultHandler);
            commandDispatcher.RegisterCommandHandler<SetExponentCommand>(SetExponentCommand.DefaultHandler);
            commandDispatcher.RegisterCommandHandler<SetLogBaseCommand>(SetLogBaseCommand.DefaultHandler);

            commandDispatcher.RegisterCommandHandler<AddPortNodeCommand>(AddPortNodeCommand.DefaultHandler);
            commandDispatcher.RegisterCommandHandler<RemovePortNodeCommand>(RemovePortNodeCommand.DefaultHandler);
            commandDispatcher.RegisterCommandHandler<SetBuildModeCommand>(SetBuildModeCommand.DefaultHandler);
            commandDispatcher.RegisterCommandHandler<SetGPUPrecisionCommand>(SetGPUPrecisionCommand.DefaultHandler);
            commandDispatcher.RegisterCommandHandler<UpdateConstantToNormalizeValueCommand>(UpdateConstantToNormalizeValueCommand.DefaultHandler);

            commandDispatcher.RegisterCommandHandler<SetDebugCommand>(SetDebugCommand.DefaultHandler);
            commandDispatcher.RegisterCommandHandler<SetOffsetXCommand>(SetOffsetXCommand.DefaultHandler);
            commandDispatcher.RegisterCommandHandler<SetValue01Command>(SetValue01Command.DefaultHandler);

        }
    }

}

