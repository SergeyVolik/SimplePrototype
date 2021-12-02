using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;

namespace SerV112.UtilityAIEditor
{
    public class AIGraphWindow2 : GraphViewEditorWindow
    {
        [InitializeOnLoadMethod]
        static void RegisterTool()
        {
            ShortcutHelper.RegisterDefaultShortcuts<AIGraphWindow2>(AIStencil.toolName);
        }

        [MenuItem("GTF/Samples/AI Editor", false)]
        public static void ShowAIGraphWindow()
        {
            Debug.Log("ShowRecipeGraphWindow");
            FindOrCreateGraphWindow<AIGraphWindow2>();
        }

        protected override void OnEnable()
        {
            EditorToolName = "AI Editor";
            base.OnEnable();
        }

        /// <inheritdoc />
        protected override GraphToolState CreateInitialState()
        {
            var prefs = Preferences.CreatePreferences(EditorToolName);
            return new AIState(GUID, prefs);
        }

        protected override GraphView CreateGraphView()
        {
            return new AIGraphView(this, CommandDispatcher, EditorToolName);
        }

        protected override BlankPage CreateBlankPage()
        {
            var onboardingProviders = new List<OnboardingProvider>();
            onboardingProviders.Add(new AIOnboardingProvider());

            return new BlankPage(CommandDispatcher, onboardingProviders);
        }

        /// <inheritdoc />
        protected override bool CanHandleAssetType(IGraphAssetModel asset)
        {
            return asset is AIGraphAssetModel2;
        }
        protected override MainToolbar CreateMainToolbar()
        {
            return new AIGraphMainToolbar(CommandDispatcher, GraphView);
        }
    }
}
