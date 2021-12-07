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
using UnityEngine;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;
using UnityEngine.GraphToolsFoundation.Overdrive;
using UnityEngine.SceneManagement;

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
            //var graphModel = graphToolState.WindowState.AssetModel.GraphModel;
            var path = graphToolState.WindowState.AssetModel.GetDirectoryName();

            //var actionGroup = graphModel.NodeModels.OfType<StateGroupNodeModel>().ToList();
            //List<string> fullPaths = new List<string>();

            //for (var i = 0; i < actionGroup.Count; i++)
            //{
            //    var actionGroupModel = actionGroup[i];

            //    var enumName = actionGroupModel.ActionGroupName;

            //    var @namespace = actionGroupModel.FileNamespace;

            //    var listEnumParams = new List<string>();

            //    var ports = actionGroupModel.GetPorts(direction: PortDirection.Input, portType: PortType.Execution).Where(e => e.GetConnectedEdges().Count() > 0).ToList();


            //    for (int j = 0; j < ports.Count; j++)
            //    {
            //        var port = ports[j];

            //        var node = port.GetConnectedEdges().FirstOrDefault()?.FromPort.NodeModel as StateNodeModel;

            //        if (node.State == ModelState.Enabled)
            //            listEnumParams.Add(node.Name);
            //    }

            //    var enumSettings = new CreateEnumSettings(enumName, listEnumParams);

            //    if (!string.IsNullOrEmpty(@namespace))
            //    {
            //        enumSettings.Namespace = @namespace;
            //    }




            //    fullPaths.Add(T4GenUtils.CreateEnum(path, enumName, enumSettings));
            //}

            var @namespace = "MyNamespace";
            var compName = "Struct1";
            var enumName = "Enum1";
            var authComp = "Authoring" + compName;
            var namesOfClasses = new string[] { compName, enumName, authComp };

            var pathWithRes = string.Join("/", path, "Resources");
            var pathWithScripts = string.Join("/", path, "_CodeGen");

            CodeGenValidationUtils.Validate(namesOfClasses, pathWithScripts, @namespace);

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




            var externalVariables = graphToolState.WindowState.AssetModel.GraphModel.VariableDeclarations;

            for (int i = 0; i < externalVariables.Count; i++)
            {
                if (externalVariables[i].DataType == TypeHandle.Float)
                {

                    var variable = externalVariables[i];
                    var strcutName = variable.GetVariableName();
                    Debug.Log(strcutName);

                    T4GenUtils.CreateStruct(pathWithScripts, strcutName, new CreateStructSettings
                    {
                        StructName = strcutName,
                        Attributes = new List<string> { "Serializable", "GenerateAuthoringComponent" },
                        Using = new List<string>() { "Unity.Entities", "System" },

                        Fields = new List<CreateStructSettings.FieldData> { new CreateStructSettings.FieldData { Type = "float", Name = "Value" } },
                        Interfaces = new List<string> { "IComponentData" },
                        Namespace = ""
                    });
                }

            }



          

            //T4GenUtils.CreateStruct(pathWithScripts, compName, new CreateStructSettings
            //{
            //    StructName = compName,
            //    Attributes = new List<string> { "Serializable" },
            //    Using = new List<string>() { "System", "Unity.Mathematics", "Unity.Entities", "Unity.Collections" },

            //    Fields = new List<CreateStructSettings.FieldData> { new CreateStructSettings.FieldData { Type = enumName, Name = "Field1" } },
            //    Interfaces = new List<string> { "IComponentData"},
            //    Namespace = @namespace
            //});

            //T4GenUtils.CreateEnum(pathWithScripts, enumName, new CreateEnumSettings(enumName, new List<string> { "Idle","Run","Walk"}, @namespace));

            //T4GenUtils.CreateAuthoringComponent(pathWithScripts, authComp, new CreateAuthoringComponentSettings
            //{
            //    Name = authComp,
            //    Namespace = @namespace,
            //    RuntimeComponent = new CreateAuthoringComponentSettings.Component
            //    {
            //        Type = compName,
            //        Fields = new List<CreateAuthoringComponentSettings.ComponentField> {
            //                  new CreateAuthoringComponentSettings.ComponentField{
            //                       Type = enumName,
            //                        Name = "Field1"
            //                   }

            //             }

            //    },
            //    Using = new List<string>()
                


            //});

            AssetDatabase.Refresh();


        }
    }
    public static class T4GenUtils
    {
        private static readonly string EnumTemplate = string.Join("/", Application.dataPath, "AiEditor/CodeGen/Templates/EnumTemplate.tt");
        private static readonly string AuthoringComponentTemplate = string.Join("/", Application.dataPath, "AiEditor/CodeGen/Templates/AuthoringComponentTemplate.tt");
        private static readonly string ComputeShaderTemplate = string.Join("/", Application.dataPath, "AiEditor/CodeGen/Templates/ComputeShaderTemplate.tt");
        private static readonly string StructTemplate = string.Join("/", Application.dataPath, "AiEditor/CodeGen/Templates/StructTemplate.tt");
       
        public static string CreateEnum(string SavePath, string fileName, CreateEnumSettings settings)
        {

            fileName = Path.ChangeExtension(fileName, "gen.cs");
            SavePath = string.Join("/", SavePath, fileName);
            string templatePath = EnumTemplate;

            var templateSettings = TemplateSettings.CreateDefault(templatePath);
            string templateInputSettigns = JsonConvert.SerializeObject(settings);

            UnityTemplateGenerator.RunForTemplate(
              templatePath,
              SavePath,
              settings: templateSettings,
              parameters: new Dictionary<string, string>() {
                    { "settings" , templateInputSettigns }
                }
              );

            return SavePath;
        }


        public static void CreateComputeShader(string SavePath, string fileName, CreateComputeShaderSettings settings)
        {
            fileName = Path.ChangeExtension(fileName, "gen.compute");
            SavePath = string.Join("/", SavePath, fileName);
            string templatePath = ComputeShaderTemplate;

            var templateSettings = TemplateSettings.CreateDefault(templatePath);
            string templateInputSettigns = JsonConvert.SerializeObject(settings);

            UnityTemplateGenerator.RunForTemplate(
             templatePath,
             SavePath,
             settings: templateSettings,
             parameters: new Dictionary<string, string>() {
                    { "settings" , templateInputSettigns }
               }
             );
        }

        public static void CreateStruct(string SavePath, string fileName, CreateStructSettings settings)
        {
            fileName = Path.ChangeExtension(fileName, "gen.cs");
            SavePath = string.Join("/", SavePath, fileName);
            string templatePath = StructTemplate;
        

            var templateSettings = TemplateSettings.CreateDefault(templatePath);
            string templateInputSettigns = JsonConvert.SerializeObject(settings);

            UnityTemplateGenerator.RunForTemplate(
             templatePath,
             SavePath,
             settings: templateSettings,
             parameters: new Dictionary<string, string>() {
                    { "settings" , templateInputSettigns }
               }
             );
        }

        public static void CreateAuthoringComponent(string SavePath, string fileName, CreateAuthoringComponentSettings settings)
        {
            fileName = Path.ChangeExtension(fileName, "cs");
            SavePath = string.Join("/", SavePath, fileName);
            string templatePath = AuthoringComponentTemplate;


            var templateSettings = TemplateSettings.CreateDefault(templatePath);
            string templateInputSettigns = JsonConvert.SerializeObject(settings);

            UnityTemplateGenerator.RunForTemplate(
             templatePath,
             SavePath,
             settings: templateSettings,
             parameters: new Dictionary<string, string>() {
                    { "settings" , templateInputSettigns }
               }
             );
        }
    }

    [Serializable]
    public class CreateComputeShaderSettings
    {
        public int Numthreads { get; set; }
        public CreateComputeShaderSettings(int numthreads)
        {
            Numthreads = numthreads;
        }
    }

    [Serializable]
    public class CreateAuthoringComponentSettings
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public List<string> Using { get; set; }

        public Component RuntimeComponent { get; set; }

        [Serializable]
        public class Component
        {
            public string Type { get; set; }
            public List<ComponentField> Fields { get; set; }

        }

        [Serializable]
        public class ComponentField
        {
            public string Name { get; set; }
            public string Type { get; set; }
        }

        public bool IsNamespace()
        {
            return !string.IsNullOrEmpty(Namespace);
        }

        public string GetUsingString()
        {
            var Using1 = new StringBuilder("");
            if (Using != null && Using.Count > 0)
            {
                for (var i = 0; i < Using.Count; i++)
                {
                    Using1.Append("using ");
                    Using1.Append(Using[i]);
                    Using1.Append(";");
                    Using1.Append(Environment.NewLine);
                }
            }

            return Using1.ToString();
        }
        public string GetFields()
        {
            var FieldsComponentFields = new StringBuilder("");
            if (RuntimeComponent != null && RuntimeComponent.Fields != null && RuntimeComponent.Fields.Count > 0)
            {
                for (var i = 0; i < RuntimeComponent.Fields.Count; i++)
                {
                    FieldsComponentFields.AppendLine("[SerializeField]");
                    FieldsComponentFields.AppendLine($"private {RuntimeComponent.Fields[i].Type}  m_{RuntimeComponent.Fields[i].Name};");

                }
            }

            return FieldsComponentFields.ToString();
        }
        public string GetInitCopmonentValues()
        {
            var InitComponentFields = new StringBuilder("");
            if (RuntimeComponent.Fields.Count > 0)
            {
                for (var i = 0; i < RuntimeComponent.Fields.Count; i++)
                {
                    InitComponentFields.Append(RuntimeComponent.Fields[i].Name);
                    InitComponentFields.Append(" = ");
                    InitComponentFields.Append($"m_{RuntimeComponent.Fields[i].Name}");
                    if (RuntimeComponent.Fields.Count - 1 > i)
                    {
                        InitComponentFields.Append(",");
                        InitComponentFields.Append(Environment.NewLine);
                    }

                }
            }

            return InitComponentFields.ToString();
        }
    }

    [Serializable]
    public class CreateStructSettings
    {
        public string StructName { get; set; }
        public string Namespace { get; set; }

        public List<string> Interfaces { get; set; }
        public List<string> Attributes { get; set; }
        public List<string> Using { get; set; }
        public List<FieldData> Fields { get; set; }

        [Serializable]
        public class FieldData
        {
            public string Type;
            public string Name;

            public override string ToString()
            {
                return "public " + Type + " " + Name + ";";
            }
        }

        public string GetAttributs()
        {
            var AttributsString = new StringBuilder("");
            if (Attributes.Count > 0)
            {
               
                for (var i = 0; i < Attributes.Count; i++)
                {
                    AttributsString.Append("[");
                    AttributsString.Append(Attributes[i]);
                    AttributsString.Append("]");
                    if (Attributes.Count - 1 > i)
                        AttributsString.Append(Environment.NewLine);
                }
               
            }
            return AttributsString.ToString();
        }

        public bool IsNamespace()
        {
            if (string.IsNullOrEmpty(Namespace))
            {
                return false;
            }

            return true;
        }

        public string GetUsingString()
        {
            var Using1 = new StringBuilder("");
            if (Using.Count > 0)
            {
                for (var i = 0; i < Using.Count; i++)
                {
                    Using1.Append("using ");
                    Using1.Append(Using[i]);
                    Using1.Append(";");
                    Using1.Append(Environment.NewLine);
                }
            }

            return Using1.ToString();
        }

        public string GetInterfacesString()
        {
            var Interfaces1 = new StringBuilder("");

            if (Interfaces.Count > 0)
            {
                Interfaces1.Append(" : ");
                for (var i = 0; i < Interfaces.Count; i++)
                {
                    Interfaces1.Append(Interfaces[i]);
                    if(Interfaces.Count-1 > i)
                        Interfaces1.Append(",");
                }
            }
            return Interfaces1.ToString();
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

        public bool IsNamespace()
        {
            return !string.IsNullOrEmpty(Namespace);
        }
    }
}
