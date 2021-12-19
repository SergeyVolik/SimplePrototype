using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;

namespace SerV112.UtilityAIEditor
{
    public interface IHLSLDeclaration
    {
        public bool IsDeclared { get; }
        public void Declare();
    }

    public class HLSLGenerator
    {
        private readonly HLSLMinusInfinity MinusInfinity;
        private readonly HLSlPlusInfinity PlusInfinity;
        private readonly HLSL_PI PI;
        private readonly HLSL_E E;
                
        private readonly HLSLLogitCurveFunction HLSLLogitCurveFunction;
        private readonly HLSLLinearCurveFunction HLSLLinearCurveFunction;
        private readonly HLSLExponentialCurveFunction HLSLExponentialCurveFunction;
        private readonly HLSLLogWithBaseFunction HLSLLogWithBaseFunction;
        private readonly HLSLLogisticCurveFunction HLSLLogisticCurveFunction;
        private readonly HLSLSmoothstepCurveFunction HLSLSmoothstepCurveFunction;
        private readonly HLSLSmootherstepCurveFunction HLSLSmootherstepCurveFunction;
        private readonly HLSLSineCurveFunction HLSLSineCurveFunction;
        private readonly HLSLCosineCurveFunction HLSLCosineCurveFunction;
        private readonly HLSLOneMinusFunction HLSLOneMinusFunction;
        private readonly HLSLNormalizeFunction HLSLNormalizeFunction;

        static HLSLGenerator()
        {
           
        }
       

        //StringBuilder m_Code;
        //StringBuilder m_DecalarationCode;
        StringBuilder m_DecalarationConstanValues = new StringBuilder();
        StringBuilder m_DecalarationFunctions = new StringBuilder();
        StringBuilder m_DecalarationInputVriables = new StringBuilder();
        StringBuilder m_CSMainCode = new StringBuilder();
        StringBuilder m_CodeGenComment = new StringBuilder();
        AIGraphAssetModel m_Asset;
        string m_SavePath;
        string m_Filename;
        public HLSLGenerator(AIGraphAssetModel asset, string path)
        {
            m_SavePath = path;
            m_Filename = asset.GraphModel.NodeModels.OfType<AIProcessorNodeModel>().ToList()[0].Name;

            m_Asset = asset;

            MinusInfinity = new HLSLMinusInfinity(m_DecalarationConstanValues);
            PlusInfinity = new HLSlPlusInfinity(m_DecalarationConstanValues);
            PI = new HLSL_PI(m_DecalarationConstanValues);
            E = new HLSL_E(m_DecalarationConstanValues);

            HLSLLogWithBaseFunction = new HLSLLogWithBaseFunction(m_DecalarationFunctions);
            HLSLLinearCurveFunction = new HLSLLinearCurveFunction(m_DecalarationFunctions);
            HLSLExponentialCurveFunction = new HLSLExponentialCurveFunction(m_DecalarationFunctions);
            HLSLLogisticCurveFunction = new HLSLLogisticCurveFunction(m_DecalarationFunctions, E);
            HLSLSmoothstepCurveFunction = new HLSLSmoothstepCurveFunction(m_DecalarationFunctions);
            HLSLSmootherstepCurveFunction = new HLSLSmootherstepCurveFunction(m_DecalarationFunctions);
            HLSLSineCurveFunction = new HLSLSineCurveFunction(m_DecalarationFunctions, PI);
            HLSLCosineCurveFunction = new HLSLCosineCurveFunction(m_DecalarationFunctions, PI);
            HLSLOneMinusFunction = new HLSLOneMinusFunction(m_DecalarationFunctions);
            HLSLNormalizeFunction = new HLSLNormalizeFunction(m_DecalarationFunctions);
            HLSLLogitCurveFunction = new HLSLLogitCurveFunction(HLSLLogWithBaseFunction, m_DecalarationFunctions);
        }
        //-----------------------------------------------------------------------
        // This file is AUTO-GENERATED.
        // Changes for this script by hand might be lost when auto-generation is run.
        // (Generated date: 2021.12.15 10:23:57)
        //-----------------------------------------------------------------------
        private void DeclarationPart()
        {
            m_CodeGenComment.AppendLine("//-----------------------------------------------------------------------");
            m_CodeGenComment.AppendLine("// This file is AUTO-GENERATED.");
            m_CodeGenComment.AppendLine("// Changes for this script by hand might be lost when auto-generation is run.");
            m_CodeGenComment.AppendLine($"// (Generated date:{DateTime.Now})");
            m_CodeGenComment.AppendLine(" //-----------------------------------------------------------------------");
            m_DecalarationConstanValues.AppendLine("#pragma kernel CSMain");
        }


