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
    public static class CreateCosineNodeFieldsWithUGFReflection
    {
        public static IModelUI CreateActionGroupNodeInspector(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, CosineCurveNodeModel model)
        {
            var ui = UnityEditor.GraphToolsFoundation.Overdrive.ModelInspectorFactoryExtensions.CreateNodeInspector(elementBuilder, dispatcher, model);

            (ui as ModelUI)?.PartList.AppendPart(CosineNodeInspectorFields.Create("bake-node-fields", model, ui, ModelInspector.ussClassName));

            ui.BuildUI();
            ui.UpdateFromModel();

            return ui;
        }
    }
    class CosineNodeInspectorFields : CurveNodeInspectorFields<CosineCurveNodeModel>
    {
        public static CosineNodeInspectorFields Create(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
        {
            if (model is CosineCurveNodeModel)
            {
                return new CosineNodeInspectorFields(name, model, ownerElement, parentClassName);
            }

            return null;
        }

        CosineNodeInspectorFields(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
            : base(name, model, ownerElement, parentClassName) { }

        protected override void AddFields()
        {
            base.AddFields();

            if (m_Model is CosineCurveNodeModel nodeModel)
            {

                Fields.Add(new ModelPropertyField<float>(
                   m_OwnerElement.CommandDispatcher,
                   nodeModel,
                   nameof(CosineCurveNodeModel.Steepness),
                   nameof(CosineCurveNodeModel.Steepness),
                   typeof(SetStepnessCommand)));


                Fields.Add(new ModelPropertyField<float>(
                    m_OwnerElement.CommandDispatcher,
                    nodeModel,
                    nameof(CosineCurveNodeModel.OffsetY),
                    nameof(CosineCurveNodeModel.OffsetY),
                    typeof(SetOffsetYCommand)));


                Fields.Add(new ModelPropertyField<float>(
                    m_OwnerElement.CommandDispatcher,
                    nodeModel,
                    nameof(CosineCurveNodeModel.OffsetX),
                    nameof(CosineCurveNodeModel.OffsetX),
                    typeof(SetOffsetXCommand)));


            }



        }

    }
}
