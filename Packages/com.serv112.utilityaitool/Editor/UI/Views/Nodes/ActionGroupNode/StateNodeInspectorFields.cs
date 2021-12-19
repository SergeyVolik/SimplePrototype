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
    public static class CreateStateNodeInspectorFieldsWithUGFReflection
    {
        public static IModelUI CreateActionGroupNodeInspector(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, StateNodeModel model)
        {
            var ui = UnityEditor.GraphToolsFoundation.Overdrive.ModelInspectorFactoryExtensions.CreateNodeInspector(elementBuilder, dispatcher, model);

            (ui as ModelUI)?.PartList.AppendPart(StateNodeInspectorFields.Create("bake-node-fields", model, ui, ModelInspector.ussClassName));

            ui.BuildUI();
            ui.UpdateFromModel();

            return ui;
        }
    }
    class StateNodeInspectorFields : FieldsInspector
    {
        public static StateNodeInspectorFields Create(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
        {
            if (model is StateNodeModel)
            {
                return new StateNodeInspectorFields(name, model, ownerElement, parentClassName);
            }

            return null;
        }

        StateNodeInspectorFields(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
            : base(name, model, ownerElement, parentClassName) { }

        protected override IEnumerable<BaseModelPropertyField> GetFields()
        {
            if (m_Model is StateNodeModel nodeModel)
            {
                yield return new ModelPropertyField<string>(
                    m_OwnerElement.CommandDispatcher,
                    nodeModel,
                    nameof(StateNodeModel.Name),
                    nameof(StateNodeModel.Name),
                    typeof(SetNameCommand));



            }
        }
    }
}