        Dictionary<string, string> VariablesBuffers = new Dictionary<string, string>();
        Dictionary<int, HLSLSelectMaxValueIndexFunction> SelecteIndexFunctions = new Dictionary<int, HLSLSelectMaxValueIndexFunction>();
        private void CodePart()
        {

            m_CSMainCode.AppendLine("RWStructuredBuffer<int> Result;");
            m_CSMainCode.AppendLine("[numthreads(64,1,1)]");
            m_CSMainCode.AppendLine("void CSMain (uint3 id : SV_DispatchThreadID)");
            m_CSMainCode.AppendLine("{");
            m_CSMainCode.AppendLine("    int index = id.x;");
           
            var processors = m_Asset.GraphModel.NodeModels.OfType<AIProcessorNodeModel>().ToList();


            if (processors.Count == 0)
                throw new Exception("Please, add AIProcessorNodeModel!");

            var processor = processors[0];

            var stateGroupNodeModels = processor.GetConnectedNodes(PortDirection.Input, PortType.Data).OfType<StateGroupNodeModel>().ToList();

            for (int i = 0; i < stateGroupNodeModels.Count; i++)
            {
                var stateNodes = stateGroupNodeModels[i].GetConnectedNodes(PortDirection.Input, PortType.Data).OfType<StateNodeModel>().ToList();

                List<string> entrys = new List<string>();

                for (int j = 0; j < stateNodes.Count; j++)
                {
                    entrys.Add($"entry{j}");
                    m_CSMainCode.AppendLine($"    float entry{j} = {NextNode(stateNodes[j].GetConnectedNodes( PortDirection.Input, PortType.Data).OfType<NodeModel>().ToList()[0])};");
                }

                m_CSMainCode.AppendLine($"    float values[{stateNodes.Count}] = {{");
                for (int j = 0; j < entrys.Count; j++)
                {
                    m_CSMainCode.AppendLine($"        {entrys[j]},");
                }
                m_CSMainCode.AppendLine("    };");


                if (!SelecteIndexFunctions.TryGetValue(stateNodes.Count, out var func))
                {
                    func = new HLSLSelectMaxValueIndexFunction(stateNodes.Count, MinusInfinity, m_DecalarationFunctions);
                    SelecteIndexFunctions.Add(stateNodes.Count, func);
                }


                m_CSMainCode.AppendLine($"    Result[index] = {func.Execute("values")};");
            }

            m_CSMainCode.AppendLine("}");
        }

        string GetValue(IPortModel port)
        {
            var inputEdges = port.GetConnectedEdges().ToList();

            var value = string.Empty;

            if (inputEdges.Count() > 0)
            {
                Debug.Log(inputEdges[0].FromPort.NodeModel);
                value = NextNode(inputEdges[0].FromPort.NodeModel as NodeModel);
            }
            else
            {
                value = port.EmbeddedValue.ObjectValue.ToString();
            }

            return value;
        }
        string NextNode(NodeModel nodeModel)
        {

            switch (nodeModel)
            {
                case StateGroupNodeModel stateGroup:

                    break;
                case StateNodeModel state:

                    break;
                case AverageNodeModel averNode:
                     
                    break;

                case CosineCurveNodeModel cosineCurveNode:

                    break;
                case OneMinusNodeModel oneMinus:

                    return HLSLOneMinusFunction.Execute(GetValue(oneMinus.InputPort));

                    break;
                case ExponentialCurveNodeModel expCurveNode:

                    break;
                case LinearCurveNodeModel linearCurveNode:

                    break;

                case LogisticCurveNodeModel logisticNode:

                    break;
                case LogitCurveNodeModel logitNode:

                    break;
                case NormalizationNodeModel normNode:

                    var value = GetValue(normNode.InputPort);
                    var min = GetValue(normNode.MinPort);
                    var max = GetValue(normNode.MaxPort);

                    return HLSLNormalizeFunction.Execute(value, min, max);



                case VariableNodeModel variable:

                    if(!VariablesBuffers.TryGetValue(variable.VariableDeclarationModel.Title, out var vartiable))
                    {
                        var declaration = $"RWStructuredBuffer<float> {variable.VariableDeclarationModel.Title};";
                        VariablesBuffers.Add(variable.VariableDeclarationModel.Title, declaration);
                        m_DecalarationInputVriables.AppendLine(declaration);
                         
                    }

                    return $"{variable.VariableDeclarationModel.Title}[index]";
                case SineCurveNodeModel sineCurveNode:

                    break;
                case SmootherstepCurveNodeModel smootherstepCurveNode:

                    break;
                case SmoothstepCurveNodeModel smoothstepCurveNode:

                    break;

                default:
                    Debug.Log(nodeModel);
                    break;
            }

            return "unavailable node";


        }

        private void SaveFilePart()
        {
            m_DecalarationConstanValues.AppendLine();
            m_DecalarationFunctions.AppendLine();
            m_DecalarationInputVriables.AppendLine();
            m_CSMainCode.AppendLine();
            m_CodeGenComment.AppendLine();

            var fileContent = m_CodeGenComment.ToString() + 
                m_DecalarationConstanValues.ToString() +
                m_DecalarationFunctions.ToString() +
                m_DecalarationInputVriables.ToString() +
                m_CSMainCode.ToString();

            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create($"{m_SavePath}/{m_Filename}.compute"))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(fileContent);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

            }

            catch (Exception ex)
            {
                Debug.LogError(ex.ToString());
            }
        }
        public void GenerateAndSave()
        {
            DeclarationPart();
            CodePart();
            SaveFilePart();





           
        }
    }
}
