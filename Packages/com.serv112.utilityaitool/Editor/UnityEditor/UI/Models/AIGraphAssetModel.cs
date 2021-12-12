using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using static SerV112.UtilityAIEditor.ToolSettingsWindowStateComponent;

namespace SerV112.UtilityAIEditor
{

    public class AIGraphAssetModel : GraphAssetModel, INamespaceField
    {
        [SerializeField]
        private string m_Namespace = "";
        [SerializeField]
        private BuildMode m_BuildMode = BuildMode.MonoBehaviour;

        public string Namespace { get => m_Namespace; set => m_Namespace = value; }
        public BuildMode BuildMode { get => m_BuildMode; set => m_BuildMode = value; }

        [MenuItem("Assets/Create/AIAsset")]
        public static void CreateGraph(MenuCommand menuCommand)
        {


            string path = ProjectWindowUtilReflection.GetCurrentFolderPath();

            var template = new GraphTemplate<AIStencil>(AIStencil.graphName);
            CommandDispatcher commandDispatcher = null;
            if (EditorWindow.HasOpenInstances<AIGraphWindow>())
            {
                var window = EditorWindow.GetWindow<AIGraphWindow>();
                if (window != null)
                {
                    commandDispatcher = window.CommandDispatcher;
                }
            }

            GraphAssetCreationHelpers<AIGraphAssetModel>.CreateInProjectWindow(template, commandDispatcher, path);
        }

        [OnOpenAsset(1)]
        public static bool OpenGraphAsset(int instanceId, int line)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceId);
            if (obj is AIGraphAssetModel graphAssetModel)
            {
                var window = GraphViewEditorWindow.FindOrCreateGraphWindow<AIGraphWindow>();
                window.SetCurrentSelection(graphAssetModel, GraphViewEditorWindow.OpenMode.OpenAndFocus);
                return window != null;
            }

            return false;
        }

        protected override Type GraphModelType => typeof(AIGraphModel);


    }
}
