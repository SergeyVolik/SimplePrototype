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
    public static class CreateLogitNodeFieldsWithUGFReflection
    {
        public static IModelUI CreateActionGroupNodeInspector(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, LogitCurveNodeModel model)
        {
            var ui = UnityEditor.GraphToolsFoundation.Overdrive.ModelInspectorFactoryExtensions.CreateNodeInspector(elementBuilder, dispatcher, model);

            (ui as ModelUI)?.PartList.AppendPart(LogitNodeInspectorFields.Create("bake-node-fields", model, ui, ModelInspector.ussClassName));

            ui.BuildUI();
            ui.UpdateFromModel();

            return ui;
        }
    }
    class LogitNodeInspectorFields : CurveNodeInspectorFields<LogitCurveNodeModel>
    {
        public static LogitNodeInspectorFields Create(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
        {
            if (model is LogitCurveNodeModel)
            {
                return new LogitNodeInspectorFields(name, model, ownerElement, parentClassName);
            }

            return null;
        }

        LogitNodeInspectorFields(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
            : base(name, model, ownerElement, parentClassName) { }


        protected override void AddFields()
        {
            base.AddFields();

            if (m_Model is LogitCurveNodeModel nodeModel)
            {

                Fields.Add(new ModelPropertyField<float>(
                    m_OwnerElement.CommandDispatcher,
                    nodeModel,
                    nameof(LogitCurveNodeModel.LogBase),
                    nameof(LogitCurveNodeModel.LogBase),
                    typeof(SetLogBaseCommand)));


            }    

        }
    }
}
