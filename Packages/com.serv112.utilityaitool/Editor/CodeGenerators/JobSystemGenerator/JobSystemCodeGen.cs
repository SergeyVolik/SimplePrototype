using SerV112.UtilityAI.Math;
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
    public class JobSystemCodeGen : CodeGenerator
    {
        private StringBuilder m_FileLinesOfCode = new StringBuilder();
        private StringBuilder m_StructDeclarationPart = new StringBuilder();

        private StringBuilder m_PropertiesDecl = new StringBuilder();
        private StringBuilder m_ExecuteFUnction = new StringBuilder();


        private string m_Namesapce;

        public JobSystemCodeGen(AIGraphAssetModel Asset, AIStencil Stencil, string Filename, string FilePath) : base(Asset, Stencil, Filename, FilePath, "cs")
        {
            m_Namesapce = "MYNamespace";
            m_TabsForClass = 1;
            m_TabsForFunctions = 2;
            m_TabsForLocalVars = 3;
        }



        private string outputLocalVariable = "resultVar";
        protected override string AIProcessorNodeModelNode(AIProcessorNodeModel processor)
        {
            m_ExecuteFUnction.AppendLine(GetTabsForFunctions("public void Execute (int index)"));
            m_ExecuteFUnction.AppendLine(GetTabsForFunctions("{"));
            m_ExecuteFUnction.AppendLine(GetTabsForLocal($"var {outputLocalVariable} = new {OutAgentDataName}();"));

            var staetGroups = processor.GetConnectedNodes(PortDirection.Input, PortType.Data).OfType<StateGroupNodeModel>().ToList();

            for (int i = 0; i < staetGroups.Count; i++)
            {
               NextNode(staetGroups[i]);
            }

            m_ExecuteFUnction.AppendLine(m_LocalVariableDeclaration.ToString());

            m_ExecuteFUnction.AppendLine(GetTabsForLocal($"{OutAgentDataName}[index] = {outputLocalVariable};"));

            var nativeArrays = initedFloatArrays.Values.ToList();
            for (int i = 0; i < nativeArrays.Count; i++)
            {
                m_ExecuteFUnction.AppendLine(GetTabsForLocal($"{nativeArrays[i]}.Dispose();"));
            }

            m_ExecuteFUnction.AppendLine(GetTabsForFunctions("}"));

            return string.Empty;
        }

        int average;
        protected override string AverageNodeModel(AverageNodeModel averNode)
        {
            var stateNodes = averNode.GetConnectedInputDataNodes().OfType<NodeModel>().ToList();

            string allParamsString = "";
            List<string> allParamsList = new List<string>();
            for (int j = 0; j < stateNodes.Count; j++)
            {

                var variablename = NextNode(stateNodes[j]);
                allParamsList.Add(variablename);
            }

            allParamsList.Sort();
            for (int j = 0; j < allParamsList.Count; j++)
            {
                allParamsString += allParamsList[j];
            }

            if (!initedFloatArrays.TryGetValue(allParamsString, out var result))
            {
                result = $"values{arrayNumber}";
                m_LocalVariableDeclaration.AppendLine(GetTabsForLocal($"NativeArray<float> {result} = new  NativeArray<float>({allParamsList.Count}, Allocator.Temp);"));
                for (int j = 0; j < allParamsList.Count; j++)
                {
                    m_LocalVariableDeclaration.AppendLine(GetTabsForLocal($"{result}[{j}] = {allParamsList[j]};"));
                }

                initedFloatArrays.Add(allParamsString, result);


                arrayNumber++;
            }

            var vari = SaveFunctionCallToVariable($"{nameof(UtilityAIMath)}.{nameof(UtilityAIMath.Average)}({result})", nameof(average), ref average, "float");

            return vari;
        }

        protected override void CodePart()
        {
            m_FileLinesOfCode.AppendLine("using Unity.Jobs;");
            m_FileLinesOfCode.AppendLine("using Unity.Collections;");
            m_FileLinesOfCode.AppendLine("using Unity.Burst;");
            m_FileLinesOfCode.AppendLine("using SerV112.UtilityAI.Math;");

            if (!string.IsNullOrEmpty(m_Namesapce))
            {
                m_FileLinesOfCode.AppendLine($"namespace {m_Namesapce}");
                m_FileLinesOfCode.AppendLine("{");
            }

            m_FileLinesOfCode.AppendLine(GetTabsForClass("[BurstCompile]"));
            m_FileLinesOfCode.AppendLine(GetTabsForClass("public struct ProcessAI : IJobParallelFor"));
            m_FileLinesOfCode.AppendLine(GetTabsForClass("{"));

            var proc = m_Stencil.GetEntryPoints().OfType<AIProcessorNodeModel>().ToList()[0];
            NextNode(proc);


            if (DeclareStruct(inputDataFieldNames, InAgentDataName, "float"))
            {


                m_PropertiesDecl.AppendLine(GetTabsForFunctions($"public NativeArray<{InAgentDataName}> {InAgentDataName};"));

            }

            if (DeclareStruct(outputDataFieldNames, OutAgentDataName, "int"))
            {
                m_PropertiesDecl.AppendLine(GetTabsForFunctions($"[ReadOnly]"));
                m_PropertiesDecl.AppendLine(GetTabsForFunctions($"public NativeArray<{OutAgentDataName}> {OutAgentDataName};"));
            }

            m_FileLinesOfCode.AppendLine(m_PropertiesDecl.ToString());
            m_FileLinesOfCode.AppendLine(m_ExecuteFUnction.ToString());         
            m_FileLinesOfCode.AppendLine(GetTabsForClass("}"));
            m_FileLinesOfCode.AppendLine(m_StructDeclarationPart.ToString());
            if (!string.IsNullOrEmpty(m_Namesapce))
            {

                m_FileLinesOfCode.AppendLine("}");
            }

            m_FileContent = m_FileLinesOfCode.ToString();
        }


        bool DeclareStruct(List<string> inputDataFieldNames, string structName, string type)
        {
            if (inputDataFieldNames.Count > 0)
            {
                m_StructDeclarationPart.AppendLine(GetTabsForClass($"public struct {structName} {{"));

                for (int i = 0; i < inputDataFieldNames.Count; i++)
                {
                    m_StructDeclarationPart.AppendLine(GetTabsForFunctions($"public {type} {inputDataFieldNames[i]};"));
                }

                m_StructDeclarationPart.AppendLine(GetTabsForClass($"}}"));

                return true;
            }

            return false;
        }
        



        private const string InAgentDataName = "InAgentData";
        private const string OutAgentDataName = "OutAgentData";

        List<string> inputDataFieldNames = new List<string>();
        List<string> outputDataFieldNames = new List<string>();
        protected override void DeclarationPart()
        {
            m_FileLinesOfCode.AppendLine("//-----------------------------------------------------------------------");
            m_FileLinesOfCode.AppendLine("// This file is AUTO-GENERATED.");
            m_FileLinesOfCode.AppendLine("// Changes for this script by hand might be lost when auto-generation is run.");
            m_FileLinesOfCode.AppendLine($"// (Generated date:{DateTime.Now})");
            m_FileLinesOfCode.AppendLine(" //-----------------------------------------------------------------------");

        }

        int cosine = 0;
        protected override string CosineCurveNodeModel(CosineCurveNodeModel cosineCurveNode)
        {
            var Value = GetValue(cosineCurveNode.InputPort, out _);
            var Steepness = GetFloatStrWithDot(cosineCurveNode.Steepness);
            var OffsetY = GetFloatStrWithDot(cosineCurveNode.OffsetY);
            var OffsetX = GetFloatStrWithDot(cosineCurveNode.OffsetX);
            var functionName = nameof(UtilityAIMath.CosineCurve);
            var MathClass = nameof(UtilityAIMath);

            return SaveFunctionCallToVariable(
                $"{MathClass}.{functionName}({Value}, {Steepness}, {OffsetY}, {OffsetX})",
                nameof(cosine),
                ref cosine,
                "float"
            );
        }


        int exponential = 0;
        protected override string ExponentialCurveNodeModel(ExponentialCurveNodeModel expCurveNode)
        {
            var Value = GetValue(expCurveNode.InputPort, out _);
            var Exponent = GetFloatStrWithDot(expCurveNode.Exponent);
            var OffsetY = GetFloatStrWithDot(expCurveNode.OffsetY);
            var functionName = nameof(UtilityAIMath.ExponentialCurve);
            var MathClass = nameof(UtilityAIMath);

            return SaveFunctionCallToVariable(
                $"{MathClass}.{functionName}({Value}, {Exponent}, {OffsetY})",
                nameof(exponential),
                ref exponential,
                "float"
            );
        }
        int linear = 0;
        protected override string LinearCurveNodeModel(LinearCurveNodeModel linearCurveNode)
        {
            var Value = GetValue(linearCurveNode.InputPort, out _);
            var Slope = GetFloatStrWithDot(linearCurveNode.Slope);
            var OffsetY = GetFloatStrWithDot(linearCurveNode.OffsetY);
            var functionName = nameof(UtilityAIMath.LinearCurve);
            var MathClass = nameof(UtilityAIMath);

            return SaveFunctionCallToVariable(
                $"{MathClass}.{functionName}({Value}, {Slope}, {OffsetY})",
                nameof(linear),
                ref linear,
                "float"
            );
        }
        int logistic = 0;
        protected override string LogisticCurveNodeModel(LogisticCurveNodeModel logisticNode)
        {
            var Value = GetValue(logisticNode.InputPort, out _);
            var Steepness = GetFloatStrWithDot(logisticNode.Steepness);
            var OffsetX = GetFloatStrWithDot(logisticNode.OffsetX);
            var functionName = nameof(UtilityAIMath.LogisticCurve);
            var MathClass = nameof(UtilityAIMath);

            return SaveFunctionCallToVariable(
                $"{MathClass}.{functionName}({Value}, {Steepness}, {OffsetX})",
                nameof(logistic),
                ref logistic,
                "float"
            );
        }
        int logit = 0;
        protected override string LogitCurveNodeModel(LogitCurveNodeModel logitNode)
        {
            var Value = GetValue(logitNode.InputPort, out _);
            var LogBase = GetFloatStrWithDot(logitNode.LogBase);
            var functionName = nameof(UtilityAIMath.LogitCurve);
            var MathClass = nameof(UtilityAIMath);

            return SaveFunctionCallToVariable(
                $"{MathClass}.{functionName}({Value}, {LogBase})",
                nameof(logit),
                ref logit,
                "float"
            );
        }
        int oneminus = 0;
        protected override string OneMinusNodeModel(OneMinusNodeModel oneMinus)
        {
            
            var Value = GetValue(oneMinus.InputPort, out _);
            var functionName = nameof(UtilityAIMath.OneMinus);
            var MathClass = nameof(UtilityAIMath);

            return SaveFunctionCallToVariable(
                $"{MathClass}.{functionName}({Value})",
                nameof(oneminus),
                ref oneminus,
                "float"
            );
        }
        int sineCounter = 0;
        protected override string SineCurveNodeModel(SineCurveNodeModel sineCurveNode)
        {
            var Value = GetValue(sineCurveNode.InputPort, out _);
            var Steepness = GetFloatStrWithDot(sineCurveNode.Steepness);
            var OffsetY = GetFloatStrWithDot(sineCurveNode.OffsetY);
            var OffsetX = GetFloatStrWithDot(sineCurveNode.OffsetX);
            var functionName = nameof(UtilityAIMath.SineCurve);
            var MathClass = nameof(UtilityAIMath);

            return SaveFunctionCallToVariable(
               $"{MathClass}.{functionName}({Value}, {Steepness}, {OffsetY}, {OffsetX})",
               nameof(sineCounter),
               ref sineCounter,
               "float"
           );
        }
        int smootherstepCounter = 0;
        protected override string SmootherstepCurveNodeModel(SmootherstepCurveNodeModel smootherstepCurveNode)
        {
            var Value = GetValue(smootherstepCurveNode.InputPort, out _);
            var functionName = nameof(UtilityAIMath.SmootherstepCurve);
            var MathClass = nameof(UtilityAIMath);

            return SaveFunctionCallToVariable(
                $"{MathClass}.{functionName}({Value})",
                nameof(smootherstepCounter),
                ref smootherstepCounter,
                "float"
            );
        }

        int smoothstepCounter = 0;
        protected override string SmoothstepCurveNodeModel(SmoothstepCurveNodeModel smoothstepCurveNode)
        {
            var Value = GetValue(smoothstepCurveNode.InputPort, out _);
            var functionName = nameof(UtilityAIMath.SmoothstepCurve);
            var MathClass = nameof(UtilityAIMath);

            return SaveFunctionCallToVariable(
                $"{MathClass}.{functionName}({Value})",
                nameof(smoothstepCounter),
                ref smoothstepCounter,
                "float"
            );
        }

        int arrayNumber = 0;
        int entryIndex = 0;
        protected override string StateGroupNodeModel(StateGroupNodeModel stateGroup)
        {
            outputDataFieldNames.Add(stateGroup.Name);

            var stateNodes = stateGroup.GetConnectedInputDataNodes().OfType<NodeModel>().ToList();

            string allParamsString = "";
            List<string> allParamsList = new List<string>();
            for (int j = 0; j < stateNodes.Count; j++)
            {

                var variablename = NextNode(stateNodes[j]);
                allParamsList.Add(variablename);
            }

            allParamsList.Sort();
            for (int j = 0; j < allParamsList.Count; j++)
            {
                allParamsString += allParamsList[j];
            }
            if (!initedFloatArrays.TryGetValue(allParamsString, out var result))
            {
                result = $"values{arrayNumber}"; 
                m_LocalVariableDeclaration.AppendLine(GetTabsForLocal($"NativeArray<float> {result} = new  NativeArray<float>({allParamsList.Count}, Allocator.Temp);"));
                for (int j = 0; j < allParamsList.Count; j++)
                {
                    m_LocalVariableDeclaration.AppendLine(GetTabsForLocal($"{result}[{j}] = {allParamsList[j]};"));
                }

                initedFloatArrays.Add(allParamsString, result);

               
                arrayNumber++;
            }

            var vari = SaveFunctionCallToVariable($"{nameof(UtilityAIMath)}.{nameof(UtilityAIMath.SelectAnswerIndex)}({result})", $"selectIndex", ref entryIndex, "int");

            m_LocalVariableDeclaration.AppendLine(GetTabsForLocal($"{outputLocalVariable}.{stateGroup.Name} = {vari};"));
            

            return "StateGroupNodeModel";
        }

        protected override string StateNodeModel(StateNodeModel state)
        {
            var nodes = state.GetConnectedInputDataNodes().OfType<NodeModel>().ToList();

            return NextNode(nodes[0]);


        }

        protected override string VariableNodeModel(VariableNodeModel variable)
        {
            if (!inputDataFieldNames.Contains(variable.Title))
            {
                inputDataFieldNames.Add(variable.Title);
            }

            var @const = variable.VariableDeclarationModel.InitializationModel as NormalizedFloatConstant;
            var normalizedConst = @const.Value;

            var value = $"{InAgentDataName}[index].{variable.VariableDeclarationModel.Title}";
            var min = normalizedConst.Min.ToString();
            var max = normalizedConst.Max.ToString();

            string nomalization = $"{nameof(UtilityAIMath)}.{nameof(UtilityAIMath.Normalize01)}({value}, {min}, {max})";

            if (!Variables.TryGetValue(nomalization, out var localVar))
            {
                localVar = $"nomalized{variable.VariableDeclarationModel.Title}";
                Variables.Add(nomalization, localVar);
                m_LocalVariableDeclaration.AppendLine(GetTabsForLocal($"float {localVar} = {nomalization};"));

            }

            return localVar;

        }

        protected override string Value01NodeModel(Value01NodeModel value01)
        {
            throw new NotImplementedException();
        }

        protected override string Multiply1NodeModel(Multiply01NodeModel mult01)
        {
            throw new NotImplementedException();
        }

        protected override string CustomCurveNodeModel(CustomCurveNodeModel customCurve)
        {
            throw new NotImplementedException();
        }
    }
}
