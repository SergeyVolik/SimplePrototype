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
    public abstract class CodeGenerator
    {
        protected Dictionary<string, string> FunctionCalls = new Dictionary<string, string>();
        protected StringBuilder m_LocalVariableDeclaration = new StringBuilder();
        protected Dictionary<string, string> Variables = new Dictionary<string, string>();
        protected Dictionary<string, string> initedFloatArrays = new Dictionary<string, string>();
        protected Dictionary<SerializableGUID, string> CustomCurves = new Dictionary<SerializableGUID, string>();
        protected AIGraphAssetModel m_Asset;
        protected AIStencil m_Stencil;
        protected string m_FileContent;
        protected string m_Filename;
        protected string m_FilePath;
        protected string m_FileExt;

        protected int m_TabsForClass;
        protected int m_TabsForFunctions;
        protected int m_TabsForLocalVars;
        public CodeGenerator(AIGraphAssetModel Asset, AIStencil Stencil, string Filename, string FilePath, string FileExt)
        {
            m_Asset = Asset;
            m_Stencil = Stencil;
            m_Filename = Filename;
            m_FilePath = FilePath;
            m_FileExt = FileExt;

            m_TabsForClass = 0;
            m_TabsForFunctions = 1;
            m_TabsForLocalVars = 2;
        }

        protected string GetFloatStrWithDot(float value)
        {
            return value.ToString("0.00f", System.Globalization.CultureInfo.InvariantCulture);
        }
        protected string GetStringWithTabs(string str, int tabs)
        {
            string tabs1 = "";
            for (int i = 0; i < tabs; i++)
            {
                tabs1 += "\t";
            }
            return String.Format("{0}{1}", tabs1, str);
        }

        protected private string GetTabsForLocal(string str)
        {
            return GetStringWithTabs(str, m_TabsForLocalVars);
        }
        protected private string GetTabsForFunctions(string str)
        {
            return GetStringWithTabs(str, m_TabsForFunctions);
        }
        protected private string GetTabsForClass(string str)
        {
            return GetStringWithTabs(str, m_TabsForClass);
        }
        protected (List<string> paramsNames, string floatArrayParams) GetFunctionParams(List<IPortModel> ports)
        {

            var paramsNames = new List<string>();
            var floatArrayParams = "";
            for (int i = 0; i < ports.Count; i++)
            {
                var param = GetValue(ports[i], out _);
                floatArrayParams += param;
                paramsNames.Add(param);

            }

            return (paramsNames, floatArrayParams);
        }

        protected string NextNode(NodeModel nodeModel)
        {

            switch (nodeModel)
            {
                case AIProcessorNodeModel processor:

                    return AIProcessorNodeModelNode(processor);


                case StateGroupNodeModel stateGroup:
                    return StateGroupNodeModel(stateGroup);

                case StateNodeModel state:

                    return StateNodeModel(state);

                case AverageNodeModel averNode:

                    return AverageNodeModel(averNode);


                case CosineCurveNodeModel cosineCurveNode:
                    return CosineCurveNodeModel(cosineCurveNode);

                case OneMinusNodeModel oneMinus:

                    return OneMinusNodeModel(oneMinus);

                case ExponentialCurveNodeModel expCurveNode:
                    return ExponentialCurveNodeModel(expCurveNode);



                case LinearCurveNodeModel linearCurveNode:

                    return LinearCurveNodeModel(linearCurveNode);

                case LogisticCurveNodeModel logisticNode:
                    return LogisticCurveNodeModel(logisticNode);

                case LogitCurveNodeModel logitNode:

                    return LogitCurveNodeModel(logitNode);


                case VariableNodeModel variable:

                    return VariableNodeModel(variable);


                case SineCurveNodeModel sineCurveNode:


                    return SineCurveNodeModel(sineCurveNode);

                case SmootherstepCurveNodeModel smootherstepCurveNode:
                    return SmootherstepCurveNodeModel(smootherstepCurveNode);



                case SmoothstepCurveNodeModel smoothstepCurveNode:

                    return SmoothstepCurveNodeModel(smoothstepCurveNode);

                case Value01NodeModel value01:
                    return Value01NodeModel(value01);

                case Multiply01NodeModel mult01:
                    return MultiplyNodeModel(mult01);

                case Max01NodeModel max01:
                    return Max01NodeModel(max01);

                case Min01NodeModel min01:
                    return Min01NodeModel(min01);

                case CustomCurveNodeModel customCurve:
                    return CustomCurveNodeModel(customCurve);

                default:
                    Debug.Log(nodeModel);
                    break;
            }

            return "unavailable node";


        }

      

        protected string GetValue(IPortModel port, out string name)
        {
            var inputEdges = port.GetConnectedEdges().ToList();

            string value;

            if (inputEdges.Count() > 0)
            {
                var model = inputEdges[0].FromPort.NodeModel as NodeModel;
                name = model.Title;
                value = NextNode(model);

            }
            else
            {
                value = ((float)port.EmbeddedValue.ObjectValue).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                name = null;
            }

            return value;
        }


        protected string SaveFunctionCallToVariable(string functionCall, string varibaleName, ref int functionCallCounter, string varType, bool cast = false)
        {
            if (!FunctionCalls.TryGetValue(functionCall, out var localVarCosine))
            {
                localVarCosine = $"{varibaleName}{functionCallCounter}";
                FunctionCalls.Add(functionCall, localVarCosine);

                string type = varType;


                if(!cast)
                    m_LocalVariableDeclaration.AppendLine(GetTabsForLocal($"{type} {localVarCosine} = {functionCall};"));
                else m_LocalVariableDeclaration.AppendLine(GetTabsForLocal($"{type} {localVarCosine} = ({type}){functionCall};"));
                functionCallCounter++;
            }

            return localVarCosine;
        }

        protected string SaveFunctionCallToVariableWithoutCashe(string functionCall, string varibaleName, ref int functionCallCounter, string varType, bool cast = false)
        {
           
            var localVarCosine = $"{varibaleName}{functionCallCounter}";


            string type = varType;


            if (!cast)
                m_LocalVariableDeclaration.AppendLine(GetTabsForLocal($"{type} {localVarCosine} = {functionCall};"));
            else m_LocalVariableDeclaration.AppendLine(GetTabsForLocal($"{type} {localVarCosine} = ({type}){functionCall};"));

            

            return localVarCosine;
        }
        protected void SaveFunctionCallToExistedVariable(string functionCall, string varibaleName)
        {

                m_LocalVariableDeclaration.AppendLine(GetTabsForLocal($"{varibaleName} = {functionCall};"));

        }

        protected abstract string Max01NodeModel(Max01NodeModel value01);
        protected abstract string Min01NodeModel(Min01NodeModel mult01);

        protected abstract string Value01NodeModel(Value01NodeModel value01);
        protected abstract string MultiplyNodeModel(Multiply01NodeModel mult01);
        protected abstract string CustomCurveNodeModel(CustomCurveNodeModel customCurve);

        protected abstract string AIProcessorNodeModelNode(AIProcessorNodeModel processor);

        protected abstract string StateGroupNodeModel(StateGroupNodeModel stateGroup);
        protected abstract string StateNodeModel(StateNodeModel state);

        protected abstract string CosineCurveNodeModel(CosineCurveNodeModel cosineCurveNode);
        protected abstract string AverageNodeModel(AverageNodeModel averNode);

        protected abstract string OneMinusNodeModel(OneMinusNodeModel oneMinus);

        protected abstract string ExponentialCurveNodeModel(ExponentialCurveNodeModel expCurveNode);

        protected abstract string LinearCurveNodeModel(LinearCurveNodeModel linearCurveNode);

        protected abstract string LogisticCurveNodeModel(LogisticCurveNodeModel logisticNode);

        protected abstract string LogitCurveNodeModel(LogitCurveNodeModel logitNode);

        protected abstract string VariableNodeModel(VariableNodeModel variable);

        protected abstract string SineCurveNodeModel(SineCurveNodeModel sineCurveNode);

        protected abstract string SmootherstepCurveNodeModel(SmootherstepCurveNodeModel smootherstepCurveNode);

        protected abstract string SmoothstepCurveNodeModel(SmoothstepCurveNodeModel smoothstepCurveNode);

        protected abstract void DeclarationPart();

        protected abstract void CodePart();

        public void GenerateAndSave()
        {
            DeclarationPart();
            CodePart();
            SaveFilePart();






        }
        private void SaveFilePart()
        {

            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create($"{m_FilePath}/{m_Filename}.{m_FileExt}"))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(m_FileContent);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

            }

            catch (Exception ex)
            {
                Debug.LogError(ex.ToString());
            }
        }

    }
}
