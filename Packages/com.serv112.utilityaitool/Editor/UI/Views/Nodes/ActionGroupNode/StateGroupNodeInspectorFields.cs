using System.Collections.Generic;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.UIElements;

namespace SerV112.UtilityAIEditor
{

    /// <summary>
    /// It's a class for binding view and model of node to insteptor window with UGF reflection
    /// </summary>
    [GraphElementsExtensionMethodsCache(typeof(ModelInspectorView))]
    public static class CreateStateGroupNodeInspectorFieldsWithUGFReflection
    {
        public static IModelUI CreateActionGroupNodeInspector(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, StateGroupNodeModel model)
        {
            var ui = UnityEditor.GraphToolsFoundation.Overdrive.ModelInspectorFactoryExtensions.CreateNodeInspector(elementBuilder, dispatcher, model);

            (ui as ModelUI)?.PartList.AppendPart(StateGroupNodeInspectorFields.Create("bake-node-fields", model, ui, ModelInspector.ussClassName));

            ui.BuildUI();
            ui.UpdateFromModel();

            return ui;
        }
    }
    class StateGroupNodeInspectorFields : FieldsInspector
    {
        public static StateGroupNodeInspectorFields Create(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
        {
            if (model is StateGroupNodeModel)
            {
                return new StateGroupNodeInspectorFields(name, model, ownerElement, parentClassName);
            }

            return null;
        }

        StateGroupNodeInspectorFields(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
            : base(name, model, ownerElement, parentClassName) { }

        protected override IEnumerable<BaseModelPropertyField> GetFields()
        {
            if (m_Model is StateGroupNodeModel bakeNodeModel)
            {
                yield return new ModelPropertyField<string>(
                    m_OwnerElement.CommandDispatcher,
                    bakeNodeModel,
                    nameof(StateGroupNodeModel.Name),
                    StateGroupNodeModel.InspectorLabelNameText,
                    typeof(SetNameCommand));

                yield return new ModelPropertyField<string>(
                   m_OwnerElement.CommandDispatcher,
                   bakeNodeModel,
                   nameof(StateGroupNodeModel.FieldName),
                   nameof(StateGroupNodeModel.FieldName),
                   typeof(SetFieldNameCommand));



            }
        }
    }
}
