using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;

namespace SerV112.UtilityAIEditor
{


    public class MonobehaviourGPUBuild : BaseBuild
    {

        public MonobehaviourGPUBuild(AIGraphModel GraphModel) : base(GraphModel)
        {

        }

        //TODO: auto fix scripts. Do I Have to refactor scripts automatically?
        //WARNING: Naive implementation!
        private void AutoScriptsFix(List<string> prevTypes, List<string> newTypes)
        {
            var dir = Application.dataPath;
            Directory
                 .EnumerateFiles(dir, "*.cs", SearchOption.AllDirectories).ToList().ForEach(e =>
                 {
                     string text = File.ReadAllText(e);

                     for (int i = 0; i < newTypes.Count; i++)
                     {
                         text = text.Replace(prevTypes[i], newTypes[i]);
                     }

                     File.WriteAllText(e, text);
                 });
        }
        private void DeletePrevEnums()
        {
            m_AssetModel.LastGeneratedEnumsNames.ForEach(e =>
            {
                File.Delete(string.Join("/", pathWithScripts, $"{e}.cs"));
                File.Delete(string.Join("/", pathWithScripts, $"{e}.cs.meta"));
            });
        }


        private void RenamePrevScripts(string AISimulationName, string AiAgentName, string hlslComputeShader)
        {
            try
            {
                string prevName;
                string newName;
                if (!string.IsNullOrEmpty(m_AssetModel.LastGeneratedMonoAgent) && AISimulationName != m_AssetModel.LastGeneratedMonoAgent)
                {
                    prevName = string.Join("/", pathWithScripts, $"{m_AssetModel.LastGeneratedMonoAgent}.cs");
                    newName = string.Join("/", pathWithScripts, $"{AiAgentName}.cs");
                    File.Move(prevName, newName);
                    prevName = string.Join("/", pathWithScripts, $"{m_AssetModel.LastGeneratedMonoAgent}.cs.meta");
                    newName = string.Join("/", pathWithScripts, $"{AiAgentName}.cs.meta");
                    File.Move(prevName, newName);
                    prevName = string.Join("/", pathWithScripts, $"{m_AssetModel.LastGeneratedMonoSimulation}.cs.meta");
                    newName = string.Join("/", pathWithScripts, $"{AISimulationName}.cs.meta");
                    File.Move(prevName, newName);
                    prevName = string.Join("/", pathWithScripts, $"{m_AssetModel.LastGeneratedMonoSimulation}.cs");
                    newName = string.Join("/", pathWithScripts, $"{AISimulationName}.cs");
                    File.Move(prevName, newName);
                    prevName = string.Join("/", pathWithResourcesScripts, $"{m_AssetModel.LastGeneratedHlsl}.compute");
                    newName = string.Join("/", pathWithResourcesScripts, $"{hlslComputeShader}.compute");
                    File.Move(prevName, newName);
                    prevName = string.Join("/", pathWithResourcesScripts, $"{m_AssetModel.LastGeneratedHlsl}.compute.meta");
                    newName = string.Join("/", pathWithResourcesScripts, $"{hlslComputeShader}.compute.meta");
                    File.Move(prevName, newName);

                    //TODO: auto fix scripts. Do I Have to refactor scripts automatically?
                    //AutoScriptsFix(new List<string> {
                    //    m_AssetModel.LastGeneratedMonoSimulation,
                    //    m_AssetModel.LastGeneratedMonoAgent
                    //},
                    //new List<string> {
                    //    AISimulationName,
                    //    AiAgentName
                    //});
                }
            }
            catch (IOException ex)
            {

                Debug.LogError(ex.Message);
            }

        }

        void MonoBehaviourBuild()
        {


            var processor = m_AIGraphModel.Stencil.GetEntryPoints().OfType<AIProcessorNodeModel>().ToList()[0];

            var AiAgentName = "AIAgent" + processor.Name;
            var AISimulationName = "AISimulation" + processor.Name;
            var AiAgentInspectorName = "Inspector" + AiAgentName;
            var AiSimulationInspectorName = "Inspector" + AISimulationName;

            DeletePrevEnums();
            RenamePrevScripts(AISimulationName, AiAgentName, processor.Name);


            m_AssetModel.LastGeneratedHlsl = processor.Name;
            m_AssetModel.LastGeneratedMonoAgent = AiAgentName;
            m_AssetModel.LastGeneratedMonoSimulation = AISimulationName;

            var resultsnames = new List<string>();
            var StateGroups = processor.GetConnectedNodes(PortDirection.Input, PortType.Data).OfType<StateGroupNodeModel>().ToList();


            StateGroups.ForEach(e =>
            {
                resultsnames.Add(e.Name);

                var enumNames = new List<string>();

                e.GetConnectedNodes(PortDirection.Input, PortType.Data).OfType<StateNodeModel>().ToList().ForEach(s =>
                {
                    enumNames.Add(s.Name);
                });

                T4GenUtils.CreateEnum(pathWithScripts, e.Name, new CreateEnumSettings(e.Name, enumNames));
            });

            HLSLGenerator generator = new HLSLGenerator(m_AIGraphModel.Stencil as AIStencil, m_AssetModel, pathWithResourcesScripts, processor.Name);
            generator.GenerateAndSave();


            var properties = generator.VariablesReadonly.ToList();
            var propertiesNames = properties.Select(e => e.Name).ToList();

            m_AssetModel.LastGeneratedEnumsNames = resultsnames;
            var fields = resultsnames.Select(e => new FieldData { Name = e, Type = e }).ToList();






            var varNames = m_AssetModel.GraphModel.VariableDeclarations.Select(e => e.Title).ToList();
            var except = varNames.Except(propertiesNames).ToList();


            List<BoxMessage> Messages = new List<BoxMessage>();
            for (int i = 0; i < except.Count; i++)
            {
                var message = $"The '{except[i]}' variable hasn't been shown that's because it doesn't have impact on a simulation.";
                Messages.Add(new BoxMessage { Message = message, MessageType = MessageType.Warning });
            }

            var message1 = "Release Mode. Max performance! Inspector results fields won't be updating in a play mode.";
            if (m_AssetModel.Debug)
            {
                message1 = "Debug Mode. Inspector results fields will be updating in a play mode.";
            }
            Messages.Add(new BoxMessage { Message = message1, MessageType = MessageType.Info });

            var AISimulation = new UtilityAISimulationSettings
            {
                AISimulationClassName = AISimulationName,
                AISimulationInspectorClassName = AiSimulationInspectorName,
                AIAgentClassName = AiAgentName,
                AIAgentInspectorClassName = AiAgentInspectorName,
                ComputeShaderResourcePath = processor.Name,
                Results = fields,
                Properties = properties,
                CodeGenMessage = "",
                AiAgentInspector = new AIAgentInspectorSettings { BoxMessages = Messages },
                Debug = m_AssetModel.Debug

            };

            T4GenUtils.CreateUtilityAISimulationMonoScript(pathWithScripts, AISimulationName, AISimulation);
            T4GenUtils.CreateUtilityAIAgentMonoScript(pathWithScripts, AiAgentName, AISimulation);



            State = AIGraphBuidState.AfterReimport;
            AssetDatabase.Refresh();



        }

        public override void Build()
        {
            MonoBehaviourBuild();
        }
    }
}
