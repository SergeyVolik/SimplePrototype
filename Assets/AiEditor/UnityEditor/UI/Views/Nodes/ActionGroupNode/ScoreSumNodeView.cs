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
    
    public static class CreateSumScoreNodeWithUGFReflection
    {
        public static IModelUI CreateNode(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, ScoreSumNodeModel model)
        {
            IModelUI ui = new ScoreSumpNodeView();
            ui.SetupBuildAndUpdate(model, dispatcher, elementBuilder.View, elementBuilder.Context);
            return ui;
        }
    }
    class ScoreSumpNodeView : CollapsibleInOutNode
    {
        public static readonly string paramContainerPartName = "parameter-container";



        protected override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            base.BuildContextualMenu(evt);

            if (!(Model is ScoreSumNodeModel verticalNodeModel))
                return;

            if (evt.menu.MenuItems().Count > 0)
                evt.menu.AppendSeparator();

            

            evt.menu.AppendAction("Input/Add Port", action =>
            {
                CommandDispatcher.Dispatch(new AddPortNodeCommand<ScoreSumNodeModel>(PortDirection.Input, PortOrientation.Vertical, verticalNodeModel));
            });

            evt.menu.AppendAction("Input/Remove Port", action =>
            {
                CommandDispatcher.Dispatch(new RemovePortNodeCommand<ScoreSumNodeModel>(PortDirection.Input, PortOrientation.Vertical, verticalNodeModel));
            }, a => verticalNodeModel.ScoreInputCount > 2 ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled);

        }
    }


 
}
