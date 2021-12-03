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
        public static bool FileExisted(string pathWithScripts, string fileName)
        {

            string[] fileEntries = Directory.GetFiles(pathWithScripts);

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
