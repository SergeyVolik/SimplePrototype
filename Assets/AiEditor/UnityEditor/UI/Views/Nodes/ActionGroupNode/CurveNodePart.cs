using System.Collections.Generic;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SerV112.UtilityAIEditor
{

    

    public class CurveNodePart : BaseModelUIPart
    {
        public static readonly string ussClassName = "curve-part";
        public static readonly string groupName = "group-name";
        public static readonly string namespaceName = "namespace-name";

        public static CurveNodePart Create(string name, IGraphElementModel model, IModelUI modelUI, string parentClassName)
        {
            if (model is INodeModel)
            {
                return new CurveNodePart(name, model, modelUI, parentClassName);
            }

            return null;
        }

        VisualElement PartContainer { get; set; }


        public override VisualElement Root => PartContainer;


        CurveNodePart(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
            : base(name, model, ownerElement, parentClassName)
        {

        }



        CurveViewElement m_CurveView; 
        protected override void BuildPartUI(VisualElement container)
        {
            if (!(m_Model is BooleanCurveScoreNodeModel model))
                return;
          
            PartContainer = new VisualElement { name = PartName };
            PartContainer.AddToClassList(ussClassName);
            PartContainer.AddToClassList(m_ParentClassName.WithUssElement(PartName));

            m_CurveView = new CurveViewElement(new LogisticCurve(model.Steepness, model.Offset));

            //var curveFiled = new CurveField();
            //var curve =  new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
            //curve.preWrapMode = WrapMode.Clamp;
            //curve.postWrapMode = WrapMode.Clamp;
            //curveFiled.value = curve;
            //curveFiled.AddToClassList(ussClassName);

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

        protected override void UpdatePartFromModel()
        {
            if (!(m_Model is BooleanCurveScoreNodeModel model))
                return;

            m_CurveView.Curve = new LogisticCurve(model.Steepness, model.Offset);
            m_CurveView.MarkDirtyRepaint();
        }
    }

   
}
