using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.UIElements;

namespace SerV112.UtilityAIEditor
{
    /// <summary>
    /// It's a class for binding view and model of node with UGF reflection
    /// </summary>
    [GraphElementsExtensionMethodsCache(typeof(AIGraphView))]
    public static class CreateStateGroupNodeWithUGFReflection
    {
        public static IModelUI CreateNode(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, StateGroupNodeModel model)
        {
            IModelUI ui = new StateGroupNodeView();
            ui.SetupBuildAndUpdate(model, dispatcher, elementBuilder.View, elementBuilder.Context);
            return ui;
        }
    }
    class StateGroupNodeView : CollapsibleInOutNode
    {
        public static readonly string paramContainerPartName = "parameter-container";


        protected override void BuildPartList()
        {
            base.BuildPartList();
            
        }

        protected override void PostBuildUI()
        {
            base.PostBuildUI();
        }

        protected override void UpdateElementFromModel()
        {
            base.UpdateElementFromModel();
        }

        private uint? lastGraphViewEpoch;
        private uint? lastSettingsEpoch;
        protected override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            base.BuildContextualMenu(evt);

            if (!(Model is StateGroupNodeModel stateGroupModel))
                return;

            if (evt.menu.MenuItems().Count > 0)
                evt.menu.AppendSeparator();



            evt.menu.AppendAction("Input/Add Vertical Port", action =>
            {
                CommandDispatcher.Dispatch(new AddPortNodeCommand(stateGroupModel));
            });

            evt.menu.AppendAction("Input/Remove Vertical Port", action =>
            {
                CommandDispatcher.Dispatch(new RemovePortNodeCommand(stateGroupModel));
            }, a => stateGroupModel.NumberOfInputPorts > 2 ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled);

            evt.menu.AppendAction("Execute", action =>
            {
                Debug.Log((Model as StateGroupNodeModel).Evaluate());
            }, a => DropdownMenuAction.Status.Normal);

            evt.menu.AppendAction("Show Code Preview", action =>
            {
                var state = CommandDispatcher?.State as AIState;
                var model = stateGroupModel;
                var path = string.Join("/", Application.temporaryCachePath, model.Name + ".cs");

                if (lastGraphViewEpoch != state.GraphViewState.CurrentVersion ||
                   !File.Exists(path) ||
                    lastSettingsEpoch != state.ToolSettingsState.CurrentVersion)
                {
                    lastGraphViewEpoch = state.GraphViewState.CurrentVersion;
                    lastSettingsEpoch = state.ToolSettingsState.CurrentVersion;

                    model.GenereteStateGroup(Application.temporaryCachePath);   
                    
                }

                var text = File.ReadAllText(path, encoding: System.Text.Encoding.UTF8);
                CodeViewWindow.OpenCodeViewWindow(text, model.Name);



            }, a => CommandDispatcher?.State?.GraphProcessingState?.Errors.Count == 0 ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled);

        }
    }


 
}
