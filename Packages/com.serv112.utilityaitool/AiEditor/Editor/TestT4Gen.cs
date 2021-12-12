using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevWare.TextTransform;
using UnityEditor;
using Newtonsoft.Json;
using System.IO;

public static class TestT4Gen 
{

    [MenuItem("T4/Gen Enum")]
    private static void GenEnum()
    {

        var enumName = "MyEnum";

        var enumSettings = new CreateEnumSettings(enumName, new List<string> { "Param1", "Param2", "Param3" });
        T4GenUtils.CreateEnum("Assets", "myEnumWithoutNamespace", enumSettings);

        var enumSettings2 = new CreateEnumSettings(enumName, new List<string> { "Param1", "Param2", "Param3" }, "MyNamespace");
        T4GenUtils.CreateEnum("Assets", "myEnumWithNamespace", enumSettings2);

        
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