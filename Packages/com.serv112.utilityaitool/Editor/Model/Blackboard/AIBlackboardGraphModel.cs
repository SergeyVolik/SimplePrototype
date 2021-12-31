using System.Collections.Generic;
using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    public class AIBlackboardGraphModel : BlackboardGraphModel
    {
        public static readonly string[] k_Sections = { /*"Ingredients", "Cookware",*/ "Out Variables"  };
            
        /// <inheritdoc />
        public AIBlackboardGraphModel(IGraphAssetModel graphAssetModel)
            : base(graphAssetModel) { }

        public override string GetBlackboardTitle()
        {
            return AssetModel?.FriendlyScriptName == null ? "behaviour" : AssetModel?.FriendlyScriptName + " behaviour";
        }

        private string k_SubTitle = "AI Params";
        public override string GetBlackboardSubTitle()
        {
            return k_SubTitle;
        }

        public override IEnumerable<string> SectionNames =>
            GraphModel == null ? Enumerable.Empty<string>() : k_Sections;

        public override IEnumerable<IVariableDeclarationModel> GetSectionRows(string sectionName)
        {

            if (sectionName == k_Sections[0])
            {
                return GraphModel?.VariableDeclarations ??
                    Enumerable.Empty<IVariableDeclarationModel>();
            }


            return Enumerable.Empty<IVariableDeclarationModel>();
        }
    }
}
