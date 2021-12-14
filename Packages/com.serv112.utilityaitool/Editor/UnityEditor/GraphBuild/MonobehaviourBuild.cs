using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace SerV112.UtilityAIEditor
{


    public class MonobehaviourBuild : BaseBuild
    {

        public MonobehaviourBuild(AIGraphAssetModel asset) : base(asset)
        {

        }

        void MonoBehaviourBuild()
        {
            var enumTypes = new List<ActionParts>();
            var nameNumber = 1;
            m_AssetModel.GraphModel.NodeModels.OfType<StateGroupNodeModel>().ToList().ForEach(e => {

                e.GenereteStateGroup(pathWithScripts);
                enumTypes.Add(new ActionParts { Name = $"Action{nameNumber}", EnumType = e.Name });
                nameNumber++;
            });

            var Namespace = m_AssetModel.Namespace;
            var varibales = m_AssetModel.GraphModel.VariableDeclarations.ToList();

            var properties = new List<PropertyParts>();
            for (int i = 0; i < varibales.Count; i++)
            {
                properties.Add(new PropertyParts
                {
                    Name = varibales[i].GetVariableName(),
                    RageAttribut = new Range
                    {
                        Max = 100,
                        Min = 0
                    }
                });
            }




            var AiProcessorSettinsName = "AiProcessorNew";
            var AiProcessorSettins = new CreateAIProcessorSettings
            {
                Name = AiProcessorSettinsName,
                Namespace = Namespace,
                Parent = "AIGraphProcessor",
                Attributes = new List<string> { "DisallowMultipleComponent" },
                PropertyPartsOfCode = properties,
                ActionPartsOfCode = enumTypes

            };

            T4GenUtils.CreateMonoBehaviourAIProcessor(pathWithScripts, AiProcessorSettinsName, AiProcessorSettins);

            var @params = new List<string>();

            for (int i = 0; i < enumTypes.Count; i++)
            {
                @params.Add(enumTypes[i].Name);
                @params.Add($"Event{enumTypes[i].Name}");
            }
            for (int i = 0; i < properties.Count; i++)
            {
                @params.Add(properties[i].Name);
            }

            
            var editorInspector = $"{AiProcessorSettinsName}Inspector";
            
            var AIProcessorInspector = new CreateAIProcessorInspectorSettings
            {
                Namespace = Namespace,
                Name = editorInspector,
                Parent = "Editor",
                Attributes = new List<string> { $"CustomEditor(typeof({AiProcessorSettinsName})) ", "CanEditMultipleObjects" },
                ErrorMessage = $"This script generated for specifyc AI Graph asset build guid: {m_AssetModel.CodeGenGuid} asset path: {AssetPath}",
                SerializedProperties = @params,
                TargetGuid = m_AssetModel.CodeGenGuid,
                TargetClass = AiProcessorSettinsName


            };

            T4GenUtils.CreateMonoBehaviourAIProcessorInspector(pathWithEditorScripts, editorInspector, AIProcessorInspector);

            State = AIGraphBuidState.AfterReimport;
            AssetDatabase.Refresh();
        }

        public override void Build()
        {
            MonoBehaviourBuild();
        }
    }
}
