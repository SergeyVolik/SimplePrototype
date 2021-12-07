using System.Collections.Generic;
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
    
    public static class CreateCodeGenNodeViewWithUGFReflection
    {
        public static IModelUI CreateNode(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, CodeGenerationSettigsModel model)
        {
            IModelUI ui = new CodeGenerationNodeView();
            ui.SetupBuildAndUpdate(model, dispatcher, elementBuilder.View, elementBuilder.Context);
            return ui;
        }
    }
    class CodeGenerationNodeView : CollapsibleInOutNode
    {
        protected override void UpdateElementFromModel()
        {
            base.UpdateElementFromModel();
   
            

        }
    }


 
}
