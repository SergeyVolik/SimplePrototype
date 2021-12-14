using GameDevWare.TextTransform;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;
using UnityEngine.GraphToolsFoundation.Overdrive;
using UnityEngine.SceneManagement;

namespace SerV112.UtilityAIEditor
{
    public enum AIGraphBuidState { 
        BeforeReimport,
        AfterReimport
    }
    public class BuildAIEditorCommand : UndoableCommand
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildAIEditorCommand"/> class.
        /// </summary>
        /// 
        
        public BuildAIEditorCommand()
        {

            UndoString = "Compile Graph";
        }

        /// <summary>
        /// Default command handler.
        /// </summary>
        /// <param name="graphToolState">The state.</param>
        /// <param name="command">The command.</param>
        /// 
        static AIGraphAssetModel m_AssetModel;
        static string pathWithScripts;
        static string pathWithEditorScripts;
        static AIGraphBuidState State;

        public static void DefaultHandler(GraphToolState graphToolState, BuildAIEditorCommand command)
        {
            State = AIGraphBuidState.BeforeReimport;
            m_AssetModel = graphToolState.WindowState.AssetModel as AIGraphAssetModel;
            var path = graphToolState.WindowState.AssetModel.GetDirectoryName();
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



            switch (m_AssetModel.BuildMode)
            {
                case BuildMode.ECS:
                    command.EcsBuild();
                    break;
                case BuildMode.MonoBehaviour:
                    command.MonoBehaviourBuild();
                    break;
                default:
                    break;
            }

            
            State = AIGraphBuidState.AfterReimport;
            AssetDatabase.Refresh();

           
           
        }


        static void CollectAssetRefs(string[] importedAssets)
        {
            if (State == AIGraphBuidState.AfterReimport && m_AssetModel)
            {

                m_AssetModel.RootDirectory = AssetDatabase.LoadAssetAtPath(pathWithScripts, typeof(UnityEngine.Object));
                m_AssetModel.GeneratedObjects.Add(new AIGraphAsset { 
                     ObjectPath = pathWithScripts,
                     Object = AssetDatabase.LoadAssetAtPath(pathWithScripts, typeof(UnityEngine.Object))
                });


                foreach (string str in importedAssets)
                {
                    m_AssetModel.GeneratedObjects.Add(new AIGraphAsset { 
                        ObjectPath = str,
                        Object = AssetDatabase.LoadAssetAtPath(str, typeof(UnityEngine.Object))
                    });
                }

                AssetDatabase.SaveAssetIfDirty(m_AssetModel);

                m_AssetModel = null;
                
            }
        }

        class AIGraphBuildPostprocessor : AssetPostprocessor
        {
            static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
            {
                
                CollectAssetRefs(importedAssets);
            }
        }

        class AiGraphAssetModificationProcessor : UnityEditor.AssetModificationProcessor
        {
          
            static AssetDeleteResult OnWillDeleteAsset(string path, RemoveAssetOptions opt)
            {
                var AiGraph = AssetDatabase.LoadAssetAtPath<AIGraphAssetModel>(path);

                var result = AssetDeleteResult.DidNotDelete;

                if (AiGraph)
                {
                    if (EditorUtility.DisplayDialog("AI Asset", "Do you want to delete AI Graph asset? If you do it, all depended assets will be deleted!", "Ok", "Cancel"))
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

            private static AssetMoveResult OnWillMoveAsset(string sourcePath, string destinationPath)
            {
               // Debug.Log("Source path: " + sourcePath + ". Destination path: " + destinationPath + ".");
                AssetMoveResult assetMoveResult = AssetMoveResult.DidNotMove;

                var AiGraph = AssetDatabase.LoadAssetAtPath<AIGraphAssetModel>(sourcePath);

                if (AiGraph)
                {
                    try
                    {
                        //var path = Path.GetDirectoryName(destinationPath);
                        //Debug.Log(Path.GetDirectoryName(AiGraph.RootDirectory));
                        //string FolderName = new DirectoryInfo(AiGraph.RootDirectory).Name;
                        //if (!string.IsNullOrEmpty(AiGraph.RootDirectory) && Directory.Exists(AiGraph.RootDirectory))
                        //{

                        //    Directory.Move(AiGraph.RootDirectory, $"{path}/{FolderName}");
                        //    AiGraph.RootDirectory = $"{path}/{FolderName}";
                        //    File.Move(sourcePath, destinationPath);
                        //    assetMoveResult = AssetMoveResult.DidMove;
                        //}
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



        void EcsBuild()
        {
            var varibales = m_AssetModel.GraphModel.VariableDeclarations.ToList();
            var Namespace = m_AssetModel.Namespace;

            varibales.ForEach(e =>
            {
                T4GenUtils.CreateEcsComponent(pathWithScripts, e.GetVariableName(), typeof(float), Namespace);
            });
        }

        void MonoBehaviourBuild()
        {
           var enumTypes = new List<ActionParts>();
            var nameNumber = 1;
            m_AssetModel.GraphModel.NodeModels.OfType<StateGroupNodeModel>().ToList().ForEach(e => {

                e.GenereteStateGroup(pathWithScripts);
                enumTypes.Add(new ActionParts { Name = $"Action{nameNumber}", EnumType = e.Name  });
                nameNumber++;
            });

            var Namespace = m_AssetModel.Namespace;
            var varibales = m_AssetModel.GraphModel.VariableDeclarations.ToList();

            var properties = new List<PropertyParts>();
            for (int i = 0; i < varibales.Count; i++)
            {
                properties.Add(new PropertyParts
                {
                    Name = varibales[i].GetVariableName(),
                    RageAttribut = new Range { 
                         Max = 100,
                         Min = 0
                    }
                }); 
            }




            var AiProcessorSettinsName = "AiProcessorNew";
            var AiProcessorSettins = new CreateAIProcessorSettings
            {
                 Name = AiProcessorSettinsName,
                 Namespace = Namespace,
                 Parent = "AIGraphProcessor",
                 Attributes = new List<string> { "DisallowMultipleComponent" },
                 PropertyPartsOfCode = properties,
                 ActionPartsOfCode = enumTypes

            };

            T4GenUtils.CreateMonoBehaviourAIProcessor(pathWithScripts, AiProcessorSettinsName, AiProcessorSettins);

            var @params = new List<string>();

            for (int i = 0; i < enumTypes.Count; i++)
            {
                @params.Add(enumTypes[i].Name);
                @params.Add($"Event{enumTypes[i].Name}");
            }
            for (int i = 0; i < properties.Count; i++)
            {
                @params.Add(properties[i].Name);
            }

            var editorInspector = $"{AiProcessorSettinsName}Inspector";
            var AIProcessorInspector = new CreateAIProcessorInspectorSettings
            {
                Namespace = Namespace,
                Name = editorInspector,
                Parent = "Editor",
                Attributes = new List<string> { $"CustomEditor(typeof({AiProcessorSettinsName})) ","CanEditMultipleObjects"},
                ErrorMessage = "Asset not setted!",
                SerializedProperties = @params,
                 

            };

            T4GenUtils.CreateMonoBehaviourAIProcessorInspector(pathWithEditorScripts, editorInspector, AIProcessorInspector);
        }  

        
    }
    
}
