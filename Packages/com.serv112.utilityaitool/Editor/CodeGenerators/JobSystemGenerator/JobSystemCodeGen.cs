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

        public string InAgentDataName = "InAgentData";
        public string OutAgentDataName = "OutAgentData";


        private string m_Namesapce;

        private string outputLocalVariable = "resultVar";
        private List<string> m_OutputEnumTypes;
        string processorName;
        public JobSystemCodeGen(AIGraphAssetModel Asset, AIStencil Stencil, string Filename, string FilePath, List<string> OutputEnumTypes, string name) : base(Asset, Stencil, Filename, FilePath, "cs")
        {
            InAgentDataName = "InAgentData" + name;
            OutAgentDataName = "OutAgentData" + name;
            //m_Namesapce = "Namespace";
            //m_TabsForClass = 1;
            //m_TabsForFunctions = 2;
            //m_TabsForLocalVars = 3;

            m_OutputEnumTypes = OutputEnumTypes;
        }



      
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
            m_FileLinesOfCode.AppendLine("using System;");
            m_FileLinesOfCode.AppendLine("using Unity.Jobs;");
            m_FileLinesOfCode.AppendLine("using Unity.Collections;");
            m_FileLinesOfCode.AppendLine("using Unity.Burst;");
            m_FileLinesOfCode.AppendLine("using SerV112.UtilityAI.Math;"); 
            m_FileLinesOfCode.AppendLine("using SerV112.UtilityAI.Base;"); 

            if (!string.IsNullOrEmpty(m_Namesapce))
            {
                m_FileLinesOfCode.AppendLine($"namespace {m_Namesapce}");
                m_FileLinesOfCode.AppendLine("{");
            }

            m_FileLinesOfCode.AppendLine(GetTabsForClass("[BurstCompile]"));
            m_FileLinesOfCode.AppendLine(GetTabsForClass($"public struct {m_Filename} : IUtilityAIJob<{InAgentDataName}, {OutAgentDataName}>"));
            m_FileLinesOfCode.AppendLine(GetTabsForClass("{"));

            var proc = m_Stencil.GetEntryPoints().OfType<AIProcessorNodeModel>().ToList()[0];
            NextNode(proc);

            List<string> list = new List<string>();
            for (int i = 0; i < inputDataFieldNames.Count; i++)
            {
                list.Add("float");
            }

            if (DeclareStruct(inputDataFieldNames, InAgentDataName, list))
            {

                m_PropertiesDecl.AppendLine(GetTabsForFunctions($"[ReadOnly]"));
                m_PropertiesDecl.AppendLine(GetTabsForFunctions($"private NativeArray<{InAgentDataName}> {InAgentDataName};"));
                m_PropertiesDecl.AppendLine(GetTabsForFunctions($"public NativeArray<{InAgentDataName}> InAgentDataArray {{ get => {InAgentDataName}; set => {InAgentDataName} = value; }}"));
                

            }

            if (DeclareStruct(outputDataFieldNames, OutAgentDataName, m_OutputEnumTypes))
            {
              
                m_PropertiesDecl.AppendLine(GetTabsForFunctions($"private NativeArray<{OutAgentDataName}> {OutAgentDataName};"));
                m_PropertiesDecl.AppendLine(GetTabsForFunctions($"public NativeArray<{OutAgentDataName}> OutAgentDataArray {{ get => {OutAgentDataName}; set => {OutAgentDataName} = value; }}"));
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


        bool DeclareStruct(List<string> inputDataFieldNames, string structName, List<string> types)
        {
           
            m_StructDeclarationPart.AppendLine(GetTabsForClass($"[Serializable]"));
            m_StructDeclarationPart.AppendLine(GetTabsForClass($"public struct {structName} {{"));

            for (int i = 0; i < inputDataFieldNames.Count; i++)
            {
                m_StructDeclarationPart.AppendLine(GetTabsForFunctions($"public {types[i]} {inputDataFieldNames[i]};"));
            }

            m_StructDeclarationPart.AppendLine(GetTabsForClass($"}}"));

            return true;
            

        }
        



      

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

            var stateNodes = stateGroup.GetConnectedNodes(PortDirection.Input, PortType.Data).OfType<StateNodeModel>().ToList();

            string allParamsString = "";
            List<string> allParamsList = new List<string>();
            for (int j = 0; j < stateNodes.Count; j++)
            {

                var variablename = NextNode(stateNodes[j]);
                allParamsList.Add(variablename);
            }


            for (int j = 0; j < allParamsList.Count; j++)
            {
                allParamsString += allParamsList[j];
            }
            if (!initedFloatArrays.TryGetValue(allParamsString, out var result))
            {
                result = $"values{arrayNumber}"; 
                m_LocalVariableDeclaration.AppendLine(GetTabsForLocal($"NativeArray<float> {result} = new  NativeArray<float>({allParamsList.Count}, Allocator.Temp);"));
                for (int j =0; j < allParamsList.Count; j++)
                {
                    m_LocalVariableDeclaration.AppendLine(GetTabsForLocal($"{result}[{j}] = {allParamsList[j]};"));
                }

                initedFloatArrays.Add(allParamsString, result);

               
                arrayNumber++;
            }

            var vari = SaveFunctionCallToVariable($"{nameof(UtilityAIMath)}.{nameof(UtilityAIMath.SelectAnswerIndex)}({result})", $"selectIndex", ref entryIndex, m_OutputEnumTypes[entryIndex], true);

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
            return GetFloatStrWithDot(value01.Value01);
        }

        int arraysCounter;
     
        private (bool existed, string arrayName) PrepareFloatArray(string floatArrayParams, List<string> paramsNames)
        {
            if (!initedFloatArrays.TryGetValue(floatArrayParams, out var array))
            {

               
                array = $"floatArray{arraysCounter}";
                m_LocalVariableDeclaration.AppendLine(GetTabsForLocal($"NativeArray<float> {array} = new  NativeArray<float>({paramsNames.Count}, Allocator.Temp);"));

                for (int i = 0; i < paramsNames.Count; i++)
                {

                    m_LocalVariableDeclaration.AppendLine(GetTabsForLocal($"{array}[{i}] = {paramsNames[i]};"));
                }



                initedFloatArrays.Add(floatArrayParams, array);

                arraysCounter++;
                return (false, array);
            }

            return (true, array);
        }


        int multiply;
        protected override string MultiplyNodeModel(Multiply01NodeModel mult01)
        {
            var ports = mult01.GetPorts(PortDirection.Input, PortType.Data).ToList();
            var paramsData = GetFunctionParams(ports);
            var result = PrepareFloatArray(paramsData.floatArrayParams, paramsData.paramsNames);

            string Mylt = $"{nameof(UtilityAIMath)}.{nameof(UtilityAIMath.Multiply)}({result.arrayName})";


               
            return SaveFunctionCallToVariable(Mylt, "multiply", ref multiply, "float");
            
        }

        int customCurvesArrayCounter;
        protected override string CustomCurveNodeModel(CustomCurveNodeModel customCurve)
        {

            if (!CustomCurves.TryGetValue(customCurve.Guid, out var arrayName))
            {
                float step = 1f / 100f;
                arrayName = $"customCurveArray{customCurvesArrayCounter}";

                m_PropertiesDecl.AppendLine(GetTabsForFunctions($"private static readonly float[] {arrayName} = {{"));
                for (float x = 0; x < 1f; x += step)
                {
                    m_PropertiesDecl.AppendLine(GetTabsForLocal($"{GetFloatStrWithDot(Mathf.Clamp01(customCurve.CustomCurve.Evaluate(x)))},"));
                }

                m_PropertiesDecl.AppendLine(GetTabsForFunctions($"}};"));
                customCurvesArrayCounter++;

                CustomCurves.Add(customCurve.Guid, arrayName);
            }



            return $"{arrayName}[(int)({GetValue(customCurve.InputPort, out _)}*100)]";
        }

        int maxCounter;
        protected override string Max01NodeModel(Max01NodeModel max01)
        {

            string functionName = $"{nameof(UtilityAIMath)}.{nameof(UtilityAIMath.Max)}";
            return MultypleFunctionCall(max01, functionName, "max", ref maxCounter);
        }

        string GetFuncCallStr(string funcName, List<string> @params)
        {
            string result = $"{funcName}(";
            for (int i = 0; i < @params.Count; i++)
            {
                if(i != @params.Count-1 )
                    result += $"{@params[i]}, ";
                else result += $"{@params[i]}";
            }

            return result;
        }
        string MultypleFunctionCall(NodeModel max01, string functionName, string varibaleName, ref int funcCallCounter)
        {
            var ports = max01.GetPorts(PortDirection.Input, PortType.Data).ToList();
            var paramsData = GetFunctionParams(ports);

            var key = GetFuncCallStr(functionName, paramsData.paramsNames);

            if (!FunctionCalls.TryGetValue(key, out var varName))
            {
                varName = "0";
                if (paramsData.paramsNames.Count >= 2)
                {
                    string Mylt = $"{functionName}({paramsData.paramsNames[0]},{paramsData.paramsNames[1]})";
                    varName = SaveFunctionCallToVariableWithoutCashe(Mylt, varibaleName, ref funcCallCounter, "float");

                    funcCallCounter++;
                    for (int i = 2; i < paramsData.paramsNames.Count; i++)
                    {
                        SaveFunctionCallToExistedVariable($"{functionName}({varName}, {paramsData.paramsNames[i]})", varName);
                    }
                    FunctionCalls.Add(key, varName);
                }
                else if (paramsData.paramsNames.Count == 1)
                {
                    varName = paramsData.paramsNames[0];
                    FunctionCalls.Add(varName, varName);
                }

                
            }
           

            return varName;
        }

        int minCounter;
        protected override string Min01NodeModel(Min01NodeModel min01)
        {
            string functionName = $"{nameof(UtilityAIMath)}.{nameof(UtilityAIMath.Min)}";
            return MultypleFunctionCall(min01, functionName, "min", ref minCounter);
        }
    }
}
