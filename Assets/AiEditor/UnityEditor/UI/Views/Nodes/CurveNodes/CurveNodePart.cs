using System.Collections.Generic;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SerV112.UtilityAIEditor
{


   

    public abstract class CurveNodePart : BaseModelUIPart
    {
        public static readonly string ussClassName = "curve-part";
        public static readonly string groupName = "group-name";
        public static readonly string namespaceName = "namespace-name";


        VisualElement PartContainer { get; set; }
        protected CurveViewElement m_CurveView;

        public override VisualElement Root => PartContainer;


        public CurveNodePart(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
            : base(name, model, ownerElement, parentClassName)
        {
            
        }

        protected virtual float StartX => 0f;
        protected virtual float EndX => 1f;
        protected override void BuildPartUI(VisualElement container)
        {



            if (!(m_Model is ICurveNodeModel))
                return;
          
            PartContainer = new VisualElement { name = PartName };
            PartContainer.style.maxHeight = new StyleLength(StyleKeyword.Auto);
            PartContainer.style.maxWidth = new StyleLength(StyleKeyword.Auto);
            PartContainer.style.width = 300;
            PartContainer.style.height = 300;
            PartContainer.AddToClassList(ussClassName);
            PartContainer.AddToClassList(m_ParentClassName.WithUssElement(PartName));

            m_CurveView = new CurveViewElement(StartX, EndX);
            UpdateCurveFromModel();

            PartContainer.Add(m_CurveView);

            container.Add(PartContainer);

        }



        protected override void PostBuildPartUI()
        {
            base.PostBuildPartUI();

            var stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/AiEditor/UnityEditor/UI/Stylesheets/GroupActionsPart.uss");
            if (stylesheet != null)
            {
                PartContainer.styleSheets.Add(stylesheet);

            }


        }


        protected abstract void UpdateCurveFromModel();
       
        protected override void UpdatePartFromModel()
        {
           
            UpdateCurveFromModel();
            m_CurveView.MarkDirtyRepaint();
        }
    }

   
}
