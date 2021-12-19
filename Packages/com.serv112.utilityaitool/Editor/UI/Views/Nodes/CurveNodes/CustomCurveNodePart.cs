using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SerV112.UtilityAIEditor
{


   

    public class CustomCurveNodePart : BaseModelUIPart
    {
        public static readonly string ussClassName = "curve-part";
        public static readonly string groupName = "group-name";
        public static readonly string namespaceName = "namespace-name";


        VisualElement PartContainer { get; set; }
        protected CurveField m_CurveView;

        public override VisualElement Root => PartContainer;


        public CustomCurveNodePart(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
            : base(name, model, ownerElement, parentClassName)
        {

        }

        protected override void BuildPartUI(VisualElement container)
        {
            if (!(m_Model is CustomCurveNodeModel model))
                return;
          
            PartContainer = new VisualElement { name = PartName };
            PartContainer.AddToClassList(ussClassName);
            PartContainer.AddToClassList(m_ParentClassName.WithUssElement(PartName));

            m_CurveView = new CurveField();
            
            m_CurveView.RegisterValueChangedCallback((evt) => model.CustomCurve = evt.newValue);
            m_CurveView.AddToClassList("curve-part");

            PartContainer.Add(m_CurveView);

            container.Add(PartContainer);

        }



        protected override void PostBuildPartUI()
        {
            base.PostBuildPartUI();
            var path = string.Join("/", DirectoryUtils.DefaultPath, "Editor/UI/Stylesheets/GroupActionsPart.uss");
            var stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
            if (stylesheet != null)
            {
                PartContainer.styleSheets.Add(stylesheet);

            }


        }



        protected override void UpdatePartFromModel()
        {
            if (!(m_Model is CustomCurveNodeModel model))
                return;

            m_CurveView.SetValueWithoutNotify(model.CustomCurve);
        }
    }

   
}
