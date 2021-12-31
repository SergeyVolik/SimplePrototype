using System.Collections.Generic;
using System.Linq;
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
    public static class CreateLogisticNodeFieldsWithUGFReflection
    {
        public static IModelUI CreateActionGroupNodeInspector(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, LogisticCurveNodeModel model)
        {
            var ui = UnityEditor.GraphToolsFoundation.Overdrive.ModelInspectorFactoryExtensions.CreateNodeInspector(elementBuilder, dispatcher, model);

            (ui as ModelUI)?.PartList.AppendPart(LogisticNodeInspectorFields.Create("bake-node-fields", model, ui, ModelInspector.ussClassName));

            ui.BuildUI();
            ui.UpdateFromModel();

            return ui;
        }
    }
    class LogisticNodeInspectorFields : CurveNodeInspectorFields<LogisticCurveNodeModel>
    {
        public static LogisticNodeInspectorFields Create(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
        {
            if (model is LogisticCurveNodeModel)
            {
                return new LogisticNodeInspectorFields(name, model, ownerElement, parentClassName);
            }

            return null;
        }

        LogisticNodeInspectorFields(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
            : base(name, model, ownerElement, parentClassName) { }

        
         protected override void AddFields()
        {
            base.AddFields();

            if (m_Model is LogisticCurveNodeModel nodeModel)
            {

                Fields.Add(new ModelPropertyField<float>(
                   m_OwnerElement.CommandDispatcher,
                   nodeModel,
                   nameof(LogisticCurveNodeModel.Steepness),
                   nameof(LogisticCurveNodeModel.Steepness),
                   typeof(SetStepnessCommand)));


                Fields.Add(new ModelPropertyField<float>(
                    m_OwnerElement.CommandDispatcher,
                    nodeModel,
                    nameof(LogisticCurveNodeModel.OffsetX),
                    nameof(LogisticCurveNodeModel.OffsetX),
                    typeof(SetOffsetYCommand)));



            }

        }
        
    }
}
