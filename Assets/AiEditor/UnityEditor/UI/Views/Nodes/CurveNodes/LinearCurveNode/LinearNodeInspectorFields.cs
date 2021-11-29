using System.Collections.Generic;
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
    public static class CreateLinearNodeFieldsWithUGFReflection
    {
        public static IModelUI CreateActionGroupNodeInspector(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, LinearCurveNodeModel model)
        {
            var ui = UnityEditor.GraphToolsFoundation.Overdrive.ModelInspectorFactoryExtensions.CreateNodeInspector(elementBuilder, dispatcher, model);

            (ui as ModelUI)?.PartList.AppendPart(LinearNodeInspectorFields.Create("bake-node-fields", model, ui, ModelInspector.ussClassName));

            ui.BuildUI();
            ui.UpdateFromModel();

            return ui;
        }
    }
    class LinearNodeInspectorFields : FieldsInspector
    {
        public static LinearNodeInspectorFields Create(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
        {
            if (model is LinearCurveNodeModel)
            {
                return new LinearNodeInspectorFields(name, model, ownerElement, parentClassName);
            }

            return null;
        }

        LinearNodeInspectorFields(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
            : base(name, model, ownerElement, parentClassName) { }

        protected override IEnumerable<BaseModelPropertyField> GetFields()
        {
            if (m_Model is LinearCurveNodeModel nodeModel)
            {

                //TODO: add command for set value to model
                yield return new ModelPropertyField<float>(
                   m_OwnerElement.CommandDispatcher,
                   nodeModel,
                   nameof(LinearCurveNodeModel.Slope),
                   nameof(LinearCurveNodeModel.Slope),
                   typeof(SetSlopeCommand));

                //TODO: add command for set value to model
                yield return new ModelPropertyField<float>(
                    m_OwnerElement.CommandDispatcher,
                    nodeModel,
                    nameof(LinearCurveNodeModel.Offset),
                    nameof(LinearCurveNodeModel.Offset),
                    typeof(SetOffsetCommand));



            }
        }
    }
}
