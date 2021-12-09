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
        public static void DefaultHandler(GraphToolState graphToolState, BuildAIEditorCommand command)
        {

            var path = graphToolState.WindowState.AssetModel.GetDirectoryName();

            var pathWithRes = string.Join("/", path, "Resources");
            var pathWithScripts = string.Join("/", path, "_CodeGen");


            if (AssetDatabase.IsValidFolder(pathWithRes))
            {
               
                AssetDatabase.DeleteAsset(pathWithRes);
               
            }
            AssetDatabase.CreateFolder(path, "Resources");

            if (AssetDatabase.IsValidFolder(pathWithScripts))
            {
                AssetDatabase.DeleteAsset(pathWithScripts);           
            }

            

            AssetDatabase.CreateFolder(path, "_CodeGen");

            var Namespace = ((AIGraphAssetModel)graphToolState.WindowState.AssetModel).Namespace;
            var varibales = graphToolState.WindowState.AssetModel.GraphModel.VariableDeclarations.ToList();

            varibales.ForEach(e =>
            {
                T4GenUtils.CreateEcsComponent(pathWithScripts, e.Title, typeof(float), Namespace);
            });

            graphToolState.WindowState.AssetModel.GraphModel.NodeModels.OfType<StateGroupNodeModel>().ToList().ForEach(e => e.GenereteStateGroup(pathWithScripts));

            AssetDatabase.Refresh();


        }

        
    }
    
}
