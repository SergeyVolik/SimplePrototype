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
    public static class CreateCodegenpNodeInspectorFieldsWithUGFReflection
    {
        public static IModelUI CreateActionGroupNodeInspector(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, CodeGenerationSettigsModel model)
        {
            var ui = UnityEditor.GraphToolsFoundation.Overdrive.ModelInspectorFactoryExtensions.CreateNodeInspector(elementBuilder, dispatcher, model);

            (ui as ModelUI)?.PartList.AppendPart(CodeGenNodeInspectorFields.Create("bake-node-fields", model, ui, ModelInspector.ussClassName));

            ui.BuildUI();
            ui.UpdateFromModel();

            return ui;
        }
    }
    class CodeGenNodeInspectorFields : FieldsInspector
    {
        public static CodeGenNodeInspectorFields Create(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
        {
            if (model is CodeGenerationSettigsModel)
            {
                return new CodeGenNodeInspectorFields(name, model, ownerElement, parentClassName);
            }

            return null;
        }

        CodeGenNodeInspectorFields(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
            : base(name, model, ownerElement, parentClassName) { }

        protected override IEnumerable<BaseModelPropertyField> GetFields()
        {
            if (m_Model is CodeGenerationSettigsModel bakeNodeModel)
            {
                yield return new ModelPropertyField<string>(
                    m_OwnerElement.CommandDispatcher,
                    bakeNodeModel,
                    nameof(CodeGenerationSettigsModel.Namespace),
                    nameof(CodeGenerationSettigsModel.Namespace),
                    typeof(SetNamespaceNameCommand));

               

            }
        }
    }
}
