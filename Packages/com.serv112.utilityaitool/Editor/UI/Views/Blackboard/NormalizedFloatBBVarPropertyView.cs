using System;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEditor.MemoryProfiler;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;
using UnityEngine.GraphToolsFoundation.Overdrive;
using UnityEngine.UIElements;

namespace SerV112.UtilityAIEditor
{
   

    public class NormalizedFloatBBVarPropertyView : SingleFixBBVarPropertyView
    {
        protected override void BuildRows()
        {
            base.BuildRows();
            AddInitializationField2();

        }


        protected void AddInitializationField2()
        {
            VisualElement initializationElement = null;

            if (Model is IVariableDeclarationModel variableDeclarationModel)
            {
                if (variableDeclarationModel.InitializationModel == null)
                {
                    var stencil = (Stencil)CommandDispatcher.State.WindowState.GraphModel.Stencil;
                    if (stencil.RequiresInitialization(variableDeclarationModel))
                    {
                        initializationElement = new Button(OnInitializationButton) { text = "Create Init value" };
                    }
                }
                else
                {

                    initializationElement = BuildToTormalizeConstant(variableDeclarationModel.InitializationModel);

                }
            }

            if (initializationElement != null)
            {
                var row = MakeRow(initializationElement, rowInitValueUssClassName);
                Add(row);
            }
        }

        protected static VisualElement MakeRow(VisualElement control, string newRowUssClassName)
        {
            var row = new VisualElement { name = "blackboard-variable-property-view-row" };
            row.AddToClassList(BlackboardVariablePropertyView.rowUssClassName);
            if (!string.IsNullOrEmpty(newRowUssClassName))
                row.AddToClassList(newRowUssClassName);


            if (control != null)
            {
                control.AddToClassList(rowControlUssClassName);
                row.Add(control);
            }

            return row;
        }

        private Slider m_SliderField;
        private FloatField m_NormalizedField;
        private FloatField m_ValueField;
        private FloatField m_MaxField;
        private FloatField m_MinField;

        public VisualElement BuildToTormalizeConstant(IConstant @const)
        {
            var oldValue = @const as NormalizedFloatConstant;
            var val = oldValue.Value;
            var root = new VisualElement();
            root.AddToClassList(PropertyField.ussClassName);

            var visualEle = new VisualElement();
            //visualEle.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            m_ValueField = new FloatField("Value");
            m_ValueField.SetEnabled(false);
            visualEle.Add(m_ValueField);
            m_ValueField.SetValueWithoutNotify(val.Value);
            m_NormalizedField = new FloatField("Normalized");
            m_NormalizedField.SetEnabled(false);
            visualEle.Add(m_NormalizedField);
            m_NormalizedField.SetValueWithoutNotify(MathUtils.Normalization01WithClamp01(val.Value, val.Min, val.Max));
            m_SliderField = new Slider("Slider", val.Min, val.Max);
            m_SliderField.SetValueWithoutNotify(val.Value);

            m_SliderField.RegisterValueChangedCallback(evt => {             
               
                CommandDispatcher.Dispatch(new UpdateConstantToNormalizeValueCommand(oldValue, evt.newValue, UpdateToNormailzeField.Value));

                UpdateElementFromModel();
            });
            visualEle.Add(m_SliderField);
            m_MaxField = new FloatField("Max");

            m_MaxField.SetValueWithoutNotify(val.Max);
            m_MaxField.RegisterValueChangedCallback(evt => {
                CommandDispatcher.Dispatch(new UpdateConstantToNormalizeValueCommand(oldValue, evt.newValue, UpdateToNormailzeField.Max));
                UpdateElementFromModel();
            });

            m_MinField = new FloatField("Min");

            m_MinField.SetValueWithoutNotify(val.Min);

            m_MinField.RegisterValueChangedCallback(evt => {
                CommandDispatcher.Dispatch(new UpdateConstantToNormalizeValueCommand(oldValue, evt.newValue, UpdateToNormailzeField.Min));
                UpdateElementFromModel();
            });


            root.Add(visualEle);
            root.Add(m_MaxField);
            root.Add(m_MinField);

            return root;
        }


        protected override void UpdateElementFromModel()
        {
            base.UpdateElementFromModel();
            
            if (Model is IVariableDeclarationModel variableDeclarationModel)
            {
                if (variableDeclarationModel.InitializationModel is NormalizedFloatConstant @const)
                {
                    var value = @const.Value;
                   
                    m_SliderField.lowValue = value.Min;
                    m_SliderField.highValue = value.Max;
                    m_SliderField.value = value.Value;

                    m_MaxField?.SetValueWithoutNotify(value.Max);
                    m_MinField?.SetValueWithoutNotify(value.Min);


                    m_ValueField?.SetValueWithoutNotify(value.Value);
                    m_NormalizedField?.SetValueWithoutNotify(MathUtils.Normalization01WithClamp01(value.Value, value.Min, value.Max));

                }
            }


        }

    }

    [GraphElementsExtensionMethodsCache(typeof(AIGraphView))]
    public static class BindBlackboardFloatVarModelToViewWithReflection
    {

        public static IModelUI CreateFloatVariableDeclarationModelUI(this ElementBuilder elementBuilder, CommandDispatcher commandDispatcher, NormalizedFloatVariableDeclarationModel model)
        {
            IModelUI ui;

            if (elementBuilder.Context == BlackboardVariablePropertiesPart.blackboardVariablePropertiesPartCreationContext)
            {
                ui = new NormalizedFloatBBVarPropertyView();
                ui.SetupBuildAndUpdate(model, commandDispatcher, elementBuilder.View, elementBuilder.Context);

            }
            else
            {
                ui = UnityEditor.GraphToolsFoundation.Overdrive.GraphViewFactoryExtensions.CreateVariableDeclarationModelUI(elementBuilder, commandDispatcher, model);
            }

            return ui;
        }

    }

    //[GraphElementsExtensionMethodsCache(typeof(AIGraphView))]
    //public static class ConstantEditorExtensions
    //{

        

    //    public static VisualElement BuildDefaultConstantEditor(this IConstantEditorBuilder builder, ToNormalizeFloatConstant constant)
    //    {
    //        Debug.Log("BuildDefaultConstantEditor");

    //        return BuildToTormalizeConstant((ToNormalizeFloat)constant.ObjectValue, builder.OnValueChanged);
    //    }

       


    //}

}
