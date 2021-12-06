﻿using System.Collections.Generic;
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
    
    public static class CreateAverageNodeWithUGFReflection
    {
        public static IModelUI CreateNode(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, AverageNodeModel model)
        {
            IModelUI ui = new AverageNodeView();
            ui.SetupBuildAndUpdate(model, dispatcher, elementBuilder.View, elementBuilder.Context);
            return ui;
        }
    }
    class AverageNodeView : CollapsibleInOutNode
    {
        public static readonly string paramContainerPartName = "parameter-container";



        protected override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            base.BuildContextualMenu(evt);

            if (!(Model is AverageNodeModel verticalNodeModel))
                return;

            if (evt.menu.MenuItems().Count > 0)
                evt.menu.AppendSeparator();

            

            //evt.menu.AppendAction("Input/Add Port", action =>
            //{
            //    CommandDispatcher.Dispatch(new AddPortNodeCommand<ScoreSumNodeModel>(PortDirection.Input, PortOrientation.Vertical, verticalNodeModel));
            //});

            //evt.menu.AppendAction("Input/Remove Port", action =>
            //{
            //    CommandDispatcher.Dispatch(new RemovePortNodeCommand<ScoreSumNodeModel>(PortDirection.Input, PortOrientation.Vertical, verticalNodeModel));
            //}, a => verticalNodeModel.ScoreInputCount > 2 ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled);

        }
    }


 
}
