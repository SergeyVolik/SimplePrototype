using System;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;
using UnityEngine.UIElements;

namespace SerV112.UtilityAIEditor
{
   

    public class NamespaceStringBBVarPropertyView : SingleFixBBVarPropertyView
    {

    }

    [GraphElementsExtensionMethodsCache(typeof(AIGraphView))]
    public static class BindBlackboardStringVarModelToViewWithReflection
    {

        public static IModelUI CreateStringVariableDeclarationModelUI(this ElementBuilder elementBuilder, CommandDispatcher commandDispatcher, NamespaceVariableDeclarationModel model)
        {
            IModelUI ui;

            if (elementBuilder.Context == BlackboardVariablePropertiesPart.blackboardVariablePropertiesPartCreationContext)
            {
                ui = new NamespaceStringBBVarPropertyView();
                ui.SetupBuildAndUpdate(model, commandDispatcher, elementBuilder.View, elementBuilder.Context);

            }
            else
            {
                ui = UnityEditor.GraphToolsFoundation.Overdrive.GraphViewFactoryExtensions.CreateVariableDeclarationModelUI(elementBuilder, commandDispatcher, model);
            }

            return ui;
        }
    }

}
