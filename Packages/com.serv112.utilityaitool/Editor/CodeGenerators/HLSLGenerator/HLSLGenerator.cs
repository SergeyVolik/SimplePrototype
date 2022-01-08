using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    public interface IHLSLDeclaration
    {
        public bool IsDeclared { get; }
        public void Declare();
    }

    public enum GPUPrecision
    {
        @float,
        @fixed,
        @half,
       
    }

 
    public class HLSLGenerator : CodeGenerator
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

        private readonly StringBuilder m_DecalarationConstanValues = new StringBuilder();
        private readonly StringBuilder m_DecalarationFunctions = new StringBuilder();
        private readonly StringBuilder m_DecalarationInputVriables = new StringBuilder();
        private readonly StringBuilder m_CSMainCodeBeforeDeclaration = new StringBuilder();
        private readonly StringBuilder m_CSMainCodeAfterDeclaration = new StringBuilder();
        private readonly StringBuilder m_CodeGenComment = new StringBuilder();
        private readonly StringBuilder m_InputStructurePart = new StringBuilder();
        private readonly StringBuilder m_OutStructurePart = new StringBuilder();

       
        private int m_NumThreads;

      
       
        GPUPrecision m_Precision;


        Dictionary<string, string> InStructureFields = new Dictionary<string, string>();
     

        Dictionary<int, HLSLSelectMaxValueIndexFunction> SelecteIndexFunctions = new Dictionary<int, HLSLSelectMaxValueIndexFunction>();
        Dictionary<int, HLSLAverageFunction> AverageFunctions = new Dictionary<int, HLSLAverageFunction>();
        Dictionary<int, HLSLMultiplyFunction> MultiplyFunctions = new Dictionary<int, HLSLMultiplyFunction>();
        Dictionary<int, HLSLMinFunction> MinFunctions = new Dictionary<int, HLSLMinFunction>();
        Dictionary<int, HLSLMaxFunction> MaxFunctions = new Dictionary<int, HLSLMaxFunction>();
        private HashSet<Property> VariablesSet = new HashSet<Property>();
        public IReadOnlyCollection<Property> VariablesReadonly => VariablesSet;

        private int oneMinusCallCounter = 0;
        private int cosineCurveCallCounter = 0;
        private int exponentialCurveCallCounter = 0;
        private int linearCurveCallCounter = 0;
        private int logisticCurveCallCounter = 0;
        private int logitCurveCallCounter = 0;
        private int sineCurveCallCounter = 0;
        private int smootherstepCurveCallCounter = 0;
        private int smoothstepCurveCallCounter = 0;
        private int averageArrayCounter = 0;
        private int multiplyArrayCounter = 0;
        private int selectIndexCounter = 0;



        bool inputStructStarted = false;
        bool outStructStarted = false;
        const string InAgentData = "InAgentData";
        const string OutAgentData = "OutAgentData";
        const string InAgentBuffer = "InAgentBuffer";
        const string OutAgentBuffer = "OutAgentBuffer";

        public HLSLGenerator(AIStencil Stencil, AIGraphAssetModel asset, string path, string filename, int numThreads = 64) : base(asset, Stencil, filename, path, "compute")
        {

            m_NumThreads = numThreads;
            m_Precision = asset.GPUPrecision;

            m_TabsForClass = 0;
            m_TabsForFunctions = 0;
            m_TabsForLocalVars = 1;

            MinusInfinity = new HLSLMinusInfinity(m_DecalarationConstanValues, m_Precision);
            PlusInfinity = new HLSlPlusInfinity(m_DecalarationConstanValues, m_Precision);
            PI = new HLSL_PI(m_DecalarationConstanValues, m_Precision);
            E = new HLSL_E(m_DecalarationConstanValues, m_Precision);

            HLSLLogWithBaseFunction = new HLSLLogWithBaseFunction(m_DecalarationFunctions, m_Precision);
            HLSLLinearCurveFunction = new HLSLLinearCurveFunction(m_DecalarationFunctions, m_Precision);
            HLSLExponentialCurveFunction = new HLSLExponentialCurveFunction(m_DecalarationFunctions, m_Precision);
            HLSLLogisticCurveFunction = new HLSLLogisticCurveFunction(m_DecalarationFunctions, E, m_Precision);
            HLSLSmoothstepCurveFunction = new HLSLSmoothstepCurveFunction(m_DecalarationFunctions, m_Precision);
            HLSLSmootherstepCurveFunction = new HLSLSmootherstepCurveFunction(m_DecalarationFunctions, m_Precision);
            HLSLSineCurveFunction = new HLSLSineCurveFunction(m_DecalarationFunctions, PI, m_Precision);
            HLSLCosineCurveFunction = new HLSLCosineCurveFunction(m_DecalarationFunctions, PI, m_Precision);
            HLSLOneMinusFunction = new HLSLOneMinusFunction(m_DecalarationFunctions, m_Precision);
            HLSLNormalizeFunction = new HLSLNormalizeFunction(m_DecalarationFunctions, m_Precision);
            HLSLLogitCurveFunction = new HLSLLogitCurveFunction(HLSLLogWithBaseFunction, m_DecalarationFunctions, m_Precision);
        }

        void AddToInputStructDeclaration(string structFieldName)
        {
            if (!InStructureFields.TryGetValue(structFieldName, out _))
            {
                var declaration = structFieldName;
                InStructureFields.Add(declaration, declaration);




                if (!inputStructStarted)
                {
                    inputStructStarted = true;

                    m_InputStructurePart.AppendLine($"struct {InAgentData} {{");
                    DeclareStructuredBuffer(InAgentData, InAgentBuffer);
                }

                m_InputStructurePart.AppendLine($"   float {structFieldName};");
            }
        }

        void DeclareStructuredBuffer(string type, string bufferName, bool readWrite = false)
        {
            string rw = "";
            if (readWrite)
                rw = "RW";
            m_DecalarationInputVriables.AppendLine($"{rw}StructuredBuffer<{type}> {bufferName};");
        }

     
        void FinishInputStructDeclaration()
        {
            if (inputStructStarted)
            {
                
                m_InputStructurePart.AppendLine("};");
            }
        }
        private const string AgeField = "Age";
        void FinishOutStructDeclaration()
        {
            if (outStructStarted)
            {
                m_OutStructurePart.AppendLine($"   float {AgeField};");
                m_OutStructurePart.AppendLine("};");
            }
        }

        void AddToOutputStructDeclaration(string structFieldName)
        {
            if (!outStructStarted)
            {
                outStructStarted = true;
                m_OutStructurePart.AppendLine($"struct {OutAgentData} {{");
                DeclareStructuredBuffer(OutAgentData, OutAgentBuffer, true);

            }

            m_OutStructurePart.AppendLine($"   int {structFieldName};");
        }




        protected override void DeclarationPart()
        {
            m_CodeGenComment.AppendLine("//-----------------------------------------------------------------------");
            m_CodeGenComment.AppendLine("// This file is AUTO-GENERATED.");
            m_CodeGenComment.AppendLine("// Changes for this script by hand might be lost when auto-generation is run.");
            m_CodeGenComment.AppendLine($"// (Generated date:{DateTime.Now})");
            m_CodeGenComment.AppendLine(" //-----------------------------------------------------------------------");

            m_DecalarationConstanValues.AppendLine("#pragma kernel CSMain");
            m_DecalarationConstanValues.AppendLine($"#define NB_THREADS_PER_GROUP {m_NumThreads}");
            m_DecalarationConstanValues.AppendLine("#include \"Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl\"");
            m_DecalarationInputVriables.AppendLine("CBUFFER_START(Always)");
            m_DecalarationInputVriables.AppendLine("    float SimulationTime;");
            m_DecalarationInputVriables.AppendLine("    float DeltaTime;");
            m_DecalarationInputVriables.AppendLine("CBUFFER_END");
        }

        protected override void CodePart()
        {

            var processors = m_Asset.GraphModel.NodeModels.OfType<AIProcessorNodeModel>().ToList();

            if (processors.Count == 0)
                throw new Exception("Please, add AIProcessorNodeModel!");

            NextNode(processors[0]);

           

            FinishInputStructDeclaration();
            FinishOutStructDeclaration();

            m_DecalarationConstanValues.AppendLine();
            m_DecalarationFunctions.AppendLine();
            m_DecalarationInputVriables.AppendLine();
            m_CSMainCodeBeforeDeclaration.AppendLine();
            m_CodeGenComment.AppendLine();

            m_FileContent = m_CodeGenComment.ToString() +
                m_DecalarationConstanValues.ToString() +
                m_DecalarationFunctions.ToString() +
                m_InputStructurePart.ToString() +
                m_OutStructurePart.ToString() +
                m_DecalarationInputVriables.ToString() +
                m_CSMainCodeBeforeDeclaration.ToString() +
                m_LocalVariableDeclaration.ToString() +
                m_CSMainCodeAfterDeclaration.ToString();

        }

      

        protected override string AIProcessorNodeModelNode(AIProcessorNodeModel processor)
        {
            m_CSMainCodeBeforeDeclaration.AppendLine($"[numthreads(NB_THREADS_PER_GROUP,1,1)]");
            m_CSMainCodeBeforeDeclaration.AppendLine("void CSMain (uint3 id : SV_DispatchThreadID)");
            m_CSMainCodeBeforeDeclaration.AppendLine("{");
            m_CSMainCodeBeforeDeclaration.AppendLine("    int index = id.x;");

            var stateGroupNodeModels = processor.GetConnectedNodes(PortDirection.Input, PortType.Data).OfType<StateGroupNodeModel>().ToList();

            for (int i = 0; i < stateGroupNodeModels.Count; i++)
            {
                NextNode(stateGroupNodeModels[i]);

            }
            m_CSMainCodeAfterDeclaration.AppendLine($"   {outLocalStructName}.{AgeField} = {OutAgentBuffer}[index].{AgeField} + DeltaTime; ");
            m_CSMainCodeAfterDeclaration.AppendLine($"   {OutAgentBuffer}[index] = {outLocalStructName};");
            m_CSMainCodeAfterDeclaration.AppendLine("}");

            return "processor";
        }


        protected override string StateGroupNodeModel(StateGroupNodeModel stateGroup)
        {
            var stateNodes = stateGroup.GetConnectedNodes(PortDirection.Input, PortType.Data).OfType<StateNodeModel>().ToList();

            string allParamsString = "";
            List<string> allParamsList = new List<string>();
            for (int j = 0; j < stateNodes.Count; j++)
            {

                var variablename = NextNode(stateNodes[j]);
                allParamsString += variablename;
                allParamsList.Add(variablename);
            }

            var result = PrepareFloatArray(allParamsString, allParamsList);

            if (!SelecteIndexFunctions.TryGetValue(stateNodes.Count, out var func))
            {
                func = new HLSLSelectMaxValueIndexFunction(stateNodes.Count, MinusInfinity, m_DecalarationFunctions, m_Precision);
                SelecteIndexFunctions.Add(stateNodes.Count, func);
            }

            var vari = SaveFunctionCallToVariable(func.Execute(result.arrayName), $"selectIndex", ref selectIndexCounter, "int");

            if (!outputStructCreated)
            {
                m_CSMainCodeAfterDeclaration.AppendLine($"   {OutAgentData} {outLocalStructName}; ");
                outputStructCreated = true;
            }
            m_CSMainCodeAfterDeclaration.AppendLine($"   {outLocalStructName}.{stateGroup.Name} = {vari}; ");
            AddToOutputStructDeclaration(stateGroup.Name);

            return "StateGroupNodeModel";
        }

        bool outputStructCreated = false;
        const string outLocalStructName = "output";
        protected override string StateNodeModel(StateNodeModel state)
        {
            var nodes1 = state.GetConnectedNodes(PortDirection.Input, PortType.Data).OfType<NodeModel>().ToList();

            return NextNode(nodes1[0]);
        }

        protected override string CosineCurveNodeModel(CosineCurveNodeModel cosineCurveNode)
        {
            var cosineCurveNodeResult = HLSLCosineCurveFunction.Execute(
                       GetValue(cosineCurveNode.InputPort, out _),
                        GetFloatStrWithDot(cosineCurveNode.Steepness),
                        GetFloatStrWithDot(cosineCurveNode.OffsetY)                     
                       );
            return SaveFunctionCallToVariable(cosineCurveNodeResult, "cosineCurve", ref cosineCurveCallCounter, m_Precision.ToString());
        }

        private int arraysCounter;
        private (bool existed, string arrayName) PrepareFloatArray(string floatArrayParams, List<string> paramsNames)
        {
            if (!initedFloatArrays.TryGetValue(floatArrayParams, out var array))
            {
               array = $"floatArray{arraysCounter}";
               m_LocalVariableDeclaration.AppendLine($"    {m_Precision} {array}[{paramsNames.Count}] = {{");

                for (int i = 0; i < paramsNames.Count; i++)
                {

                    m_LocalVariableDeclaration.AppendLine($"        {paramsNames[i]},");
                }

                m_LocalVariableDeclaration.AppendLine("    };");

               

                initedFloatArrays.Add(floatArrayParams, array);

                arraysCounter++;
                return (false, array);
            }

            return (true, array);
        }



        protected override string AverageNodeModel(AverageNodeModel averNode)
        {
            var ports = averNode.GetPorts(PortDirection.Input, PortType.Data).ToList();
            var paramsData = GetFunctionParams(ports);
            var result = PrepareFloatArray(paramsData.floatArrayParams, paramsData.paramsNames);

            if (!AverageFunctions.TryGetValue(ports.Count, out var avrFunc))
            {
                avrFunc = new HLSLAverageFunction(ports.Count, m_DecalarationFunctions, m_Precision);
                AverageFunctions.Add(ports.Count, avrFunc);
            }

            if (!result.existed)
            {
                var avrageResult = avrFunc.Execute(result.arrayName);
                return SaveFunctionCallToVariable(avrageResult, "average", ref averageArrayCounter, m_Precision.ToString());
            }

            return SaveFunctionCallToVariable(avrFunc.Execute(result.arrayName), "average", ref averageArrayCounter, m_Precision.ToString());
        }

        protected override string OneMinusNodeModel(OneMinusNodeModel oneMinus)
        {
            var oneMinusResult = HLSLOneMinusFunction.Execute(GetValue(oneMinus.InputPort, out _));
            return SaveFunctionCallToVariable(oneMinusResult, "oneMinus", ref oneMinusCallCounter, m_Precision.ToString());
        }

        protected override string ExponentialCurveNodeModel(ExponentialCurveNodeModel expCurveNode)
        {
            var exponentialCurveResult = HLSLExponentialCurveFunction.Execute(
                       GetValue(expCurveNode.InputPort, out _),
                       GetFloatStrWithDot(expCurveNode.Exponent),
                       GetFloatStrWithDot(expCurveNode.OffsetY)
                       );
            return SaveFunctionCallToVariable(exponentialCurveResult, "exponentialCurve", ref exponentialCurveCallCounter, m_Precision.ToString());
        }

        protected override string LinearCurveNodeModel(LinearCurveNodeModel linearCurveNode)
        {
            var linearCurveResult = HLSLLinearCurveFunction.Execute(
                      GetValue(linearCurveNode.InputPort, out _),
                       GetFloatStrWithDot(linearCurveNode.Slope),
                        GetFloatStrWithDot(linearCurveNode.OffsetY)
                      );
            return SaveFunctionCallToVariable(linearCurveResult, "linearCurve", ref linearCurveCallCounter, m_Precision.ToString());
        }

        protected override string LogisticCurveNodeModel(LogisticCurveNodeModel logisticNode)
        {
            var logisticCurveResult = HLSLLogisticCurveFunction.Execute(
                      GetValue(logisticNode.InputPort, out _),
                      GetFloatStrWithDot(logisticNode.Steepness),
                        GetFloatStrWithDot(logisticNode.OffsetX)
                      );
            return SaveFunctionCallToVariable(logisticCurveResult, "logisticCurve", ref logisticCurveCallCounter, m_Precision.ToString());
        }

        protected override string LogitCurveNodeModel(LogitCurveNodeModel logitNode)
        {
            var logitCurveResult = HLSLLogitCurveFunction.Execute(
                    GetValue(logitNode.InputPort, out _),
                    GetFloatStrWithDot(logitNode.LogBase)
                    );
            return SaveFunctionCallToVariable(logitCurveResult, "logitCurve", ref logitCurveCallCounter, m_Precision.ToString());
        }

        protected override string VariableNodeModel(VariableNodeModel variable)
        {
            
            AddToInputStructDeclaration(variable.VariableDeclarationModel.Title);

            var @const = variable.VariableDeclarationModel.InitializationModel as NormalizedFloatConstant;
            var normalizedConst = @const.Value;

            var value = $"{InAgentBuffer}[index].{variable.VariableDeclarationModel.Title}";
            var min = normalizedConst.Min.ToString();
            var max = normalizedConst.Max.ToString();

            var nomalization = HLSLNormalizeFunction.Execute(value, min, max);

            if (!Variables.TryGetValue(nomalization, out var localVar))
            {
                localVar = $"nomalized{variable.VariableDeclarationModel.Title}";
                Variables.Add(nomalization, localVar);
                m_LocalVariableDeclaration.AppendLine($"    {m_Precision} {localVar} = {nomalization};");

                VariablesSet.Add(new Property
                {
                    Name = variable.VariableDeclarationModel.Title,
                    Range = new Range { Max = normalizedConst.Max, Min = normalizedConst.Min },
                    Type = "float"
                });
            }

            return localVar;

        }

        protected override string SineCurveNodeModel(SineCurveNodeModel sineCurveNode)
        {
            var sineCurveResult = HLSLSineCurveFunction.Execute(
                    GetValue(sineCurveNode.InputPort, out _),
                     GetFloatStrWithDot(sineCurveNode.Steepness),
                      GetFloatStrWithDot(sineCurveNode.OffsetY)
                    );
            return SaveFunctionCallToVariable(sineCurveResult, "sineCurve", ref sineCurveCallCounter, m_Precision.ToString());
        }

        protected override string SmootherstepCurveNodeModel(SmootherstepCurveNodeModel smootherstepCurveNode)
        {
            var smootherstepCurveResult = HLSLSmootherstepCurveFunction.Execute(
                   GetValue(smootherstepCurveNode.InputPort, out _)
                   );
            return SaveFunctionCallToVariable(smootherstepCurveResult, "smootherstepCurve", ref smootherstepCurveCallCounter, m_Precision.ToString());
        }

        protected override string SmoothstepCurveNodeModel(SmoothstepCurveNodeModel smoothstepCurveNode)
        {
            var smoothstepCurveResult = HLSLSmoothstepCurveFunction.Execute(
                  GetValue(smoothstepCurveNode.InputPort, out _)
                  );
            return SaveFunctionCallToVariable(smoothstepCurveResult, "smoothstepCurve", ref smoothstepCurveCallCounter, m_Precision.ToString());
        }

        protected override string Value01NodeModel(Value01NodeModel value01)
        {
            return GetFloatStrWithDot(value01.Value01);
        }

        protected override string MultiplyNodeModel(Multiply01NodeModel mult01)
        {
            var ports = mult01.GetPorts(PortDirection.Input, PortType.Data).ToList();
            var paramsData = GetFunctionParams(ports);
            var result = PrepareFloatArray(paramsData.floatArrayParams, paramsData.paramsNames);

            if (!MultiplyFunctions.TryGetValue(ports.Count, out var multFunc))
            {
                multFunc = new HLSLMultiplyFunction(ports.Count, m_DecalarationFunctions, m_Precision);
                MultiplyFunctions.Add(ports.Count, multFunc);
            }


            return SaveFunctionCallToVariable(multFunc.Execute(result.arrayName), "multiply", ref multiplyArrayCounter, m_Precision.ToString());
        }

        private int customCurvesArrayCounter;
        protected override string CustomCurveNodeModel(CustomCurveNodeModel customCurve)
        {

            if (!CustomCurves.TryGetValue(customCurve.Guid, out var arrayName))
            {
                float step = 1f / 100f;
                arrayName = $"customCurveArray{customCurvesArrayCounter}";

                m_DecalarationConstanValues.AppendLine($"static const float {arrayName}[101] = {{");
                for (float x = 0; x < 1f; x += step)
                {
                    m_DecalarationConstanValues.AppendLine(GetTabsForLocal($"{GetFloatStrWithDot(Mathf.Clamp01(customCurve.CustomCurve.Evaluate(x)))},") );
                }

                m_DecalarationConstanValues.AppendLine($"}};");
                customCurvesArrayCounter++;

                CustomCurves.Add(customCurve.Guid, arrayName);
            }



            return $"{arrayName}[(int)({GetValue(customCurve.InputPort, out _)}*100)]";

        }

        protected override string Max01NodeModel(Max01NodeModel value01)
        {
            throw new NotImplementedException();
        }

        protected override string Min01NodeModel(Min01NodeModel mult01)
        {
            throw new NotImplementedException();
        }
    }
}
