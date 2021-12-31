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
        ECSJobSystem,
        MonoBehaviourJobSystem,
        MonoBehaviourGPU,
        ECSGPU

    }


    public class AIGraphAssetModel : GraphAssetModel, INamespaceField
    {
        [Header("AI Code Generation Data")]
        [SerializeField]
        private string m_Namespace = "";
        [SerializeField]
        private BuildMode m_BuildMode;
        [SerializeField]
        private GPUPrecision m_GPUPrecision;
        [SerializeField]
        private bool m_Debug = false;

        [SerializeField]
        private string m_CodeGenGuid;
        [SerializeField]
        private string m_FolderGuid;
        [SerializeReference]
        private UnityEngine.Object m_RootDirectory;
        [SerializeReference]
        private List<UnityEngine.Object> m_GeneratedObjects;

        [SerializeField]
        private string m_PrevGeneratedAiSimulationScript;

        [SerializeField]
        private string m_PrevGeneratedAiAgentScript;

        #region
        [SerializeField]
        public List<string> LastGeneratedEnumsNames;
        [SerializeField]
        public string LastGeneratedMonoAgent;
        [SerializeField]
        public string LastGeneratedMonoSimulation;
        [SerializeField]
        public string LastGeneratedHlsl;
        #endregion
        public string PrevGeneratedAiAgentScript { get => m_PrevGeneratedAiAgentScript; set => m_PrevGeneratedAiAgentScript = value; }

        public string PrevGeneratedAiSimulationScript { get => m_PrevGeneratedAiSimulationScript; set => m_PrevGeneratedAiSimulationScript = value; }
        public bool Debug { get => m_Debug; set => m_Debug = value; }
        public UnityEngine.Object RootDirectory { get => m_RootDirectory; set => m_RootDirectory = value; }
        public string CodeGenGuid { get => m_CodeGenGuid; set => m_CodeGenGuid = value; }
        public GPUPrecision GPUPrecision { get => m_GPUPrecision; set => m_GPUPrecision = value; }
        public string FolderGuid { get => m_FolderGuid; set => m_FolderGuid = value; }

        public List<UnityEngine.Object> GeneratedObjects { get => m_GeneratedObjects; set => m_GeneratedObjects = value; }
        public string Namespace { get => m_Namespace; set => m_Namespace = value; }
        public BuildMode BuildMode { get => m_BuildMode; set => m_BuildMode = value; }

      

        protected override Type GraphModelType => typeof(AIGraphModel);


    }
}
