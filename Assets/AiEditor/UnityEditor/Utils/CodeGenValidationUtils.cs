using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace SerV112.UtilityAIEditor
{

    public static class CodeGenValidationUtils
    {
        public static void Validate(string[] namesOfClasses, string scriptGenFolderPath, string @namespace)
        {
            if (AssetDatabase.IsValidFolder(scriptGenFolderPath))
            {
                string[] fileEntries = Directory.GetFiles(scriptGenFolderPath);

                for (int i = 0; i < namesOfClasses.Length; i++)
                {
                    var existed = DirectoryUtils.FileExisted(fileEntries, $"{namesOfClasses}.cs");
                    var typeExisted = AssemblyUtils.CheckIfTypeNameExisted(namesOfClasses[i], @namespace);

                    if (!existed && typeExisted)
                    {
                        throw new Exception($"{namesOfClasses[i]}.gen.cs didn't create that's because a similar class located out from {scriptGenFolderPath} folder ");
                    }

                }
            }

        }

    }

}
