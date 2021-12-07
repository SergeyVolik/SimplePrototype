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

        public static TypeHandle Namespace { get; } = TypeHandleHelpers.GenerateTypeHandle(typeof(string));
        public static TypeHandle NormalizedFloat { get; } = TypeHandleHelpers.GenerateTypeHandle(typeof(float));
        public static TypeHandle AIAction { get; } = TypeHandleHelpers.GenerateCustomTypeHandle("AIAction");
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

            if (sectionName == AIBlackboardGraphModel.k_Sections[1])
            {
                menu.AddItem(new GUIContent("Namespaces"), false, () =>
                {
                    const string newItemName = "namespace";
                    var finalName = newItemName;
                    var i = 0;
                    while (commandDispatcher.State.WindowState.GraphModel.VariableDeclarations.Any(v => v.Title == finalName))
                        finalName = newItemName + i++;

                    commandDispatcher.Dispatch(new CreateGraphVariableDeclarationCommand(finalName, true, AIStencil.Namespace, typeof(NamespaceVariableDeclarationModel)));
                });
            }


        }

        public override IGraphProcessor CreateGraphProcessor()
        {
            return new GraphCodeGenProcessor();
        }

       


    }

    class GraphCodeGenProcessor : IGraphProcessor
    {
        public GraphProcessingResult ProcessGraph(IGraphModel graphModel)
        {
            var res = new GraphProcessingResult();
            //res.AddError("Error");
            CheckDuplicatedNames(res, graphModel);
            return res;
        }

        private void CheckDuplicatedNames(GraphProcessingResult res, IGraphModel graphModel)
        {
            
            var stateNodeModels = graphModel.NodeModels
                .OfType<StateNodeModel>()
                .ToList();


            for (int i = 0; i < stateNodeModels.Count; i++)
            {
                for (int j = 0; j < stateNodeModels.Count; j++)
                {
                    if (i == j)
                        continue;

                    if (stateNodeModels[i].Name == stateNodeModels[j].Name)
                        res.AddError($"a graph contains  two or more StateNodeModel with Name {stateNodeModels[i].Name}", stateNodeModels[i]);
                }
            }

        }
    }
}
