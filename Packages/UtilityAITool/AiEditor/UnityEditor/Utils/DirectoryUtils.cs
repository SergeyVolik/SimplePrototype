using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerV112.UtilityAIEditor
{
    

    public static class DirectoryUtils
    {

        public const string DefaultPath = "Packages/com.serv112.utilityaitool";

        public static bool FileExisted(string[] fileEntries, string fileName)
        {

            for (int i = 0; i < fileEntries.Length; i++)
            {
                var fileWithoutExtention = Path.GetFileName(fileEntries[i]).Split('.')[0];

                if (fileWithoutExtention == fileName)
                    return true;
            }
            return false;
        }

    }
}
