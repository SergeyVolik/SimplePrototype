using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace SerV112.UtilityAIEditor
{
    public abstract class BaseBuild : IAIGraphCodeGenBuild
    {
        protected AIGraphAssetModel m_AssetModel;
        protected string pathWithScripts;
        protected string pathWithEditorScripts;
        protected AIGraphBuidState State;
        protected string AssetPath;
        public AIGraphAssetModel Model => m_AssetModel;

        public BaseBuild(AIGraphAssetModel asset)
        {
            m_AssetModel = asset;

            State = AIGraphBuidState.BeforeReimport;
            AssetPath = asset.GetFilename();
            var path = asset.GetDirectoryName();
            m_AssetModel.CodeGenGuid = GUID.Generate().ToString();
            var _CodeGenFolder = $"_CodeGen_{m_AssetModel.CodeGenGuid}";
            pathWithScripts = string.Join("/", path, _CodeGenFolder);

            pathWithEditorScripts = string.Join("/", pathWithScripts, "Editor");

            m_AssetModel.GeneratedObjects.Clear();

            if (m_AssetModel.RootDirectory != null)
            {
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(m_AssetModel.RootDirectory));
            }


            AssetDatabase.CreateFolder(path, _CodeGenFolder);
            AssetDatabase.CreateFolder(pathWithScripts, "Editor");
        }

        public static void CollectAssetRefs(string[] importedAssets, BaseBuild build)
        {
            if (build == null)
                return;

            var m_AssetModel = build.Model;
            var pathWithScripts = build.pathWithScripts;
            var pathWithEditorScripts = build.pathWithEditorScripts;

            if (build.State == AIGraphBuidState.AfterReimport && m_AssetModel)
            {

                m_AssetModel.RootDirectory = AssetDatabase.LoadAssetAtPath(pathWithScripts, typeof(UnityEngine.Object));
                m_AssetModel.GeneratedObjects.Add(m_AssetModel.RootDirectory);


                foreach (string str in importedAssets)
                {
                    m_AssetModel.GeneratedObjects.Add(AssetDatabase.LoadAssetAtPath(str, typeof(UnityEngine.Object)));
                }

                AssetDatabase.SaveAssetIfDirty(m_AssetModel);

                build = null;
            }
        }

        public abstract void Build();
    }
}
