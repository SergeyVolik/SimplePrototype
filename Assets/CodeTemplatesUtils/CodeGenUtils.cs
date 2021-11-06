using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;


namespace SV.CodeGen
{
    public interface INamespaceUsing
    {
        INamespaceUsing Using(string[] namespaceParts);
        ICodeGenBuilder EndUsing();
    }

    public interface INamespaceWrap
    {
        ICodeGenBuilder EndNamespace();
    }

    public interface IEnumCreate
    {
        IEnumCreate Create();
        ICodeGenBuilder FinishEnumCreate();
    }

    public interface IStructCreate
    {
        IStructCreate Create();
        ICodeGenBuilder FinishStruct();
    }

    public interface ICodeGenBuilder
    {
        ICodeGenBuilder SetPath(string saceFilePath);
        ICodeGenBuilder SetFilename(string filename);
        INamespaceUsing StartUsingNamespace();
        INamespaceWrap StartNamespace(string[] namespaceParams);

        void Build();
    }

    

    public class NamespaceUsingCodeGen : INamespaceUsing
    {
        private StringBuilder m_StringBuilder { get; set; }
        private ICodeGenBuilder m_Builder;
        public NamespaceUsingCodeGen(StringBuilder stringBuilder, ICodeGenBuilder builder)
        {
            m_StringBuilder = stringBuilder;
            m_Builder = builder;
        }
        public ICodeGenBuilder EndUsing()
        {
            return m_Builder;
        }


        public INamespaceUsing Using(string[] namespaceParts)
        {
            CodeGenUtils.AppendUsingNamespaceString(namespaceParts, m_StringBuilder);

            return this;
        }

    }

    public class NamespaceCreateCodeGen : INamespaceWrap
    {
        private StringBuilder m_StringBuilder { get; set; }
        private ICodeGenBuilder m_Builder;
        public NamespaceCreateCodeGen(StringBuilder stringBuilder, ICodeGenBuilder builder, string[] namespaceParts)
        {
            m_StringBuilder = stringBuilder;
            m_Builder = builder;

            CodeGenUtils.AppendNamespacePartTop(m_StringBuilder, namespaceParts);

        }


        public ICodeGenBuilder EndNamespace()
        {

            m_StringBuilder.Append("\n}");

            return m_Builder;

        }
    }
 
    public class CodeGenBuilder : ICodeGenBuilder
    {
        private StringBuilder m_StringBuilder { get; set; }
        private string m_SavePath;
        private string m_Filename;
        public CodeGenBuilder()
        {
            m_StringBuilder = new StringBuilder();
            m_StringBuilder.Append(CodeGenUtils.AutoGenTemplate);
            m_StringBuilder.Append("\n");
        }
        public void Build()
        {
            CodeGenUtils.WriteCodeToFile(string.Format(CodeGenUtils.FilePathFormat, m_SavePath, m_Filename), m_StringBuilder);
        }


        public INamespaceWrap StartNamespace(string[] namespaceParams)
        {
            return new NamespaceCreateCodeGen(m_StringBuilder, this, namespaceParams);
        }

        public INamespaceUsing StartUsingNamespace()
        {
            return new NamespaceUsingCodeGen(m_StringBuilder, this);
        }

        public ICodeGenBuilder SetPath(string saceFilePath)
        {
            m_SavePath = saceFilePath;
            return this;
        }

        public ICodeGenBuilder SetFilename(string filename)
        {
            m_Filename = filename;
            return this;
        }
    }


    public static class CodeGenUtils
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

        private static void AppendNamespaceString(string[] namespaceParts, StringBuilder builder)
        {
            var result = CodeGenUtils.AllPropertyNamesAreValid(namespaceParts);

            if (!result)
                throw new ArgumentException("some parts of the namespace are not formatted correctly");

           
            for (int i = 0; i < namespaceParts.Length; i++)
            {
                builder.Append(namespaceParts[i]);

                if (namespaceParts.Length - 1 != i)
                    builder.Append(".");
            }
            
        }

        public static void AppendUsingNamespaceString(string[] namespaceParts, StringBuilder builder)
        {
            builder.Append("using ");
            AppendNamespaceString(namespaceParts, builder);
            builder.Append(";\n");
        }

        public static void AppendCreateNamespaceString(string[] namespaceParts, StringBuilder builder)
        {
            
            builder.Append("namespace ");
            AppendNamespaceString(namespaceParts, builder);
            builder.Append("\n");
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
            else
            {
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

        public static void AppendNamespacePartTop(StringBuilder stringBuilder, string[] namespaceParams)
        {

            var isCorrect = AllPropertyNamesAreValid(namespaceParams);

            var namespaceParamsLenZero = namespaceParams == null || namespaceParams.Length == 0 ? true : false;

            if (!isCorrect)
            {
                throw new ArgumentException(ERROR_STR);
            }


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

            


        }


        [MenuItem("CodeGen/Create enum")]
        public static void CreateEnumTest()
        {

            if (!Directory.Exists("Assets/_CodeGen"))
            {
                Directory.CreateDirectory("Assets/_CodeGen");
            }

            var builder = new CodeGenBuilder();

            builder
                .SetPath("Assets/_CodeGen")
                .SetFilename("NewGenFile")
                .StartUsingNamespace()
                .Using(new string[] { "System" })
                .EndUsing()
                .StartNamespace(new string[] { "CodeGen", "AI" })
                .EndNamespace()
                .Build();
            //builder
            //    .SetPath("Assets/_CodeGen")
            //    .SetNamespace(new string[] { "CodeGen", "AI"  })
            //    .SetEnum("AIMoveStates", new string[] { "State1", "State2", "State3" })
            //    .Build();

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
        //public static void CreateEnum(string enumName, string[] enumParams, string saveFilePath = "Assets/_CodeGen", string[] namespaceParams = null)
        //{


        //    var enumNamesAreValid = AllPropertyNamesAreValid(enumParams);
        //    var enumNamesAreDifferent = AllStringsAreDifferent(enumParams);
        //    var enumNameIsValid = PropertyNameIsValid(enumName);



        //    if (!enumNamesAreValid)
        //        throw new ArgumentException("Enum name is invalid");

        //    if (!enumNamesAreDifferent)
        //        throw new ArgumentException("Not all enum propertis are different");

        //    if (!enumNameIsValid)
        //        throw new ArgumentException("Some enum properties are invalid");



        //    WriteCodeFile(string.Format(FilePathFormat, saveFilePath, enumName), (builderOuter) => {


        //        builderOuter.Append(AutoGenTemplate);
        //        builderOuter.Append("\n");

        //        CreateNamespacePart(builderOuter, namespaceParams, (builder, tabs) => CreateEnumPart(builder, enumName, enumParams, tabs));



        //    });

        //}

        public static void WriteCodeToFile(string path, StringBuilder builder)
        {

            try
            {


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

}

