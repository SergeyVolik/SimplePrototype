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
    public static class CreateExponentialNodeFieldsWithUGFReflection
    {
        public static IModelUI CreateActionGroupNodeInspector(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, ExponentialCurveNodeModel model)
        {
            var ui = UnityEditor.GraphToolsFoundation.Overdrive.ModelInspectorFactoryExtensions.CreateNodeInspector(elementBuilder, dispatcher, model);

            (ui as ModelUI)?.PartList.AppendPart(ExponentialNodeInspectorFields.Create("bake-node-fields", model, ui, ModelInspector.ussClassName));

            ui.BuildUI();
            ui.UpdateFromModel();

            return ui;
        }
    }
    class ExponentialNodeInspectorFields : CurveNodeInspectorFields<ExponentialCurveNodeModel>
    {
        public static ExponentialNodeInspectorFields Create(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
        {
            if (model is ExponentialCurveNodeModel)
            {
                return new ExponentialNodeInspectorFields(name, model, ownerElement, parentClassName);
            }

            return null;
        }

        ExponentialNodeInspectorFields(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
            : base(name, model, ownerElement, parentClassName) { }

        protected override void AddFields()
        {
            base.AddFields();

            if (m_Model is ExponentialCurveNodeModel nodeModel)
            {

                //TODO: add command for set value to model
                Fields.Add(new ModelPropertyField<float>(
                   m_OwnerElement.CommandDispatcher,
                   nodeModel,
                   nameof(ExponentialCurveNodeModel.Exponent),
                   nameof(ExponentialCurveNodeModel.Exponent),
                   typeof(SetExponentCommand)));

                //TODO: add command for set value to model
                Fields.Add(new ModelPropertyField<float>(
                    m_OwnerElement.CommandDispatcher,
                    nodeModel,
                    nameof(ExponentialCurveNodeModel.OffsetY),
                    nameof(ExponentialCurveNodeModel.OffsetY),
                    typeof(SetOffsetYCommand)));



            }

           

        }


    }
}
