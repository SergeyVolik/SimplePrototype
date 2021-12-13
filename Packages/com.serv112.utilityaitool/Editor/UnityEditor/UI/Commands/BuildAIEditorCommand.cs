using GameDevWare.TextTransform;
using Newtonsoft.Json;
using System;
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
        AIGraphAssetModel m_AssetModel;
        string pathWithScripts;
        public static void DefaultHandler(GraphToolState graphToolState, BuildAIEditorCommand command)
        {
            command.m_AssetModel = graphToolState.WindowState.AssetModel as AIGraphAssetModel;
            var path = graphToolState.WindowState.AssetModel.GetDirectoryName();


            command.pathWithScripts = string.Join("/", path, "_CodeGen");


            if (AssetDatabase.IsValidFolder(command.pathWithScripts))
            {
                AssetDatabase.DeleteAsset(command.pathWithScripts);           
            }

            AssetDatabase.CreateFolder(path, "_CodeGen");

           
           

            switch (command.m_AssetModel.BuildMode)
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

            AssetDatabase.Refresh();

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



            var AiProcessorSettins = new CreateAIProcessorSettings
            {
                 Name = "AiProcessorCustom",
                 Namespace = Namespace,
                 Parent = "AIGraphProcessor",
                 Attributes = new List<string> { "DisallowMultipleComponent" },
                 PropertyPartsOfCode = properties,
                 ActionPartsOfCode = enumTypes

            };

            T4GenUtils.CreateMonoBehaviourAIProcessor(pathWithScripts, "AiProcessorCustom", AiProcessorSettins);
        }  

        
    }
    
}
