using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;


public static class CodeTemplates 
{
    public const string AutoGenFormat =
@"//-----------------------------------------------------------------------
// This file is AUTO-GENERATED.
// Changes for this script by hand might be lost when auto-generation is run.
// (Generated date: {0})
//-----------------------------------------------------------------------";


    /// <summary>
    /// {0} - folder path
    /// {1} - file name without format
    /// </summary>
    public const string FilePathFormat = "{0}/{1}.gen.cs";

    // header
    public static string AutoGenTemplate { get { return string.Format(AutoGenFormat, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")); } }

    // namespace

    private static Regex ValidProperty = new Regex(VALID_PROPERTY_NAME);
    const string VALID_PROPERTY_NAME = @"^[a-zA-Z_]\w*(\.[a-zA-Z_]\w*)*$";
    const string ERROR_STR = "CodeTemplates.CreateNameSpaceString has invalid namespaceParams value";

    private static bool AllPropertyNamesAreValid(string[] propertyNames)
    {
        bool isCorrect = true;

        if (propertyNames != null)
        {
            for (int i = 0; i < propertyNames.Length; i++)
            {
                if (!ValidProperty.IsMatch(propertyNames[i]))
                {
                    isCorrect = false;
                    break;
                }
            }
        }
        return isCorrect;
    }

    private static bool AllStringsAreDifferent(string[] propertyNames)
    {
        bool isCorrect = true;
        for (int i = 0; i < propertyNames.Length; i++)
        {
            for (int j = 0; j < propertyNames.Length; j++)
            {
                if (i == j)
                    continue;

                if (propertyNames[i] == propertyNames[j])
                    isCorrect = false;
            }
        }

        return isCorrect;
    }

    private static bool PropertyNameIsValid(string propertyNames)
    {
        return ValidProperty.IsMatch(propertyNames);
    }
    public static void CreateNamespacePart(StringBuilder stringBuilder, string[] namespaceParams, Action<StringBuilder, int> createNext)
    {

        var isCorrect = AllPropertyNamesAreValid(namespaceParams);

        var namespaceParamsLenZero = namespaceParams == null || namespaceParams.Length == 0 ? true : false;

        if (!isCorrect)
        {
           throw new ArgumentException(ERROR_STR);
        }

        if (namespaceParamsLenZero)
        {
            createNext?.Invoke(stringBuilder, 0);
        }
        else {
            stringBuilder.Append("namespace ");

            for (int i = 0; i < namespaceParams.Length; i++)
            {
                stringBuilder.Append(namespaceParams[i]);

                if (namespaceParams.Length - 1 != i)
                    stringBuilder.Append(".");
            }

            stringBuilder.Append("\n");
            stringBuilder.Append("{");
            stringBuilder.Append("\n");

            createNext?.Invoke(stringBuilder, 1);

            stringBuilder.Append("\n");
            stringBuilder.Append("}");
        }
       

    }


    [MenuItem("CodeGen/Create enum")]
    public static void CreateEnumTest()
    {

        if (!Directory.Exists("Assets/_CodeGen"))
        {
            Directory.CreateDirectory("Assets/_CodeGen");
        }



        CreateEnum("MyEnum", new string[] { "EnumParam1", "EnumParam2", "EnumParam3" }
            ,new string[] { "myNamespace", "spaceName2", "spaceName2", "spaceName2", "spaceName2", "spaceName2" });
       


        AssetDatabase.Refresh();
    }

    private static void CreateEnumPart(StringBuilder stringBuilder, string enumName, string[] enumParams, int tab)
    {
        stringBuilder.Append("\n");
        for (var j = 0; j < tab; j++)
        {
            stringBuilder.Append("\t");
        }
        stringBuilder.Append($"public enum {enumName} \n");
      
        for (var j = 0; j < tab; j++)
        {
            stringBuilder.Append("\t");
        }
        stringBuilder.Append("{");

        for (int i = 0; i < enumParams.Length; i++)
        {
            stringBuilder.Append("\n\t");
            for (var j = 0; j < tab; j++)
            {
                stringBuilder.Append("\t");
            }
            stringBuilder.Append(enumParams[i]);
            stringBuilder.Append(",");
        }
        stringBuilder.Append("\n");

        for (var j = 0; j < tab; j++)
        {
            stringBuilder.Append("\t");
        }
        stringBuilder.Append("}");

        stringBuilder.Append("\n");
    }
    public static void CreateEnum(string enumName, string[] enumParams, string[] namespaceParams = null)
    {
      

        var enumNamesAreValid = AllPropertyNamesAreValid(enumParams);
        var enumNamesAreDifferent = AllStringsAreDifferent(enumParams);
        var enumNameIsValid = PropertyNameIsValid(enumName);


       
        if (!enumNamesAreValid)
           throw new ArgumentException("Enum name is invalid");

        if (!enumNamesAreDifferent)
            throw new ArgumentException("Not all enum propertis are different");

        if (!enumNameIsValid)
            throw new ArgumentException("Some enum properties are invalid");



        WriteCodeFile(string.Format(FilePathFormat, "Assets/_CodeGen", enumName), (builderOuter) => {


            builderOuter.Append(AutoGenTemplate);
            builderOuter.Append("\n");

            CreateNamespacePart(builderOuter, namespaceParams, (builder, tabs) => CreateEnumPart(builder, enumName, enumParams, tabs));



        });

       

       


    }

    private static void WriteCodeFile(string path, System.Action<StringBuilder> callback)
    {
        Debug.Assert(callback != null);

        //if (!Directory.Exists(DirPath))
        //{
        //    Directory.CreateDirectory(DirPath);
        //}

        try
        {
            StringBuilder builder = new StringBuilder();
            callback(builder);


            // Always create a new file because overwriting to existing file may generate mal-formatted script.
            // for instance, when the number of tags is reduced, last tag will be remain after the last curly brace in the file.
            using (FileStream stream = File.Open(path, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {

                    writer.Write(builder.ToString());
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);

        }

        AssetDatabase.Refresh();
    }
}
