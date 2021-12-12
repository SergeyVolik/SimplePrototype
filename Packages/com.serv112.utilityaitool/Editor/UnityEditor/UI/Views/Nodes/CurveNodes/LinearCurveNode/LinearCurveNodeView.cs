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
    
    public static class CreateLinearCurveNodeWithUGFReflection
    {
        public static IModelUI CreateNode(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, LinearCurveNodeModel model)
        {
            IModelUI ui = new LinearCurveNodeView();
            ui.SetupBuildAndUpdate(model, dispatcher, elementBuilder.View, elementBuilder.Context);
            return ui;
        }
    }

    
    class LinearCurveNodeView : CurveNodeView
    {
        protected override void BuildPartList()
        {
            base.BuildPartList();

            PartList.InsertPartAfter(titleIconContainerPartName, new LinearCurveNodePart(paramContainerPartName, Model, this, ussClassName));


        }

    }



}
