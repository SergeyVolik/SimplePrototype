using System;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    public class FloatBBVarPropertyView : SingleFixBBVarPropertyView
    {
       
        
    }

    [GraphElementsExtensionMethodsCache(typeof(AIGraphView))]
    public static class BindBlackboardFloatVarModelToViewWithReflection
    {

        public static IModelUI CreateFloatVariableDeclarationModelUI(this ElementBuilder elementBuilder, CommandDispatcher commandDispatcher, FloatVariableDeclarationModel model)
        {
            IModelUI ui;

            if (elementBuilder.Context == BlackboardVariablePropertiesPart.blackboardVariablePropertiesPartCreationContext)
            {
                ui = new FloatBBVarPropertyView();
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
