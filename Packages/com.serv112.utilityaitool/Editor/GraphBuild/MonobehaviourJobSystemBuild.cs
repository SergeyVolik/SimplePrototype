using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;

namespace SerV112.UtilityAIEditor
{

    [Serializable]
    public class JobSystemAiSimulationSettins
    {
        public string SimulationName;
        public string AgentName;
        public string SimDataSOName;
        public string JobName;
        public string InDataType;
        public string OutDataType;
    }



    public class MonobehaviourJobSystemBuild : BaseBuild
    {

        public MonobehaviourJobSystemBuild(AIGraphModel GraphModel) : base(GraphModel)
        {

        }

        void MonoBehaviourBuild()
        {
            var processor = m_AIGraphModel.Stencil.GetEntryPoints().OfType<AIProcessorNodeModel>().ToList()[0];

            var AiAgentName = "AIAgent" + processor.Name;
            var AISimulationName = "AISimulation" + processor.Name;
            var JobName = "Job" + processor.Name;
            var AISimulationDataSO = processor.Name + "DataSO";

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

            JobSystemCodeGen generator = new JobSystemCodeGen(m_AssetModel, m_AIGraphModel.Stencil as AIStencil, JobName, pathWithScripts, resultsnames, processor.Name);
            generator.GenerateAndSave();

         

           

            JobSystemAiSimulationSettins data = new JobSystemAiSimulationSettins()
            {
                 AgentName = AiAgentName,
                 JobName = JobName,
                 SimulationName = AISimulationName,
                 SimDataSOName = AISimulationDataSO,
                 InDataType = generator.InAgentDataName,
                 OutDataType = generator.OutAgentDataName

            };

            T4GenUtils.CreateUtilityJobAISimulationMonoScript(pathWithScripts, AISimulationName, data);
            T4GenUtils.CreateUtilityJobAIAgentMono(pathWithScripts, AiAgentName, data);
            T4GenUtils.CreateUtilityJobAISimulationDataSO(pathWithScripts, AISimulationDataSO, data);
            
            State = AIGraphBuidState.AfterReimport;
            AssetDatabase.Refresh();

        }

        public override void Build()
        {
            MonoBehaviourBuild();
        }
    }
}
