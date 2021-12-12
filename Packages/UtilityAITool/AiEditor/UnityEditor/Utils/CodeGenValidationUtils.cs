using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{

    public static class CodeGenValidationUtils
    {
        public static void Validate(string[] namesOfClasses, string scriptGenFolderPath, string @namespace, GraphProcessingResult res)
        {
            if (AssetDatabase.IsValidFolder(scriptGenFolderPath))
            {
                string[] fileEntries = Directory.GetFiles(scriptGenFolderPath);

                for (int i = 0; i < namesOfClasses.Length; i++)
                {
                    var existed = DirectoryUtils.FileExisted(fileEntries, $"{namesOfClasses[i]}");
                    var typeExisted = AssemblyUtils.CheckIfTypeNameExisted(namesOfClasses[i], @namespace);

                    if (!existed && typeExisted)
                    {
                        res.AddError($"{namesOfClasses[i]} class existed and located out from {scriptGenFolderPath} folder!");
                     
                        
                    }

                }
            }

        }

    }

}
