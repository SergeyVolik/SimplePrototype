using System;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;
using UnityEngine.UIElements;

namespace SerV112.UtilityAIEditor
{
    public abstract class SingleFixBBVarPropertyView : BlackboardVariablePropertyView
    {
        private const string fieldClassName = "unity-base-field";
        private const string inputFieldName = "unity-text-input";
        private const string fixClassName = "fix-black-board-single-field";

        protected bool ShowExposedCheckBox = true;
        protected override void PostBuildUI()
        {
            var stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(AiEditorStylesPaths.BlackBoardSingleValueFix);
            if (stylesheet != null)
            {
                this.styleSheets.Add(stylesheet);

            }

            base.PostBuildUI();
        }
        protected override void BuildRows()
        {


            AddTooltipField();
            if (ShowExposedCheckBox)
            {
                AddExposedToggle();
            }
        }

    }

}
