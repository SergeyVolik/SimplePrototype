using System.Collections.Generic;
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
