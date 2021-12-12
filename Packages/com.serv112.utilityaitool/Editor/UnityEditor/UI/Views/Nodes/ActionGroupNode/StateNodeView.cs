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
    
    public static class CreateStateNodeWithUGFReflection
    {
        public static IModelUI CreateNode(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, StateNodeModel model)
        {
            IModelUI ui = new StateNodeView();
            ui.SetupBuildAndUpdate(model, dispatcher, elementBuilder.View, elementBuilder.Context);
            return ui;
        }
    }
    class StateNodeView : CollapsibleInOutNode
    {
        protected override void UpdateElementFromModel()
        {
            base.UpdateElementFromModel();
   
            

        }
    }


 
}
