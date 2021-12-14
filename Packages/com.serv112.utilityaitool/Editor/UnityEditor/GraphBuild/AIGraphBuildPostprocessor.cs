using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace SerV112.UtilityAIEditor
{
    class AIGraphBuildPostprocessor : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {

            BaseBuild.CollectAssetRefs(importedAssets, BuildAIEditorCommand.Build);
        }
    }
}
