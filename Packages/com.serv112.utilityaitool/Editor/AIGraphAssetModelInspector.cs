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
        SerializedProperty m_GPUPrecision;
        SerializedProperty m_CodeGenGuid; 
        SerializedProperty m_GeneratedObjects;
        SerializedProperty m_RootDirectory;
        SerializedProperty m_FolderGuid;
        SerializedProperty LastGeneratedEnumsNames;
        SerializedProperty LastGeneratedMonoAgent;
        SerializedProperty LastGeneratedMonoSimulation;
        SerializedProperty LastGeneratedHlsl;

        void OnEnable()
        {
            m_GraphModel = serializedObject.FindProperty("m_GraphModel");
            m_Namespace = serializedObject.FindProperty("m_Namespace");
            m_BuildMode = serializedObject.FindProperty("m_BuildMode");
            m_GPUPrecision = serializedObject.FindProperty("m_GPUPrecision");
            m_CodeGenGuid = serializedObject.FindProperty("m_CodeGenGuid");
            m_GeneratedObjects = serializedObject.FindProperty("m_GeneratedObjects");
            m_RootDirectory = serializedObject.FindProperty("m_RootDirectory");
            m_FolderGuid = serializedObject.FindProperty("m_FolderGuid");
            LastGeneratedEnumsNames = serializedObject.FindProperty("LastGeneratedEnumsNames");
            LastGeneratedMonoAgent = serializedObject.FindProperty("LastGeneratedMonoAgent");
            LastGeneratedMonoSimulation = serializedObject.FindProperty("LastGeneratedMonoSimulation");
            LastGeneratedHlsl = serializedObject.FindProperty("LastGeneratedHlsl");
        }

        public static void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                File.Delete(fileName);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                ProcessDirectory(subdirectory);
                Directory.Delete(subdirectory);
            }
        }


        public override void OnInspectorGUI()
        {
            var target = (AIGraphAssetModel)this.target;

            serializedObject.Update();
            GUI.enabled = false;

            EditorGUILayout.PropertyField(m_GraphModel, true);

            EditorGUILayout.PropertyField(m_Namespace, true);         
            EditorGUILayout.PropertyField(m_BuildMode, true);
            EditorGUILayout.PropertyField(m_GPUPrecision, true);           
            EditorGUILayout.PropertyField(m_CodeGenGuid, true);
            EditorGUILayout.PropertyField(m_RootDirectory, true);
            EditorGUILayout.PropertyField(m_FolderGuid, true);
            GUI.enabled = true;

            if (m_RootDirectory.objectReferenceValue)
            {
                EditorGUILayout.HelpBox($"Be careful! Don't save assets in the _CodeGen_{m_FolderGuid.stringValue} folder. This folder is created on every graph build ", MessageType.Warning, true);
                if (GUILayout.Button($"Delete _CodeGen_{m_FolderGuid.stringValue} folder"))
                {
                    var rootFolder = AssetDatabase.GetAssetPath(m_RootDirectory.objectReferenceValue);
                    if (Directory.Exists(rootFolder))
                    {
                       
                        Directory.Delete(rootFolder, true);
                        File.Delete($"{rootFolder}.meta");
                    }

                    //m_RootDirectory.objectReferenceValue = null;
                    target.RootDirectory = null;
                    target.GeneratedObjects.Clear();
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    
                }

                if (GUILayout.Button($"Clear _CodeGen_{m_FolderGuid.stringValue} folder"))
                {
                    var rootFolder = AssetDatabase.GetAssetPath(m_RootDirectory.objectReferenceValue);
                    if (Directory.Exists(rootFolder))
                    {
                        ProcessDirectory(rootFolder);
                    }

                    target.GeneratedObjects.Clear();
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    
                }
            }
        
            GUI.enabled = false;
           

            EditorGUILayout.PropertyField(m_GeneratedObjects, true);
            EditorGUILayout.PropertyField(LastGeneratedEnumsNames, true);
            EditorGUILayout.PropertyField(LastGeneratedMonoAgent, true);
            EditorGUILayout.PropertyField(LastGeneratedMonoSimulation, true);
            EditorGUILayout.PropertyField(LastGeneratedHlsl, true);

            GUI.enabled = true;

           
            serializedObject.ApplyModifiedProperties();
        }
    }
}
