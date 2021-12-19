using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace SerV112.UtilityAIEditor
{
    class AiGraphAssetModificationProcessor : UnityEditor.AssetModificationProcessor
    {
        const string titleDelete = "AI Asset Delete";
        const string messageDelete = "Do you want to delete AI Graph asset? If you do it, folder _CodeGen will be deleted too!";
        const string ok = "Ok";
        const string cancel = "Cancel";
        static AssetDeleteResult OnWillDeleteAsset(string path, RemoveAssetOptions opt)
        {
            var AiGraph = AssetDatabase.LoadAssetAtPath<AIGraphAssetModel>(path);

            var result = AssetDeleteResult.DidNotDelete;

            if (AiGraph)
            {

                if (EditorUtility.DisplayDialog(titleDelete, messageDelete, ok, cancel))
                {
                    try
                    {
                        if (AiGraph.RootDirectory)
                        {
                            var rootFolder = AssetDatabase.GetAssetPath(AiGraph.RootDirectory);
                            var filemeta = $"{rootFolder}.meta";
                            if (Directory.Exists(rootFolder))
                                Directory.Delete(rootFolder, true);
                            if (File.Exists(filemeta))
                                File.Delete(filemeta);
                            File.Delete(path);

                            result = AssetDeleteResult.DidDelete;
                        }
                    }
                    catch (IOException ex)
                    {
                        result = AssetDeleteResult.FailedDelete;
                        Debug.LogError(ex.Message);
                    }




                }
                else result = AssetDeleteResult.FailedDelete;
            }


            return result;
        }

        const string titleMove = "AI Asset Move";
        private static AssetMoveResult OnWillMoveAsset(string sourcePath, string destinationPath)
        {
            // Debug.Log("Source path: " + sourcePath + ". Destination path: " + destinationPath + ".");
            AssetMoveResult assetMoveResult = AssetMoveResult.DidNotMove;

            var AiGraph = AssetDatabase.LoadAssetAtPath<AIGraphAssetModel>(sourcePath);

            if (AiGraph)
            {
                try
                {
                    if (AiGraph.RootDirectory)
                    {
                        var path = AssetDatabase.GetAssetPath(AiGraph.RootDirectory);
                        if (Directory.Exists(path))
                        {
                            EditorUtility.DisplayDialog("AI Asset Move", $"You can't move AI Graph asset because of {path} folder.", "Ok");
                            assetMoveResult = AssetMoveResult.FailedMove;
                        }
                    }
                }
                catch (IOException ex)
                {
                    Debug.LogError(ex.Message);
                    assetMoveResult = AssetMoveResult.FailedMove;
                }
            }


            return assetMoveResult;
        }
    }
}
