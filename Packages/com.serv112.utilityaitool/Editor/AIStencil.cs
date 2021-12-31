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

    public class NormalizedFloatConstant : Constant<NormalizedFloat> { }

   
    public class AIStencil : Stencil
    {
        public static string toolName = "AI Editor2";

        public override string ToolName => toolName;

        public static readonly string graphName = "AI Editor Graph";

        private AIGraphDebugger m_Debugger; 
        public override IDebugger Debugger => m_Debugger ??= new AIGraphDebugger();
        public override IToolbarProvider GetToolbarProvider() =>  m_ToolbarProvider ??= new AIToolbarProvider();       
        public override IBlackboardGraphModel CreateBlackboardGraphModel(IGraphAssetModel graphAssetModel) => new AIBlackboardGraphModel(graphAssetModel);
       
        public override void PopulateBlackboardCreateMenu(string sectionName, GenericMenu menu, CommandDispatcher commandDispatcher)
        {

            if (sectionName == AIBlackboardGraphModel.k_Sections[0])
            {
                menu.AddItem(new GUIContent("Input AI Params"), false, () =>
                {
                    const string newItemName = "NormalizedValue";
                    var finalName = newItemName;
                    var i = 0;
                    while (commandDispatcher.State.WindowState.GraphModel.VariableDeclarations.Any(v => v.Title == finalName))
                        finalName = newItemName + i++;

                    commandDispatcher.Dispatch(
                        new CreateGraphVariableDeclarationCommand(
                            finalName,
                            true,
                            AIGraphCustomTypes.NormalizedFloat,
                            typeof(NormalizedFloatVariableDeclarationModel))
                        );
                });
            }



        }


        public override IGraphProcessor CreateGraphProcessor() => new GraphCodeGenProcessor();

        static Dictionary<TypeHandle, Type> s_TypeToConstantNodeModelTypeCache;

        public override Type GetConstantNodeValueType(TypeHandle typeHandle)
        {
            if (s_TypeToConstantNodeModelTypeCache == null)
            {
                s_TypeToConstantNodeModelTypeCache = new Dictionary<TypeHandle, Type>
                {
                    { AIGraphCustomTypes.NormalizedFloat, typeof(NormalizedFloatConstant) },

                };
            }

            if (s_TypeToConstantNodeModelTypeCache.TryGetValue(typeHandle, out var type))
            {
                return type;
            }

            return TypeToConstantMapper.GetConstantNodeType(typeHandle);
        }

        public override IEnumerable<IPluginHandler> GetGraphProcessingPluginHandlers(GraphProcessingOptions getGraphProcessingOptions)
        {
            if (getGraphProcessingOptions.HasFlag(GraphProcessingOptions.Tracing))
            {
                if (m_DebugInstrumentationHandler == null)
                    m_DebugInstrumentationHandler = new DebugInstrumentationHandler();

                yield return m_DebugInstrumentationHandler;
            }

            yield return new MyCustomPluginHandler();
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
            var entries = GraphModel.NodeModels.OfType<AIProcessorNodeModel>();

            return entries;
        }

        public override string GetNodeDocumentation(SearcherItem node, IGraphElementModel model) 
        {     
            return $"Description {model}";
        }

       
    }

    
}
