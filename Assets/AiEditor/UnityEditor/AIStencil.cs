using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEditor.GraphToolsFoundation.Overdrive.Plugins.Debugging;
using UnityEditor.GraphToolsFoundation.Searcher;
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

        public override IEnumerable<IPluginHandler> GetGraphProcessingPluginHandlers(GraphProcessingOptions getGraphProcessingOptions)
        {
            if (getGraphProcessingOptions.HasFlag(GraphProcessingOptions.Tracing))
            {
                if (m_DebugInstrumentationHandler == null)
                    m_DebugInstrumentationHandler = new DebugInstrumentationHandler();

                yield return m_DebugInstrumentationHandler;
            }
        }
        public override void OnGraphProcessingStarted(IGraphModel graphModel)
        {
            Debug.Log("OnGraphProcessingStarted");
        }
        public override void OnGraphProcessingSucceeded(IGraphModel graphModel, GraphProcessingResult results)
        {
            Debug.Log("OnGraphProcessingSucceeded");
        }
        public override void OnGraphProcessingFailed(IGraphModel graphModel, GraphProcessingResult results)
        {
            Debug.Log("OnGraphProcessingFailed");
        }
        public override IEnumerable<INodeModel> GetEntryPoints()
        {
            return Enumerable.Empty<INodeModel>();
        }

        public override string GetNodeDocumentation(SearcherItem node, IGraphElementModel model) 
        {
            if (model is StateGroupNodeModel stateModel)
            {
                return "It's a node for AI state";
            }

            return null;
        }

    }

    
}
