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
using UnityEngine;

namespace SerV112.UtilityAIEditor
{
    public static class T4GenUtils
    {
        private static CSharpCodeProvider compiler = new CSharpCodeProvider();
        private static readonly string EnumTemplate = string.Join("/", DirectoryUtils.DefaultPath, "AiEditor/CodeGen/Templates/EnumTemplate.tt");
        private static readonly string AuthoringComponentTemplate = string.Join("/", DirectoryUtils.DefaultPath, "AiEditor/CodeGen/Templates/AuthoringComponentTemplate.tt");
        private static readonly string ComputeShaderTemplate = string.Join("/", DirectoryUtils.DefaultPath, "AiEditor/CodeGen/Templates/ComputeShaderTemplate.tt");
        private static readonly string StructTemplate = string.Join("/", DirectoryUtils.DefaultPath, "AiEditor/CodeGen/Templates/StructTemplate.tt");

        public static void CreateEcsComponent(string pathWithScripts, string name, Type type, string @namespace = "")
        {

            var type1 = new CodeTypeReference(type);

            T4GenUtils.CreateStruct(pathWithScripts, name, new CreateStructSettings
            {
                StructName = name,
                Attributes = new List<string> { "Serializable", "GenerateAuthoringComponent" },
                Using = new List<string>() { "Unity.Entities", "System" },

              

            Fields = new List<CreateStructSettings.FieldData> { new CreateStructSettings.FieldData { Type = compiler.GetTypeOutput(type1), Name = "Value" } },
                Interfaces = new List<string> { "IComponentData" },
                Namespace = @namespace
            });
        }

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

            if (Interfaces.Count > 0)
            {
                Interfaces1.Append(" : ");
                for (var i = 0; i < Interfaces.Count; i++)
                {
                    Interfaces1.Append(Interfaces[i]);
                    if (Interfaces.Count - 1 > i)
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
