using System;
using System.Linq;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
 
    public class AIStencil : Stencil
    {
        public static string toolName = "AI Editor2";

        public override string ToolName => toolName;

        public static readonly string graphName = "AI Editor Graph";


        public override IToolbarProvider GetToolbarProvider()
        {
            return m_ToolbarProvider ??= new AIToolbarProvider();
        }
        public override IBlackboardGraphModel CreateBlackboardGraphModel(IGraphAssetModel graphAssetModel)
        {
            return new AIBlackboardGraphModel(graphAssetModel);
        }
        public override Type GetConstantNodeValueType(TypeHandle typeHandle)
        {
            return TypeToConstantMapper.GetConstantNodeType(typeHandle);
        }

        public override void PopulateBlackboardCreateMenu(string sectionName, GenericMenu menu, CommandDispatcher commandDispatcher)
        {

            if (sectionName == AIBlackboardGraphModel.k_Sections[0])
            {
                menu.AddItem(new GUIContent("Input AI Params"), false, () =>
                {
                    const string newItemName = "variable";
                    var finalName = newItemName;
                    var i = 0;
                    while (commandDispatcher.State.WindowState.GraphModel.VariableDeclarations.Any(v => v.Title == finalName))
                        finalName = newItemName + i++;

                    commandDispatcher.Dispatch(new CreateGraphVariableDeclarationCommand(finalName, true, TypeHandle.Float, typeof(FloatVariableDeclarationModel)));
                });
            }


        }

      
    }
}
