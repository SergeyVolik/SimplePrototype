using System.Collections.Generic;
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
    
    public static class CreateiProcessorNodeWithUGFReflection
    {
        public static IModelUI CreateNode(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, AIProcessorNodeModel model)
        {
            IModelUI ui = new AiProcessorNodeView();
            ui.SetupBuildAndUpdate(model, dispatcher, elementBuilder.View, elementBuilder.Context);
            return ui;
        }
    }
    class AiProcessorNodeView : CollapsibleInOutNode
    {
        public static readonly string paramContainerPartName = "parameter-container";



        protected override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            base.BuildContextualMenu(evt);

            if (!(Model is AIProcessorNodeModel verticalNodeModel))
                return;

            if (evt.menu.MenuItems().Count > 0)
                evt.menu.AppendSeparator();



            evt.menu.AppendAction("Input/Add Port", action =>
            {
                CommandDispatcher.Dispatch(new AddPortNodeCommand(verticalNodeModel));
            });

            evt.menu.AppendAction("Input/Remove Port", action =>
            {
                CommandDispatcher.Dispatch(new RemovePortNodeCommand(verticalNodeModel));
            }, a => verticalNodeModel.NumberOfInputPorts > 2 ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled);

        }
    }


 
}
