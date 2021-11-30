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
    
    public static class CreateSineCurveNodeWithUGFReflection
    {
        public static IModelUI CreateNode(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, SineCurveNodeModel model)
        {
            IModelUI ui = new SineCurveNodeView();
            ui.SetupBuildAndUpdate(model, dispatcher, elementBuilder.View, elementBuilder.Context);
            return ui;
        }
    }

    
    class SineCurveNodeView : CurveNodeView
    {
        protected override void BuildPartList()
        {
            base.BuildPartList();

            PartList.InsertPartAfter(titleIconContainerPartName, new SineCurveNodePart(paramContainerPartName, Model, this, ussClassName));


        }

    }



}
