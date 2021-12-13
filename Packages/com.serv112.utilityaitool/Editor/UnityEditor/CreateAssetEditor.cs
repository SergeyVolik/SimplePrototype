using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;

namespace SerV112.UtilityAIEditor
{

    public static class CreateAssetEditor
    {

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
    }
}
