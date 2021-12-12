//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using UnityEditor;
//using UnityEngine;


//namespace SV.CodeGen
//{
//    public interface INamespaceUsing
//    {
//        INamespaceUsing Using(string[] namespaceParts);
//        ICodeGenBuilder EndUsing();
//    }

//    public interface INamespaceWrap : IEnumCreate
//    {
//        ICodeGenBuilder EndNamespace();

//        INamespaceWrap StartNamespace(string[] namespaceParts);
//    }

//    public interface IEnumCreate
//    {
//        INamespaceWrap CreateEnum(string enumName, string[] enumParams);
//    }

//    public interface IStructCreate
//    {
//        IStructCreate Create();
//        ICodeGenBuilder FinishStruct();
//    }

//    public interface ICodeGenBuilder
//    {
//        ICodeGenBuilder SetPath(string saceFilePath);
//        ICodeGenBuilder SetFilename(string filename);
//        INamespaceUsing ToUsingNamespace();
//        INamespaceWrap ToCreateNamespace();

//        void Build();
//    }

    

//    public class NamespaceUsingCodeGen : INamespaceUsing
//    {
//        private StringBuilder m_StringBuilder { get; set; }
//        private ICodeGenBuilder m_Builder;
//        public NamespaceUsingCodeGen(StringBuilder stringBuilder, ICodeGenBuilder builder)
//        {
//            m_StringBuilder = stringBuilder;
//            m_Builder = builder;
//        }
//        public ICodeGenBuilder EndUsing()
//        {
//            m_StringBuilder.Append(Environment.NewLine);
//            return m_Builder;
//        }


//        public INamespaceUsing Using(string[] namespaceParts)
//        {
//            CodeGenUtils.AppendUsingNamespaceString(namespaceParts, m_StringBuilder);

//            return this;
//        }

//    }



//    public class NamespaceCreateCodeGen : INamespaceWrap
//    {
//        private StringBuilder m_StringBuilder { get; set; }
//        private ICodeGenBuilder m_Builder;
//        public NamespaceCreateCodeGen(StringBuilder stringBuilder, ICodeGenBuilder builder)
//        {
//            m_StringBuilder = stringBuilder;
//            m_Builder = builder;
//        }
//        public INamespaceWrap StartNamespace(string[] namespaceParts)
//        {
//            CodeGenUtils.AppendNamespacePartTop(m_StringBuilder, namespaceParts);

//            return this;
//        }


//        public ICodeGenBuilder EndNamespace()
//        {

//            m_StringBuilder.Append(Environment.NewLine);
//            m_StringBuilder.Append("}");
//            m_StringBuilder.Append(Environment.NewLine);
//            m_StringBuilder.Append(Environment.NewLine);
//            return m_Builder;

//        }

//        public INamespaceWrap CreateEnum(string enumName, string[] enumParams)
//        {

//            CodeGenUtils.CreateEnumPart(m_StringBuilder, enumName, enumParams, 1);
//            return this;
//        }
//    }
 
//    public class CodeGenBuilder : ICodeGenBuilder
//    {
//        private StringBuilder m_StringBuilder { get; set; }
//        private string m_SavePath;
//        private string m_Filename;
//        public CodeGenBuilder()
//        {
//            m_StringBuilder = new StringBuilder();
//            Reset();
//        }
//        public void Build()
//        {
//            CodeGenUtils.WriteCodeToFile(string.Format(CodeGenUtils.FilePathFormat, m_SavePath, m_Filename), m_StringBuilder);
//            Reset();
//        }

//        private void Reset()
//        {
//            m_StringBuilder.Clear();
//            m_StringBuilder.Append(CodeGenUtils.AutoGenTemplate);
//            m_StringBuilder.Append(Environment.NewLine);
//            m_StringBuilder.Append(Environment.NewLine);

//            m_SavePath = null;
//            m_Filename = null;
//        }


//        public INamespaceWrap ToCreateNamespace()
//        {
//            return new NamespaceCreateCodeGen(m_StringBuilder, this);
//        }

