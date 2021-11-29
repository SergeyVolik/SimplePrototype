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
    public static class CreateBooleanScoreNodeFieldsWithUGFReflection
    {
        public static IModelUI CreateActionGroupNodeInspector(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, BooleanCurveScoreNodeModel model)
        {
            var ui = UnityEditor.GraphToolsFoundation.Overdrive.ModelInspectorFactoryExtensions.CreateNodeInspector(elementBuilder, dispatcher, model);

            (ui as ModelUI)?.PartList.AppendPart(BooleanScoreNodeInspectorFields.Create("bake-node-fields", model, ui, ModelInspector.ussClassName));

            ui.BuildUI();
            ui.UpdateFromModel();

            return ui;
        }
    }
    class BooleanScoreNodeInspectorFields : FieldsInspector
    {
        public static BooleanScoreNodeInspectorFields Create(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
        {
            if (model is BooleanCurveScoreNodeModel)
            {
                return new BooleanScoreNodeInspectorFields(name, model, ownerElement, parentClassName);
            }

            return null;
        }

        BooleanScoreNodeInspectorFields(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
            : base(name, model, ownerElement, parentClassName) { }

        protected override IEnumerable<BaseModelPropertyField> GetFields()
        {
            if (m_Model is BooleanCurveScoreNodeModel nodeModel)
            {


                yield return new ModelPropertyField<float>(
                   m_OwnerElement.CommandDispatcher,
                   nodeModel,
                   nameof(BooleanCurveScoreNodeModel.Steepness),
                   nameof(BooleanCurveScoreNodeModel.Steepness),
                   typeof(SetSlopeBooleanCurveScoreNodeModelCommand));

                yield return new ModelPropertyField<float>(
                    m_OwnerElement.CommandDispatcher,
                    nodeModel,
                    nameof(BooleanCurveScoreNodeModel.Offset),
                    nameof(BooleanCurveScoreNodeModel.Offset),
                    typeof(SetOffsetBooleanCurveScoreNodeModelCommand));



            }
        }
    }
}
