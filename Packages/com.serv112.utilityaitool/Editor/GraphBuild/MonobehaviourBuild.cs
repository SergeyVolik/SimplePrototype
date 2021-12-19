using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{


    public class MonobehaviourBuild : BaseBuild
    {

        public MonobehaviourBuild(AIGraphAssetModel asset) : base(asset)
        {

        }

        void MonoBehaviourBuild()
        {

            //var Namespace = m_AssetModel.Namespace;

            //var processors = m_AssetModel.GraphModel.NodeModels.OfType<AIProcessorNodeModel>().ToList();

            //for (int i = 0; i < processors.Count; i++)
            //{
            //    var nameNumber = 1;
            //    var enumTypes = new List<ActionParts>();
            //    var stateGroups = processors[i].GetConnectedNodes(PortDirection.Input, PortType.Data).OfType<StateGroupNodeModel>().ToList();

            //    stateGroups.ForEach(e => {

            //        e.GenereteStateGroup(pathWithScripts);
            //        enumTypes.Add(new ActionParts { Name = e.FieldName, EnumType = e.Name });
            //        nameNumber++;
            //    });

            //    var varibales = m_AssetModel.GraphModel.VariableDeclarations.ToList();

            //    var properties = new List<PropertyParts>();
            //    for (int j = 0; j < varibales.Count; j++)
            //    {
            //        properties.Add(new PropertyParts
            //        {
            //            Name = varibales[j].GetVariableName(),
            //            RageAttribut = new Range
            //            {
            //                Max = 100,
            //                Min = 0
            //            }
            //        });
            //    }




            //    var AiProcessorSettinsName = processors[i].Name;
            //    var AiProcessorSettins = new CreateAIProcessorSettings
            //    {
            //        Name = AiProcessorSettinsName,
            //        Namespace = Namespace,
            //        Parent = "AIGraphProcessor",
            //        Attributes = new List<string> { "DisallowMultipleComponent" },
            //        PropertyPartsOfCode = properties,
            //        ActionPartsOfCode = enumTypes

            //    };

            //    T4GenUtils.CreateMonoBehaviourAIProcessor(pathWithScripts, AiProcessorSettinsName, AiProcessorSettins);

            //    var @params = new List<string>();

            //    for (int j = 0; j < enumTypes.Count; j++)
            //    {
            //        @params.Add(enumTypes[i].Name);
            //        @params.Add($"Event{enumTypes[i].Name}");
            //    }
            //    for (int j = 0; j < properties.Count; j++)
            //    {
            //        @params.Add(properties[i].Name);
            //    }


            //    var editorInspector = $"{AiProcessorSettinsName}Inspector";

            //    var AIProcessorInspector = new CreateAIProcessorInspectorSettings
            //    {
            //        Namespace = Namespace,
            //        Name = editorInspector,
            //        Parent = "Editor",
            //        Attributes = new List<string> { $"CustomEditor(typeof({AiProcessorSettinsName})) ", "CanEditMultipleObjects" },
            //        ErrorMessage = $"This script generated for specific AI Graph asset. Build guid: {m_AssetModel.CodeGenGuid}, asset path: {AssetPath}",
            //        SerializedProperties = @params,
            //        TargetGuid = m_AssetModel.CodeGenGuid,
            //        TargetClass = AiProcessorSettinsName


            //    };

            //    T4GenUtils.CreateMonoBehaviourAIProcessorInspector(pathWithEditorScripts, editorInspector, AIProcessorInspector);


            //}


            //State = AIGraphBuidState.AfterReimport;
          

            HLSLGenerator generator = new HLSLGenerator(m_AssetModel, pathWithScripts);
            generator.GenerateAndSave();
            AssetDatabase.Refresh();

        }

        public override void Build()
        {
            MonoBehaviourBuild();
        }
    }
}
