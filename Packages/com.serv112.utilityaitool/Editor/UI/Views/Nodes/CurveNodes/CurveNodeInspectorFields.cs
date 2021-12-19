﻿using System.Collections.Generic;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.UIElements;

namespace SerV112.UtilityAIEditor
{


    class CurveNodeInspectorFields<T> : FieldsInspector where T : CurveNodeModel
    {
        protected List<BaseModelPropertyField> Fields = new List<BaseModelPropertyField>();
        protected CurveNodeInspectorFields(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
            : base(name, model, ownerElement, parentClassName) 
        {
            AddFields();
        }

        protected virtual void AddFields()
        {
            var nodeModel = m_Model as T;

            Fields.Add(new ModelPropertyField<float>(
                m_OwnerElement.CommandDispatcher,
                nodeModel,
                nameof(CurveNodeModel.MaxNormalizationValue),
                nameof(CurveNodeModel.MaxNormalizationValue),
                typeof(SetNormalizeMaxValueCommand)));

            Fields.Add(new ModelPropertyField<float>(
                m_OwnerElement.CommandDispatcher,
                nodeModel,
                nameof(CurveNodeModel.MinNormalizationValue),
                nameof(CurveNodeModel.MinNormalizationValue),
                typeof(SetNormalizeMinValueCommand)));
        }

        protected override IEnumerable<BaseModelPropertyField> GetFields()
        {

            for (int i = 0; i < Fields.Count; i++)
            {
                yield return Fields[i];
            }


        }
    }
}
