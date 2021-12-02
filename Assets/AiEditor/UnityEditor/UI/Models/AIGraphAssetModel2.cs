using System;

using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;

namespace SerV112.UtilityAIEditor
{
    public class AIGraphAssetModel2 : GraphAssetModel
    {
        [MenuItem("Assets/Create/AIAsset")]
        public static void CreateGraph(MenuCommand menuCommand)
        {
            const string path = "Assets";
            var template = new GraphTemplate<AIStencil>(AIStencil.graphName);
            CommandDispatcher commandDispatcher = null;
            if (EditorWindow.HasOpenInstances<AIGraphWindow2>())
            {
                var window = EditorWindow.GetWindow<AIGraphWindow2>();
                if (window != null)
                {
                    commandDispatcher = window.CommandDispatcher;
                }
            }

            GraphAssetCreationHelpers<AIGraphAssetModel2>.CreateInProjectWindow(template, commandDispatcher, path);
        }

        [OnOpenAsset(1)]
        public static bool OpenGraphAsset(int instanceId, int line)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceId);
            if (obj is AIGraphAssetModel2 graphAssetModel)
            {
                var window = GraphViewEditorWindow.FindOrCreateGraphWindow<AIGraphWindow2>();
                window.SetCurrentSelection(graphAssetModel, GraphViewEditorWindow.OpenMode.OpenAndFocus);
                return window != null;
            }

            return false;
        }

        protected override Type GraphModelType => typeof(AIGraphModel);
    }
}
