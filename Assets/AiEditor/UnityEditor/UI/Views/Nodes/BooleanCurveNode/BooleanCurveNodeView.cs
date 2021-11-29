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
    
    public static class CreateBooleanCurveNodeWithUGFReflection
    {
        public static IModelUI CreateNode(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, BooleanCurveScoreNodeModel model)
        {
            IModelUI ui = new BooleanCurveNodeView();
            ui.SetupBuildAndUpdate(model, dispatcher, elementBuilder.View, elementBuilder.Context);
            return ui;
        }
    }

    class CurveNodeView : CollapsibleInOutNode
    {
        public static readonly string paramContainerPartName = "parameter-container1";

        protected override void BuildPartList()
        {
            base.BuildPartList();


            PartList.InsertPartAfter(titleIconContainerPartName, CurveNodePart.Create(paramContainerPartName, Model, this, ussClassName));


        }
        protected override void UpdateElementFromModel()
        {
            base.UpdateElementFromModel();

            bool collapsed = (NodeModel as ICollapsible)?.Collapsed ?? false;


            var paramsConteiner = this.SafeQ(paramContainerPartName);

            if (collapsed == false)
            {

                paramsConteiner.style.display = DisplayStyle.Flex;
            }
            else
            {

                paramsConteiner.style.display = DisplayStyle.None;
            }
        }

        protected override void PostBuildUI()
        {

            base.PostBuildUI();

        }


    }
    class BooleanCurveNodeView : CurveNodeView
    {


    }



}
