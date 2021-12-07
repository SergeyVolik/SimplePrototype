using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    class GraphCodeGenProcessor : IGraphProcessor
    {
        private static CodeDomProvider provider = CodeDomProvider.CreateProvider("C#");
        public GraphProcessingResult ProcessGraph(IGraphModel graphModel)
        {
            var res = new GraphProcessingResult();

            Debug.Log("GraphCodeGenProcessor ProcessGraph");
            CheckDuplicatedNames(res, graphModel);
            ValidateNames(res, graphModel);
            return res;
        }

        private void ValidateNames(GraphProcessingResult res, IGraphModel graphModel)
        {
            ValidateVariablesNames(res, graphModel);
            ValidateOuterVariablesNames(res, graphModel);
        }
        private void ValidateVariablesNames(GraphProcessingResult res, IGraphModel graphModel)
        {
           graphModel.NodeModels
               .OfType<INameable>()
               .ToList().ForEach(e => {

                   if (!CheckName(e.Name))
                   {

                       string typeName = "Type Not Detected";
                       if (e is StateNodeModel)
                       {
                           typeName = nameof(StateNodeModel);
                       }
                       else if (e is StateGroupNodeModel)
                       {
                           typeName = nameof(StateGroupNodeModel);
                       }

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
            .ToList().ForEach(e => {

                if (!CheckName(e.Title))
                {
                    res.AddError($"Outer varibale has an invalid name: {e.Title}{Environment.NewLine}{ValidationValiableRules}");
                }
            });


        }

        private void CheckDuplicatedNames(GraphProcessingResult res, IGraphModel graphModel)
        {
            CheckDuplicatedNamesForStateNodeModel(res, graphModel);
            CheckDuplicatedNamesForStateGroupNodeModel(res, graphModel);
        }
        private void CheckDuplicatedNamesForStateNodeModel(GraphProcessingResult res, IGraphModel graphModel)
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
                    {
                        var elem = stateNodeModels[i];

                        res.AddError($"a graph contains  two or more StateNodeModel with Name: {stateNodeModels[i].Name}", stateNodeModels[i], new QuickFix("Add to name 1", (cd) =>
                        {
                            cd.Dispatch(new SetStateNameCommand(elem.Name + "1", elem));
                        }));
                    }
                }
            }

        }

        private void CheckDuplicatedNamesForStateGroupNodeModel(GraphProcessingResult res, IGraphModel graphModel)
        {

            var stateNodeModels = graphModel.NodeModels
                .OfType<StateGroupNodeModel>()
                .ToList();


            for (int i = 0; i < stateNodeModels.Count; i++)
            {
                for (int j = 0; j < stateNodeModels.Count; j++)
                {
                    if (i == j)
                        continue;

                    if (stateNodeModels[i].Name == stateNodeModels[j].Name)
                    {
                        var elem = stateNodeModels[i];

                        res.AddError($"a graph contains  two or more StateGroupNodeModel with Name: {stateNodeModels[i].Name}", stateNodeModels[i], new QuickFix("Add to name 1", (cd) =>
                        {
                            cd.Dispatch(new SetStateNameCommand(elem.Name + "1", elem));
                        }));
                    }
                }
            }

        }
    }
}
