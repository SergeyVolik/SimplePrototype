using System.Collections.Generic;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.UIElements;

namespace SerV112.UtilityAIEditor
{

   
    public class StateGroupNamePart : BaseModelUIPart
    {
        public static readonly string ussClassName = "ge-sample-bake-node-part";
        public static readonly string groupName = "group-name";
        public static readonly string namespaceName = "namespace-name";

        public static StateGroupNamePart Create(string name, IGraphElementModel model, IModelUI modelUI, string parentClassName, bool? collapsed)
        {
            if (model is INodeModel)
            {
                return new StateGroupNamePart(name, model, modelUI, parentClassName, collapsed);
            }

            return null;
        }

        VisualElement PartContainer1 { get; set; }

        EditableLabel GroupName { get; set; }
        EditableLabel NamespaceName { get; set; }


        public override VisualElement Root => PartContainer1;

        private bool? Collapsed;
        StateGroupNamePart(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName, bool? collapsed)
            : base(name, model, ownerElement, parentClassName)
        {
            Collapsed = collapsed;
        }


        private (VisualElement ve, EditableLabel editLabel) CreateField(string name, string labelText, string editableName, EventCallback<ChangeEvent<string>> onChangeCallback)
        {
            var field = new VisualElement { name = name };

            field.style.flexDirection = FlexDirection.Row;
            var label = new Label(labelText);
            var editableLabel = new EditableLabel { name = editableName };
            
            field.Add(label);
            field.Add(editableLabel);
           
            return (field, editableLabel);
        }

        protected override void BuildPartUI(VisualElement container)
        {
            if (!(m_Model is StateGroupNodeModel))
                return;
          
            PartContainer1 = new VisualElement { name = PartName };
            PartContainer1.AddToClassList(ussClassName);
            PartContainer1.AddToClassList(m_ParentClassName.WithUssElement(PartName));

           
           

         

            var field1 = CreateField("field1", StateGroupNodeModel.InspectorLabelNameText, groupName, OnChangeGroupName);
            field1.editLabel.RegisterCallback<ChangeEvent<string>>(OnChangeGroupName);
            GroupName = field1.editLabel;

            var field2 = CreateField("field2", StateGroupNodeModel.InspectorLabelNamespaceText, namespaceName, OnChangeGroupName);
            field2.editLabel.RegisterCallback<ChangeEvent<string>>(OnChangeNamespaceName);
            NamespaceName = field2.editLabel;


            PartContainer1.Add(field1.ve);
            PartContainer1.Add(field2.ve);


            container.Add(PartContainer1);

            if (Collapsed == true)
                PartContainer1.style.display = DisplayStyle.None;
        }

        void OnChangeGroupName(ChangeEvent<string> evt)
        {
            if (!(m_Model is StateGroupNodeModel nodeModel))
                return;

            m_OwnerElement.CommandDispatcher.Dispatch(new SetStateGroupNameCommand(evt.newValue, nodeModel));
        }

        void OnChangeNamespaceName(ChangeEvent<string> evt)
        {
            if (!(m_Model is StateGroupNodeModel nodeModel))
                return;

            m_OwnerElement.CommandDispatcher.Dispatch(new SetNamespaceNameCommand(evt.newValue, nodeModel));
        }



        protected override void PostBuildPartUI()
        {
            base.PostBuildPartUI();

            var stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/AiEditor/UnityEditor/UI/Stylesheets/GroupActionsPart.uss");
            if (stylesheet != null)
            {
                PartContainer1.styleSheets.Add(stylesheet);

            }


        }

        protected override void UpdatePartFromModel()
        {
            if (!(m_Model is StateGroupNodeModel bakeNodeModel))
                return;
            
            GroupName?.SetValueWithoutNotify(bakeNodeModel.ActionGroupName);
            NamespaceName?.SetValueWithoutNotify(bakeNodeModel.FileNamespace);
        }
    }

   
}
