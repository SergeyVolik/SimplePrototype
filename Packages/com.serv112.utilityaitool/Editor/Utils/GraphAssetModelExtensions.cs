using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    public static class GraphAssetModelExtensions
    {
        public static string GetFilename(this IGraphAssetModel model)
        {
            return Path.GetFileName(model.GetPath());
        }

        public static string GetDirectoryName(this IGraphAssetModel model)
        {
            
            return Path.GetDirectoryName(model.GetPath());
        }
    }
}
