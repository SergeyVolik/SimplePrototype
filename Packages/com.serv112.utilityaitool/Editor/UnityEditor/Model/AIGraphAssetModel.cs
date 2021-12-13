using System;
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

    public class AIGraphAssetModel : GraphAssetModel, INamespaceField
    {
        [SerializeField]
        private string m_Namespace = "";
        [SerializeField]
        private BuildMode m_BuildMode = BuildMode.MonoBehaviour;

        public string Namespace { get => m_Namespace; set => m_Namespace = value; }
        public BuildMode BuildMode { get => m_BuildMode; set => m_BuildMode = value; }

      

        protected override Type GraphModelType => typeof(AIGraphModel);


    }
}
