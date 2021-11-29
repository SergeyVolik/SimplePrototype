using GameDevWare.TextTransform;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace SerV112.UtilityAIEditor
{
    public class BuildAIEditorCommand : UndoableCommand
    {

        private string BuildPath = "Assets";

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
            var graphModel = graphToolState.WindowState.AssetModel.GraphModel;
            var path = graphToolState.WindowState.AssetModel.GetDirectoryName();

            var actionGroup = graphModel.NodeModels.OfType<StateGroupNodeModel>().ToList();

            for (var i = 0; i < actionGroup.Count; i++)
            {
                var actionGroupModel = actionGroup[i];

                var enumName = actionGroupModel.ActionGroupName;

                var @namespace = actionGroupModel.FileNamespace;

                var listEnumParams = new List<string>();

                var ports = actionGroupModel.GetPorts(direction: PortDirection.Input, portType: PortType.Execution).Where(e => e.GetConnectedEdges().Count() > 0).ToList();


                for (int j = 0; j < ports.Count; j++)
                {
                    var port = ports[j];

                    var node = port.GetConnectedEdges().FirstOrDefault()?.FromPort.NodeModel as StateNodeModel;

                    if(node.State == ModelState.Enabled)
                        listEnumParams.Add(node.Name);
                }

                var enumSettings = new CreateEnumSettings(enumName, listEnumParams);

                if (!string.IsNullOrEmpty(@namespace))
                {
                    enumSettings.Namespace = @namespace;
                }




                T4GenUtils.CreateEnum(path, enumName, enumSettings);
            }

           

           




            AssetDatabase.Refresh();
        }
    }
    public static class T4GenUtils
    {
        public static void CreateEnum(string SavePath, string fileName, CreateEnumSettings settings)
        {

            fileName = Path.ChangeExtension(fileName, "gen.cs");
            SavePath = string.Join("/", SavePath, fileName);
            string templatePath = string.Join("/", Application.dataPath, "AiEditor/CodeGen/Templates/EnumWithNamesapceTemplate.tt");

            if (string.IsNullOrEmpty(settings.Namespace))
            {
                templatePath = string.Join("/", Application.dataPath, "AiEditor/CodeGen/Templates/EnumWithoutNamesapceTemplate.tt");
            }

            var templateSettings = TemplateSettings.CreateDefault(templatePath);
            string templateInputSettigns = JsonConvert.SerializeObject(settings);

            UnityTemplateGenerator.RunForTemplate(
              templatePath,
              SavePath,
              settings: templateSettings,
              parameters: new Dictionary<string, string>() {
                    { "settings" , templateInputSettigns }
                }
              //,includeLookupPaths: new List<string> { "AiEditor/CodeGen/Templates/templateHeader.t4" }
              );
        }
    }


    [Serializable]
    public class CreateEnumSettings
    {
        public List<string> EnumFields;
        public string EnumName;
        public string Namespace;
        public CreateEnumSettings(string enumName, List<string> enumFields, string namespaceName = null)
        {
            EnumFields = enumFields;
            Namespace = namespaceName;
            EnumName = enumName;
        }
    }
}
