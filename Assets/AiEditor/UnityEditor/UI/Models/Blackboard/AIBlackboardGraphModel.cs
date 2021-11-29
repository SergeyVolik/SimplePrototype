using System.Collections.Generic;
using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    public class AIBlackboardGraphModel : BlackboardGraphModel
    {
        internal static readonly string[] k_Sections = { /*"Ingredients", "Cookware",*/ "Variables",  };
            
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
            //if (sectionName == k_Sections[0])
            //{
            //    return GraphModel?.VariableDeclarations?.Where(v => v.DataType == AIStencil.Ingredient) ??
            //        Enumerable.Empty<IVariableDeclarationModel>();
            //}

            //if (sectionName == k_Sections[1])
            //{
            //    return GraphModel?.VariableDeclarations?.Where(v => v.DataType == AIStencil.Cookware) ??
            //        Enumerable.Empty<IVariableDeclarationModel>();
            //}

            if (sectionName == k_Sections[0])
            {
                return GraphModel?.VariableDeclarations?.Where(v => v.DataType == TypeHandle.Float) ??
                    Enumerable.Empty<IVariableDeclarationModel>();
            }

            return Enumerable.Empty<IVariableDeclarationModel>();
        }
    }
}
