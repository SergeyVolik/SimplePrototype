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


        protected override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            base.BuildContextualMenu(evt);

            if (!(Model is StateGroupNodeModel verticalNodeModel))
                return;

            if (evt.menu.MenuItems().Count > 0)
                evt.menu.AppendSeparator();



            evt.menu.AppendAction("Input/Add Vertical Port", action =>
            {
                CommandDispatcher.Dispatch(new AddPortNodeCommand(verticalNodeModel));
            });

            evt.menu.AppendAction("Input/Remove Vertical Port", action =>
            {
                CommandDispatcher.Dispatch(new RemovePortNodeCommand(verticalNodeModel));
            }, a => verticalNodeModel.VerticalInputCount > 2 ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled);

        }
    }


 
}
