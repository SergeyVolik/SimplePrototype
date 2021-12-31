using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SerV112.UtilityAIEditor
{
    /// <summary>
    /// It's a class for binding view and model of node with UGF reflection
    /// </summary>
    [GraphElementsExtensionMethodsCache(typeof(AIGraphView))]
    
    public static class CreateValue01NodeWithUGFReflection
    {
        public static IModelUI CreateNode(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, Value01NodeModel model)
        {
            IModelUI ui = new Value01NodeView();
            ui.SetupBuildAndUpdate(model, dispatcher, elementBuilder.View, elementBuilder.Context);
            return ui;
        }
    }
    class Value01NodeView : CollapsibleInOutNode
    {
        public static readonly string paramContainerPartName = "parameter-container";
        protected override void UpdateElementFromModel()
        {
            base.UpdateElementFromModel();
   
            

        }
        protected override void BuildPartList()
        {
            base.BuildPartList();

           PartList.InsertPartAfter(titleIconContainerPartName, Value01SliderPart.Create(paramContainerPartName, Model, this, ussClassName));
        }
    }

    public class Value01SliderPart : BaseModelUIPart
    {
        public static readonly string ussClassName = "ge-sample-bake-node-part";
        public static readonly string temperatureLabelName = "temperature";
        public static readonly string durationLabelName = "duration";

        public static Value01SliderPart Create(string name, IGraphElementModel model, IModelUI modelUI, string parentClassName)
        {
            if (model is INodeModel)
            {
                return new Value01SliderPart(name, model, modelUI, parentClassName);
            }

            return null;
        }

        VisualElement TemperatureAndTimeContainer { get; set; }
        Slider Slider { get; set; }
        FloatField m_NormalizedField { get; set; }

        public override VisualElement Root => TemperatureAndTimeContainer;

        Value01SliderPart(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
            : base(name, model, ownerElement, parentClassName)
        {
        }

        protected override void BuildPartUI(VisualElement container)
        {
            if (!(m_Model is Value01NodeModel))
                return;

            TemperatureAndTimeContainer = new VisualElement { name = PartName };
            TemperatureAndTimeContainer.AddToClassList(ussClassName);
            TemperatureAndTimeContainer.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            TemperatureAndTimeContainer.AddToClassList(m_ParentClassName.WithUssElement(PartName));
            TemperatureAndTimeContainer.style.marginLeft = 10;
            TemperatureAndTimeContainer.style.marginRight = 10;
            TemperatureAndTimeContainer.style.marginTop = 10;
            TemperatureAndTimeContainer.style.marginBottom = 10;
            Slider = new Slider(0, 1, SliderDirection.Horizontal);
            Slider.RegisterCallback<ChangeEvent<float>>(OnChangeValue);
            Slider.AddToClassList(ussClassName.WithUssElement("temperature"));
            Slider.AddToClassList(m_ParentClassName.WithUssElement("temperature"));
            Slider.style.width = 100;
           

            m_NormalizedField = new FloatField() { name = durationLabelName };
            m_NormalizedField.SetEnabled(false);
            m_NormalizedField.style.width = 50;
            TemperatureAndTimeContainer.Add(Slider);
            TemperatureAndTimeContainer.Add(m_NormalizedField);

           

            container.Add(TemperatureAndTimeContainer);
        }

        void OnChangeValue(ChangeEvent<float> evt)
        {
            if (!(m_Model is Value01NodeModel bakeNodeModel))
                return;

            m_NormalizedField.value = evt.newValue;
           
            m_OwnerElement.CommandDispatcher.Dispatch(new SetValue01Command(evt.newValue, bakeNodeModel));
        }



        protected override void PostBuildPartUI()
        {
            base.PostBuildPartUI();

            var stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Packages/com.unity.graphtools.foundation/Samples/Recipes/Editor/UI/Stylesheets/BakeNodePart.uss");
            if (stylesheet != null)
            {
                TemperatureAndTimeContainer.styleSheets.Add(stylesheet);
            }
        }

        protected override void UpdatePartFromModel()
        {
            if (!(m_Model is Value01NodeModel bakeNodeModel))
                return;

            Slider.SetValueWithoutNotify(bakeNodeModel.Value01);
            m_NormalizedField.SetValueWithoutNotify(bakeNodeModel.Value01);
        }
    }



}
