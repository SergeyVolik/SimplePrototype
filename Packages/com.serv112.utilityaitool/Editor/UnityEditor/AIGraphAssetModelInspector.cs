using UnityEngine;
using UnityEditor;
using System.IO;

namespace SerV112.UtilityAIEditor
{
    [CustomEditor(typeof(AIGraphAssetModel))]
    [CanEditMultipleObjects]
    public class AIGraphAssetModelInspector : Editor
    {
        SerializedProperty m_GraphModel;
        SerializedProperty m_Namespace;
        SerializedProperty m_BuildMode;
        SerializedProperty m_CodeGenGuid; 
        SerializedProperty m_GeneratedObjects;
        SerializedProperty m_RootDirectory;
        
        void OnEnable()
        {
            m_GraphModel = serializedObject.FindProperty("m_GraphModel");
            m_Namespace = serializedObject.FindProperty("m_Namespace");
            m_BuildMode = serializedObject.FindProperty("m_BuildMode");
            m_CodeGenGuid = serializedObject.FindProperty("m_CodeGenGuid");
            m_GeneratedObjects = serializedObject.FindProperty("m_GeneratedObjects");
            m_RootDirectory = serializedObject.FindProperty("m_RootDirectory");

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            GUI.enabled = false;

            EditorGUILayout.PropertyField(m_GraphModel, true);

            EditorGUILayout.PropertyField(m_Namespace, true);         
            EditorGUILayout.PropertyField(m_BuildMode, true);
            EditorGUILayout.PropertyField(m_CodeGenGuid, true);
            EditorGUILayout.PropertyField(m_RootDirectory, true);
            GUI.enabled = true;

            if (m_RootDirectory.objectReferenceValue)
            {
                EditorGUILayout.HelpBox($"Be careful! Don't save assets in the _CodeGen_{m_CodeGenGuid.stringValue} folder. This folder is created on every graph build ", MessageType.Warning, true);
                if (GUILayout.Button($"Delete _CodeGen_{m_CodeGenGuid.stringValue} folder"))
                {
                    var rootFolder = AssetDatabase.GetAssetPath(m_RootDirectory.objectReferenceValue);
                    if (Directory.Exists(rootFolder))
                    {
                        Directory.Delete(rootFolder, true);
                        File.Delete($"{rootFolder}.meta");
                    }

                    m_RootDirectory.objectReferenceValue = null;
                    AssetDatabase.Refresh();
                    m_GeneratedObjects.arraySize = 0;
                }
            }
        
            GUI.enabled = false;
           

            EditorGUILayout.PropertyField(m_GeneratedObjects, true);
          
            GUI.enabled = true;

           
            serializedObject.ApplyModifiedProperties();
        }
    }
}