//        public INamespaceUsing ToUsingNamespace()
//        {
//            return new NamespaceUsingCodeGen(m_StringBuilder, this);
//        }

//        public ICodeGenBuilder SetPath(string saveFilePath)
//        {
//            m_SavePath = saveFilePath;
//            return this;
//        }

//        public ICodeGenBuilder SetFilename(string filename)
//        {
//            m_Filename = filename;
//            return this;
//        }
//    }


//    public static class CodeGenUtils
//    {
//        public const string AutoGenFormat =
//@"//-----------------------------------------------------------------------
//// This file is AUTO-GENERATED.
//// Changes for this script by hand might be lost when auto-generation is run.
//// (Generated date: {0})
////-----------------------------------------------------------------------";


//        /// <summary>
//        /// {0} - folder path
//        /// {1} - file name without format
//        /// </summary>
//        public const string FilePathFormat = "{0}/{1}.gen.cs";

//        // header
//        public static string AutoGenTemplate { get { return string.Format(AutoGenFormat, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")); } }

//        // namespace

//        private static Regex ValidProperty = new Regex(VALID_PROPERTY_NAME);
//        const string VALID_PROPERTY_NAME = @"^[a-zA-Z_]\w*(\.[a-zA-Z_]\w*)*$";
//        const string ERROR_STR = "CodeTemplates.CreateNameSpaceString has invalid namespaceParams value";

//        private static bool AllPropertyNamesAreValid(string[] propertyNames)
//        {
//            bool isCorrect = true;

//            if (propertyNames != null)
//            {
//                for (int i = 0; i < propertyNames.Length; i++)
//                {
//                    if (!ValidProperty.IsMatch(propertyNames[i]))
//                    {
//                        isCorrect = false;
//                        break;
//                    }
//                }
//            }
//            return isCorrect;
//        }

//        private static bool AllStringsAreDifferent(string[] propertyNames)
//        {
//            bool isCorrect = true;
//            for (int i = 0; i < propertyNames.Length; i++)
//            {
//                for (int j = 0; j < propertyNames.Length; j++)
//                {
//                    if (i == j)
//                        continue;

//                    if (propertyNames[i] == propertyNames[j])
//                        isCorrect = false;
//                }
//            }

//            return isCorrect;
//        }

//        private static bool PropertyNameIsValid(string propertyNames)
//        {
//            return ValidProperty.IsMatch(propertyNames);
//        }

//        private static void AppendNamespaceString(string[] namespaceParts, StringBuilder builder)
//        {
//            var result = CodeGenUtils.AllPropertyNamesAreValid(namespaceParts);

//            if (!result)
//                throw new ArgumentException("some parts of the namespace are not formatted correctly");

           
//            for (int i = 0; i < namespaceParts.Length; i++)
//            {
//                builder.Append(namespaceParts[i]);

//                if (namespaceParts.Length - 1 != i)
//                    builder.Append(".");
//            }
            
//        }

//        public static void AppendUsingNamespaceString(string[] namespaceParts, StringBuilder builder, int tabs = 0)
//        {
//            AppendTabs(builder, tabs);
//            builder.Append("using ");
//            AppendNamespaceString(namespaceParts, builder);
//            builder.Append(";");
//            builder.Append(Environment.NewLine);
//        }

//        public static void AppendCreateNamespaceString(string[] namespaceParts, StringBuilder builder, int tabs = 0)
//        {
//            AppendTabs(builder, tabs);
//            builder.Append("namespace ");
//            AppendNamespaceString(namespaceParts, builder);
//            builder.Append(Environment.NewLine);
//        }



//        private static void AppendTabs(StringBuilder builder, int tabs)
//        {
//            for (int i = 0; i < tabs; i++)
//            {
//                builder.Append("\t");
//            }
//        }

//        public static void AppendNamespacePartTop(StringBuilder stringBuilder, string[] namespaceParams, int tab = 0)
//        {

//            var isCorrect = AllPropertyNamesAreValid(namespaceParams);

