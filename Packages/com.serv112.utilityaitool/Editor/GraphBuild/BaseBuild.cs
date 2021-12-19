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
            string _CodeGenFolder;
            State = AIGraphBuidState.BeforeReimport;
            AssetPath = AssetDatabase.GetAssetPath(asset);
            var path = asset.GetDirectoryName();

            if (m_AssetModel.RootDirectory == null)
            {
                m_AssetModel.FolderGuid = GUID.Generate().ToString();
                _CodeGenFolder = $"_CodeGen_{m_AssetModel.FolderGuid}";
                AssetDatabase.CreateFolder(path, _CodeGenFolder);
                AssetDatabase.CreateFolder(pathWithScripts, "Editor");
            }
            else
            {
                _CodeGenFolder = $"_CodeGen_{m_AssetModel.FolderGuid}";
            }

          

            m_AssetModel.CodeGenGuid = GUID.Generate().ToString();
         
            pathWithScripts = string.Join("/", path, _CodeGenFolder);

            pathWithEditorScripts = string.Join("/", pathWithScripts, "Editor");

            m_AssetModel.GeneratedObjects.Clear();


           

        }

        public static void CollectAssetRefs(string[] importedAssets, BaseBuild build)
        {
            if (build == null)
                return;

            var m_AssetModel = build.Model;
            var pathWithScripts = build.pathWithScripts;

            if (build.State == AIGraphBuidState.AfterReimport && m_AssetModel)
            {

                m_AssetModel.RootDirectory = AssetDatabase.LoadAssetAtPath(pathWithScripts, typeof(UnityEngine.Object));

                foreach (string str in importedAssets)
                {
                    m_AssetModel.GeneratedObjects.Add(AssetDatabase.LoadAssetAtPath(str, typeof(UnityEngine.Object)));
                }

                build.State = AIGraphBuidState.AssetsSaved;
                EditorUtility.SetDirty(m_AssetModel);                
                AssetDatabase.SaveAssetIfDirty(m_AssetModel);

                build = null;
            }
        }

        public abstract void Build();
    }
}
