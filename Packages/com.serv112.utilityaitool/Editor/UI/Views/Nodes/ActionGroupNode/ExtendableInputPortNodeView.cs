using System.Collections.Generic;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.UIElements;

namespace SerV112.UtilityAIEditor
{
    abstract class ExtendableInputPortNodeView : CollapsibleInOutNode
    {

        protected int m_MinNumberOfPorts = 2;

        protected override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            base.BuildContextualMenu(evt);

            if (!(Model is ExtendableInputPortNode verticalNodeModel))
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
            }, a => verticalNodeModel.NumberOfInputPorts > m_MinNumberOfPorts ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled);

        }
    }


 
}
