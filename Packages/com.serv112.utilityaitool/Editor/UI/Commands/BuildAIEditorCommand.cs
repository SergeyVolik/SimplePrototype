using GameDevWare.TextTransform;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static BaseBuild Build;


        public static void DefaultHandler(GraphToolState graphToolState, BuildAIEditorCommand command)
        {
            var AiGraphModel = graphToolState.WindowState.GraphModel as AIGraphModel;
            var assetModel = AiGraphModel.AssetModel as AIGraphAssetModel;

            switch (assetModel.BuildMode)
            {
                case BuildMode.MonoBehaviourGPU:
                    Build = new MonobehaviourGPUBuild(AiGraphModel);
                    break;
                case BuildMode.MonoBehaviourJobSystem:
                    Build = new MonobehaviourJobSystemBuild(AiGraphModel);
                    
                    break;
                default:
                    Build = new MonobehaviourGPUBuild(AiGraphModel);
                    break;
            }

            Build.Build();

        }

    }

}
