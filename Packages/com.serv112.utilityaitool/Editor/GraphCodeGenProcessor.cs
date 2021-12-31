using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;


namespace SerV112.UtilityAIEditor
{

    public static class NodeExtentions
    {
        public static IEnumerable<INodeModel> GetConnectedNodes(this NodeModel model, PortDirection dir, PortType type)
        {
            List<INodeModel> connectedNodes = new List<INodeModel>();
            var ports = model.GetPorts(dir, type).ToList();

            for (int i = 0; i < ports.Count; i++)
            {
                var edges = ports[i].GetConnectedEdges().ToList();

                for (int j = 0; j < edges.Count; j++)
                {
                    connectedNodes.Add(edges[j].FromPort.NodeModel);
                }
            }

            return connectedNodes;
        }

        public static IEnumerable<INodeModel> GetConnectedInputDataNodes(this NodeModel model)
        {
            List<INodeModel> connectedNodes = new List<INodeModel>();
            var ports = model.GetPorts(PortDirection.Input, PortType.Data).ToList();

            for (int i = 0; i < ports.Count; i++)
            {
                var edges = ports[i].GetConnectedEdges().ToList();

                for (int j = 0; j < edges.Count; j++)
                {
                    connectedNodes.Add(edges[j].FromPort.NodeModel);
                }
            }

            return connectedNodes;
        }

    }
    class GraphCodeGenProcessor : IGraphProcessor
    {
        private static CodeDomProvider provider = CodeDomProvider.CreateProvider("C#");
        public GraphProcessingResult ProcessGraph(IGraphModel graphModel)
        {
            var res = new GraphProcessingResult();

            Debug.Log("GraphCodeGenProcessor");
            ValidateErrors(res, graphModel);

            return res;
        }

        private void ValidateErrors(GraphProcessingResult res, IGraphModel graphModel)
        {
            CheckDuplicatedNames(res, graphModel);
            ValidateNames(res, graphModel);
            CheckExistedFilesWithNodesNames(res, graphModel);
            ValidateNamespace(res, graphModel);
        }



        private void ValidateNames(GraphProcessingResult res, IGraphModel graphModel)
        {
            ValidateVariablesNames(res, graphModel);
            ValidateOuterVariablesNames(res, graphModel);
        }

        private void ValidateNamespace(GraphProcessingResult res, IGraphModel graphModel)
        {
          

            var model = graphModel.AssetModel as AIGraphAssetModel;
            if (string.IsNullOrEmpty(model.Namespace))
                return;
            var @namespace = model.Namespace.Split('.');

         
            for (int i = 0; i < @namespace.Length; i++)
            {
                if (!provider.IsValidIdentifier(@namespace[i]))
                {
                    res.AddError($"Graph has an invalid namespace. Please fix it in a graph settings window {ValidationValiableRules}");
                    break;
                }
            }
        }

        private void CheckExistedFilesWithNodesNames(GraphProcessingResult res, IGraphModel graphModel)
        {
            var asset = graphModel.AssetModel as AIGraphAssetModel;

            var path = asset.GetDirectoryName();
            var @namespace = asset.Namespace;

            var namesOfClasses = new List<string>();

            graphModel.VariableDeclarations.ToList().ForEach(e =>
            {
                namesOfClasses.Add(e.Title);
            });

            var pathWithScripts = string.Join("/", path, "_CodeGen");

            CodeGenValidationUtils.Validate(namesOfClasses.ToArray(), pathWithScripts, @namespace, res);
        }
        private void ValidateVariablesNames(GraphProcessingResult res, IGraphModel graphModel)
        {

            graphModel.NodeModels
                .OfType<INameable>()
                .ToList().ForEach(e =>
                {

                    if (!CheckName(e.Name))
                    {

                        string typeName = "Type Not Detected";
                        typeName = e.GetType().Name;

                        res.AddError($"{typeName} node has an invalid name: {e.Name}{Environment.NewLine}{ValidationValiableRules}");
                    }
                });
        }

        private bool CheckName(string name)
        {

            if (provider.IsValidIdentifier(name))
            {
                return true;
            }

            return false;

        }

        const string ValidationValiableRules =
@"1. The only allowed characters for identifiers are all alphanumeric characters([A-Z], [a-z], [0-9]),
‘_‘ (underscore). example “geek@” is not a valid C# identifier as it contain‘@’ – special character.
Identifiers should not start with digits([0-9]).
2. For example “123geeks” is a not a valid in C# identifier. 
3. Identifiers should not contain white spaces.";
        private void ValidateOuterVariablesNames(GraphProcessingResult res, IGraphModel graphModel)
        {

            graphModel.VariableDeclarations
            .ToList().ForEach(e =>
            {
                if (!CheckName(e.Title))
                {
                    res.AddError($"Outer varibale has an invalid name: {e.Title}{Environment.NewLine}{ValidationValiableRules}");
                }
            });


        }

        private void CheckDuplicatedNames(GraphProcessingResult res, IGraphModel graphModel)
        {
            CheckDuplicatedScriptNames(res, graphModel);
            CheckDuplicatedEnumScriptNames(res, graphModel);
        }
        private void CheckDuplicatedScriptNames(GraphProcessingResult res, IGraphModel graphModel)
        {

            var stateNodeModels = graphModel.NodeModels
                .OfType<IScriptName>()
                .ToList();

            var decModels = graphModel.VariableDeclarations
                .OfType<IScriptName>()
                .ToList();

            stateNodeModels = stateNodeModels.Concat(decModels).ToList();

            for (int i = 0; i < stateNodeModels.Count; i++)
            {
                for (int j = 0; j < stateNodeModels.Count; j++)
                {
                    if (i == j)
                        continue;

                    if (stateNodeModels[i].Name == stateNodeModels[j].Name)
                    {

                        var elem = stateNodeModels[i];

                        if (stateNodeModels[i] is StateGroupNodeModel sgnm)
                        {
                            res.AddError($"a graph contains  two or more StateNodeModel with Name: {stateNodeModels[i].Name}", sgnm, new QuickFix("Add to name 1", (cd) =>
                            {
                                cd.Dispatch(new SetNameCommand(elem.Name + "1", elem));
                            }));
                        }
                        else if (stateNodeModels[i] is IVariableDeclarationModel vd)
                        {
                            res.AddError($"a graph contains  two or more scripts with Name: {stateNodeModels[i].Name}");

                            
                        }

                    }
                }
            }

        }

        
        private void CheckDuplicatedEnumScriptNames(GraphProcessingResult res, IGraphModel graphModel)
        {

            var stateNodeModels = graphModel.NodeModels
                .OfType<StateGroupNodeModel>()
                .ToList();

            for (int k = 0; k < stateNodeModels.Count; k++)
            {



                var nodes = stateNodeModels[k].GetConnectedNodes(PortDirection.Input, PortType.Data).OfType<INameable>().ToList();
                for (int i = 0; i < nodes.Count; i++)
                {
                    for (int j = 0; j < nodes.Count; j++)
                    {
                        if (i == j)
                            continue;

                        if (nodes[i].Name == nodes[j].Name)
                        {
                            var elem = nodes[i];

                            res.AddError($"a graph contains  two or more StateNodeModel with Name: {nodes[i].Name}", nodes[i] as INodeModel, new QuickFix("Add to name 1", (cd) =>
                            {
                                cd.Dispatch(new SetNameCommand(elem.Name + "1", elem));
                            }));
                        }
                    }
                }
            }

        }
    }
}