//            var namespaceParamsLenZero = namespaceParams == null || namespaceParams.Length == 0 ? true : false;

//            if (!isCorrect)
//            {
//                throw new ArgumentException(ERROR_STR);
//            }


//            AppendCreateNamespaceString(namespaceParams, stringBuilder, tab);

//            AppendTabs(stringBuilder, tab);
//            stringBuilder.Append("{");

//            stringBuilder.Append(Environment.NewLine);

            


//        }


//        [MenuItem("CodeGen/Create enum")]
//        public static void CreateEnumTest()
//        {

//            if (!Directory.Exists("Assets/_CodeGen"))
//            {
//                Directory.CreateDirectory("Assets/_CodeGen");
//            }

//            var builder = new CodeGenBuilder();

//            builder
//                .SetPath("Assets/_CodeGen")
//                .SetFilename("NewGenFile")
//                .ToUsingNamespace()
//                .Using(new string[] { "System" })
//                .Using(new string[] { "System", "Linq" })
//                .EndUsing()
//                .ToCreateNamespace()             
//                .StartNamespace(new string[] { "CodeGen", "AI" })
//                .CreateEnum("AiEnum", new string[] { "Field1"})
//                .EndNamespace()
//                .ToCreateNamespace()
//                .StartNamespace(new string[] { "CodeGen", "AI" })
//                .CreateEnum("AiEnum2", new string[] { "Field1" })
//                .EndNamespace()
//                .Build();

//            builder
//              .SetPath("Assets/_CodeGen")
//              .SetFilename("NewGenFile2")
//              .ToUsingNamespace()
//              .Using(new string[] { "System" })
//              .Using(new string[] { "System", "Linq" })
//              .EndUsing()
//              .ToCreateNamespace()
//              .StartNamespace(new string[] { "CodeGen", "AI" })
//              .CreateEnum("AiEnum3", new string[] { "Field1" })
//              .EndNamespace()
//              .ToCreateNamespace()
//              .StartNamespace(new string[] { "CodeGen", "AI" })
//              .CreateEnum("AiEnum4", new string[] { "Field1" })
//              .EndNamespace()
//              .Build();


//            AssetDatabase.Refresh();
//        }

//        public static void CreateEnumPart(StringBuilder stringBuilder, string enumName, string[] enumParams, int tab)
//        {
//            stringBuilder.Append(Environment.NewLine);
//            for (var j = 0; j < tab; j++)
//            {
//                stringBuilder.Append("\t");
//            }
//            stringBuilder.Append($"public enum {enumName} ");
//            stringBuilder.Append(Environment.NewLine);
//            for (var j = 0; j < tab; j++)
//            {
//                stringBuilder.Append("\t");
//            }
//            stringBuilder.Append("{");

//            for (int i = 0; i < enumParams.Length; i++)
//            {
//                stringBuilder.Append(Environment.NewLine);
//                stringBuilder.Append("\t");
//                for (var j = 0; j < tab; j++)
//                {
//                    stringBuilder.Append("\t");
//                }
//                stringBuilder.Append(enumParams[i]);
//                stringBuilder.Append(",");
//            }
//            stringBuilder.Append(Environment.NewLine);

//            for (var j = 0; j < tab; j++)
//            {
//                stringBuilder.Append("\t");
//            }
//            stringBuilder.Append("}");

//            stringBuilder.Append(Environment.NewLine);
//        }


//        public static void WriteCodeToFile(string path, StringBuilder builder)
//        {

//            try
//            {


//                // Always create a new file because overwriting to existing file may generate mal-formatted script.
//                // for instance, when the number of tags is reduced, last tag will be remain after the last curly brace in the file.
//                using (FileStream stream = File.Open(path, FileMode.Create, FileAccess.Write))
//                {
//                    using (StreamWriter writer = new StreamWriter(stream))
//                    {

//                        writer.Write(builder.ToString());
//                    }
//                }
//            }
//            catch (System.Exception e)
//            {
//                Debug.LogException(e);

//            }

//            AssetDatabase.Refresh();
//        }
//    }

//}

