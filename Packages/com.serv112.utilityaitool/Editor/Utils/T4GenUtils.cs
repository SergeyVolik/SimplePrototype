using GameDevWare.TextTransform;
using Microsoft.CodeDom.Providers.DotNetCompilerPlatform;
using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace SerV112.UtilityAIEditor
{
    public static class T4GenUtils
    {
        private static CSharpCodeProvider compiler = new CSharpCodeProvider();
        private static readonly string EnumTemplate = string.Join("/", DirectoryUtils.DefaultPath, "Editor/CodeGen/Templates/EnumTemplate.tt");
        private static readonly string AuthoringComponentTemplate = string.Join("/", DirectoryUtils.DefaultPath, "Editor/CodeGen/Templates/AuthoringComponentTemplate.tt");
        private static readonly string StructTemplate = string.Join("/", DirectoryUtils.DefaultPath, "Editor/CodeGen/Templates/StructTemplate.tt");
        private static readonly string UtilityAIAgentMonoInspector = string.Join("/", DirectoryUtils.DefaultPath, "Editor/CodeGen/Templates/UtilityAIAgentMonoInspector.tt");

        private static readonly string UtilityAISimulationTemplate = string.Join("/", DirectoryUtils.DefaultPath, "Editor/CodeGen/Templates/UtilityAISimulationMono.tt");
        private static readonly string UtilityAIAgentTemplate = string.Join("/", DirectoryUtils.DefaultPath, "Editor/CodeGen/Templates/UtilityAIAgentMono.tt");

        private static readonly string UtilityJobAISimulationMono = string.Join("/", DirectoryUtils.DefaultPath, "Editor/CodeGen/Templates/UtilityJobAISimulationMono.tt");
        private static readonly string UtilityJobAISimulationDataSO = string.Join("/", DirectoryUtils.DefaultPath, "Editor/CodeGen/Templates/UtilityJobAISimulationDataSO.tt");
        private static readonly string UtilityJobAIAgentMono = string.Join("/", DirectoryUtils.DefaultPath, "Editor/CodeGen/Templates/UtilityJobAIAgentMono.tt");



        //public static void CreateMonoBehaviourAIProcessorInspector(string pathWithScripts, string filename, AIAgentInspectorSettings settings)
        //{
        //    CreateTemplate(pathWithScripts, filename, "cs", JsonConvert.SerializeObject(settings), UtilityAIAgentMonoInspector);
        //}
        public static void CreateUtilityJobAISimulationMonoScript(string pathWithScripts, string filename, JobSystemAiSimulationSettins settings)
        {
            CreateTemplate(pathWithScripts, filename, "cs", JsonConvert.SerializeObject(settings), UtilityJobAISimulationMono);
        }

        public static void CreateUtilityJobAISimulationDataSO(string pathWithScripts, string filename, JobSystemAiSimulationSettins settings)
        {
            CreateTemplate(pathWithScripts, filename, "cs", JsonConvert.SerializeObject(settings), UtilityJobAISimulationDataSO);
        }

        public static void CreateUtilityJobAIAgentMono(string pathWithScripts, string filename, JobSystemAiSimulationSettins settings)
        {
            CreateTemplate(pathWithScripts, filename, "cs", JsonConvert.SerializeObject(settings), UtilityJobAIAgentMono);
        }

        public static void CreateUtilityAISimulationMonoScript(string pathWithScripts, string filename, UtilityAISimulationSettings settings)
        {
            CreateTemplate(pathWithScripts, filename, "cs", JsonConvert.SerializeObject(settings), UtilityAISimulationTemplate);
        }

        public static void CreateUtilityAIAgentMonoScript(string pathWithScripts, string filename, UtilityAISimulationSettings settings)
        {
            CreateTemplate(pathWithScripts, filename, "cs", JsonConvert.SerializeObject(settings), UtilityAIAgentTemplate);
        }

        public static void CreateTemplate(string pathWithScripts, string filename, string extention, string settingsJson, string templatePath)
        {
            filename = Path.ChangeExtension(filename, extention);
            pathWithScripts = string.Join("/", pathWithScripts, filename);

            var templateSettings = TemplateSettings.CreateDefault(templatePath);
            var temporaryFolder = string.Join("/", Application.temporaryCachePath, filename); ;
            var isFileExistInUnityProject = File.Exists(pathWithScripts);


            if (isFileExistInUnityProject)
            {
                UnityTemplateGenerator.RunForTemplate(
                  templatePath,
                  temporaryFolder,
                  settings: templateSettings,
                  parameters: new Dictionary<string, string>() {
                    { "settings" , settingsJson }
                    }
                  );

                if (!FileCompare(pathWithScripts, temporaryFolder))
                {
                    Debug.LogWarning($"files not equals: {pathWithScripts} {temporaryFolder}");
                    File.Copy(temporaryFolder, pathWithScripts, true);
                }
            }
            else
            {
                UnityTemplateGenerator.RunForTemplate(
                templatePath,
                pathWithScripts,
                settings: templateSettings,
                parameters: new Dictionary<string, string>() {
                    { "settings" , settingsJson }
                  }
                );
            }
        }

        private static bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            // Determine if the same file was referenced two times.
            if (file1 == file2)
            {
                // Return true to indicate that the files are the same.
                return true;
            }

            // Open the two files.
            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);

            // Check the file sizes. If they are not the same, the files
            // are not the same.
            if (fs1.Length != fs2.Length)
            {
                // Close the file
                fs1.Close();
                fs2.Close();

                // Return false to indicate files are different
                return false;
            }

            // Read and compare a byte from each file until either a
            // non-matching set of bytes is found or until the end of
            // file1 is reached.
            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            // Close the files.
            fs1.Close();
            fs2.Close();

            // Return the success of the comparison. "file1byte" is
            // equal to "file2byte" at this point only if the files are
            // the same.
            return ((file1byte - file2byte) == 0);
        }

        public static void CreateEcsComponent(string pathWithScripts, string name, Type type, string @namespace = "")
        {

            var type1 = new CodeTypeReference(type);

            T4GenUtils.CreateStruct(pathWithScripts, name, new CreateStructSettings
            {
                Name = name,
                Attributes = new List<string> { "Serializable", "GenerateAuthoringComponent" },
                Using = new List<string>() { "Unity.Entities", "System" },



                Fields = new List<FieldData> { new FieldData { Type = compiler.GetTypeOutput(type1), Name = "Value" } },
                Interfaces = new List<string> { "IComponentData" },
                Namespace = @namespace
            });
        }

        public static void CreateEcsComponent(string pathWithScripts, string name, string type, string @namespace = "")
        {

            var type1 = new CodeTypeReference(type);

            T4GenUtils.CreateStruct(pathWithScripts, name, new CreateStructSettings
            {
                Name = name,
                Attributes = new List<string> { "Serializable", "GenerateAuthoringComponent" },
                Using = new List<string>() { "Unity.Entities", "System" },
                Fields = new List<FieldData> { new FieldData { Type = type, Name = "Value" } },
                Interfaces = new List<string> { "IComponentData" },
                Namespace = @namespace
            });
        }



        public static void CreateEnum(string SavePath, string fileName, CreateEnumSettings settings)
        {

            CreateTemplate(SavePath, fileName, "cs", JsonConvert.SerializeObject(settings), EnumTemplate);
        }


        public static void CreateStruct(string SavePath, string fileName, CreateStructSettings settings)
        {
            CreateTemplate(SavePath, fileName, "cs", JsonConvert.SerializeObject(settings), StructTemplate);
        }

        public static void CreateAuthoringComponent(string SavePath, string fileName, CreateAuthoringComponentSettings settings)
        {

            CreateTemplate(SavePath, fileName, "cs", JsonConvert.SerializeObject(settings), AuthoringComponentTemplate);

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



    //[Serializable]
    //public class UtilityAIAgentMonoSettings : BaseGenCodeSettings
    //{
      
    //    public string SimulationClassName { get; set; }
    //    public List<string> ResultsTypesNames { get; set; }

    //}

    [Serializable]
    public class UtilityAISimulationSettings : BaseGenCodeSettings
    {
        public string AIAgentClassName { get; set; }
        public string AISimulationClassName { get; set; }
        public string AIAgentInspectorClassName { get; set; }
        public string AISimulationInspectorClassName { get; set; }

        public string ComputeShaderResourcePath { get; set; }

        public List<Property> Properties { get; set; }
        public List<FieldData> Results { get; set; }

        public AIAgentInspectorSettings AiAgentInspector { get; set; }

        public bool Debug;
        public int NumThreadsInComputeShader;
 
    }

    [Serializable]
    public class Property
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Range Range { get; set; }
    }
    [Serializable]
    public class BoxMessage
    {
        public MessageType MessageType { get; set; }
        public string Message { get; set; }
    }
    [Serializable]
    public class AIAgentInspectorSettings
    {
        public List<BoxMessage> BoxMessages;
    }

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

    [Serializable]
    public class Range
    {
        public float Min { get; set; }
        public float Max { get; set; }
    }

    [Serializable]
    public abstract class BaseGenCodeSettings
    {
        public string CodeGenMessage { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Parent { get; set; }
        public List<string> Using { get; set; }
        public List<string> Interfaces { get; set; }
        public List<string> Attributes { get; set; }


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
                    if (!string.IsNullOrEmpty(Namespace))
                    {
                        AttributsString.Append("\t");
                    }
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

            var parentExist = !string.IsNullOrEmpty(Parent);
            var interfaceExist = Interfaces?.Count > 0;
            if (parentExist || interfaceExist)
            {
                Interfaces1.Append(" : ");

                if (parentExist)
                {
                    Interfaces1.Append($"{Parent}");
                    if (interfaceExist && Interfaces.Count > 0)
                        Interfaces1.Append(",");
                }


                if (interfaceExist)
                {
                    for (var i = 0; i < Interfaces.Count; i++)
                    {
                        Interfaces1.Append(Interfaces[i]);
                        if (Interfaces.Count - 1 > i)
                            Interfaces1.Append(",");
                    }
                }

            }
            return Interfaces1.ToString();
        }
    }




    
    //[Serializable]
    //public class CreateAIProcessorSettings : BaseGenCodeSettings
    //{
    //    public List<ActionParts> ActionPartsOfCode { get; set; }
    //    public List<PropertyParts> PropertyPartsOfCode { get; set; }




    //}

    //[Serializable]
    //public class ActionParts
    //{
    //    public string EnumType { get; set; }
    //    public string Name { get; set; }

    //}

    //[Serializable]
    //public class PropertyParts
    //{
    //    public string Name { get; set; }

    //    public Range RageAttribut { get; set; }




    //}



    [Serializable]
    public class CreateStructSettings : BaseGenCodeSettings
    {
        public List<FieldData> Fields { get; set; }



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
