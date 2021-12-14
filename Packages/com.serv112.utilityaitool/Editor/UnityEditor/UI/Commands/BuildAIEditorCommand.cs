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
            var assetModel = graphToolState.WindowState.AssetModel as AIGraphAssetModel;

            switch (assetModel.BuildMode)
            {
                case BuildMode.ECS:
                    Build = new ECSBuild(assetModel);

                    break;
                case BuildMode.MonoBehaviour:
                    Build = new MonobehaviourBuild(assetModel);
                    break;
                default:
                    Build = new MonobehaviourBuild(assetModel);
                    break;
            }

            Build.Build();

        }

    }

}
