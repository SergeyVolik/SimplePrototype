﻿using System.Collections.Generic;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.UIElements;

namespace SerV112.UtilityAIEditor
{

    /// <summary>
    /// It's a class for binding view and model of node to insteptor window with UGF reflection
    /// </summary>
    [GraphElementsExtensionMethodsCache(typeof(ModelInspectorView))]
    public static class CreateSineNodeFieldsWithUGFReflection
    {
        public static IModelUI CreateActionGroupNodeInspector(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, SineCurveNodeModel model)
        {
            var ui = UnityEditor.GraphToolsFoundation.Overdrive.ModelInspectorFactoryExtensions.CreateNodeInspector(elementBuilder, dispatcher, model);

            (ui as ModelUI)?.PartList.AppendPart(SineNodeInspectorFields.Create("bake-node-fields", model, ui, ModelInspector.ussClassName));

            ui.BuildUI();
            ui.UpdateFromModel();

            return ui;
        }
    }
    class SineNodeInspectorFields : FieldsInspector
    {
        public static SineNodeInspectorFields Create(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
        {
            if (model is SineCurveNodeModel)
            {
                return new SineNodeInspectorFields(name, model, ownerElement, parentClassName);
            }

            return null;
        }

        SineNodeInspectorFields(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
            : base(name, model, ownerElement, parentClassName) { }

        protected override IEnumerable<BaseModelPropertyField> GetFields()
        {
            if (m_Model is SineCurveNodeModel nodeModel)
            {

                //TODO: add command for set value to model
                yield return new ModelPropertyField<float>(
                   m_OwnerElement.CommandDispatcher,
                   nodeModel,
                   nameof(SineCurveNodeModel.Steepness),
                   nameof(SineCurveNodeModel.Steepness),
                   typeof(SetStepnessCommand));

                //TODO: add command for set value to model
                yield return new ModelPropertyField<float>(
                    m_OwnerElement.CommandDispatcher,
                    nodeModel,
                    nameof(SineCurveNodeModel.Offset),
                    nameof(SineCurveNodeModel.Offset),
                    typeof(SetOffsetCommand));



            }
        }
    }
}
