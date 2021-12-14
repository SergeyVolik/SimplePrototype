using System;
using System.Collections.Generic;

using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;

namespace SerV112.UtilityAIEditor
{
    public enum BuildMode
    {
        ECS,
        MonoBehaviour
    }

    [Serializable]
    public class AIGraphAsset
    {
        public UnityEngine.Object Object;
        public string ObjectPath;
    }
    public class AIGraphAssetModel : GraphAssetModel, INamespaceField
    {
        [Header("AI Code Generation Data")]
        [SerializeField]
        private string m_Namespace = "";
        [SerializeField]
        private BuildMode m_BuildMode = BuildMode.MonoBehaviour;
        [SerializeField]
        private string m_CodeGenGuid;

        [SerializeField]
        private UnityEngine.Object m_RootDirectory;


        public UnityEngine.Object RootDirectory { get => m_RootDirectory; set => m_RootDirectory = value; }
        [SerializeField]
        private List<AIGraphAsset> m_GeneratedObjects;

        public string CodeGenGuid { get => m_CodeGenGuid; set => m_CodeGenGuid = value; }

        public List<AIGraphAsset> GeneratedObjects { get => m_GeneratedObjects; set => m_GeneratedObjects = value; }
        public string Namespace { get => m_Namespace; set => m_Namespace = value; }
        public BuildMode BuildMode { get => m_BuildMode; set => m_BuildMode = value; }

      

        protected override Type GraphModelType => typeof(AIGraphModel);


    }
}
