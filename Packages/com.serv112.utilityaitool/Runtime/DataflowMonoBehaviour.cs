using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.DataFlowGraph;
using SerV112.UtilityAIEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using Unity.Collections;

namespace SerV112.UtilityAIRuntime
{


    public class DataflowMonoBehaviour : MonoBehaviour
    {
        [SerializeField]
        AIGraphAssetModel m_Model;

        NodeSet m_Set;
        NativeList<NodeHandle> m_Nodes;
        NativeList<GraphValue<int>> m_GraphOutputs;
        Dictionary<string, NodeHandle<VariableNode>> m_Variables;

        public struct NodeAssetAndRuntime
        {
            public NodeModel modelAsset;
            public NodeHandle runtimeNode;
        }

        void StateGroupNodeModelConversion(NodeAssetAndRuntime model)
        {
            Debug.Log(model.modelAsset.Title);
            m_Nodes.Add(model.runtimeNode);

            var states = model.modelAsset.GetConnectedNodes(PortDirection.Input, PortType.Data).OfType<StateNodeModel>().ToList();
            var nullableHandle = (NodeHandle<SelectResultNode>)m_Set.As<SelectResultNode>(model.runtimeNode);
            m_Set.SetPortArraySize(nullableHandle, SelectResultNode.KernelPorts.Input, states.Count);
            m_GraphOutputs.Add(m_Set.CreateGraphValue(nullableHandle, SelectResultNode.KernelPorts.Output));


            for (int i = 0; i < states.Count; i++)
            {
                var stateNode = m_Set.Create<StateNode>();
                m_Set.Connect(stateNode, StateNode.KernelPorts.Output, nullableHandle, SelectResultNode.KernelPorts.Input, i);
                var modelRepres = new NodeAssetAndRuntime { modelAsset = states[i], runtimeNode = stateNode };
                NextNode(modelRepres);
            }
        }
        void ConnectNodesForPortArray<TNode>(NodeAssetAndRuntime model, PortArray<DataInput<TNode, float>> targetPort) where TNode : NodeDefinition
        {
            Debug.Log(model.modelAsset.Title);
            m_Nodes.Add(model.runtimeNode);
            var assetNodes = model.modelAsset.GetConnectedNodes(PortDirection.Input, PortType.Data).OfType<NormalizedFunctionNodeModel>().ToList();
            var targetNodeHandle = (NodeHandle<TNode>)m_Set.As<TNode>(model.runtimeNode);

            for (int i = 0; i < assetNodes.Count; i++)
            {
                switch (assetNodes[i])
                {
                    case NormalizationNodeModel normNode:
                        var normRuntime = m_Set.Create<NormalizationNode>();
                        m_Set.Connect(normRuntime, NormalizationNode.KernelPorts.Output, targetNodeHandle, targetPort, i);

                        NextNode(new NodeAssetAndRuntime { modelAsset = normNode, runtimeNode = normRuntime });
                        break;
                    case OneMinusNodeModel oneMinus:
                        var oneMinusRuntime = m_Set.Create<OneMinusNode>();

                        m_Set.Connect(oneMinusRuntime, OneMinusNode.KernelPorts.Output, targetNodeHandle, targetPort, i);
                        NextNode(new NodeAssetAndRuntime { modelAsset = oneMinus, runtimeNode = oneMinusRuntime });
                        break;
                    case SineCurveNodeModel sineCurve:
                        var sineCurveRuntime = m_Set.Create<SineCurveNode>();
                        m_Set.SendMessage(sineCurveRuntime, SineCurveNode.SimulationPorts.WeightPort, new SineCurveMessage { offset = sineCurve.Offset, steepness = sineCurve.Steepness });
                        m_Set.Connect(sineCurveRuntime, SineCurveNode.KernelPorts.Output, targetNodeHandle, targetPort, i);
                        NextNode(new NodeAssetAndRuntime { modelAsset = sineCurve, runtimeNode = sineCurveRuntime });
                        break;
                    case CosineCurveNodeModel cosineCurve:
                        var cosineCurveRuntime = m_Set.Create<CosineCurveNode>();
                        m_Set.SendMessage(cosineCurveRuntime, CosineCurveNode.SimulationPorts.WeightPort, new CosineCurveMessage { offset = cosineCurve.Offset, steepness = cosineCurve.Steepness });
                        m_Set.Connect(cosineCurveRuntime, CosineCurveNode.KernelPorts.Output, targetNodeHandle, targetPort, i);
                        NextNode(new NodeAssetAndRuntime { modelAsset = cosineCurve, runtimeNode = cosineCurveRuntime });
                        break;
                    case ExponentialCurveNodeModel expCurve:
                        var expCurveRuntime = m_Set.Create<ExponentialCurveNode>();
                        m_Set.SendMessage(expCurveRuntime, ExponentialCurveNode.SimulationPorts.WeightPort, new ExponentialCurveMessage { offset = expCurve.Offset, exponent = expCurve.Exponent });
                        m_Set.Connect(expCurveRuntime, ExponentialCurveNode.KernelPorts.Output, targetNodeHandle, targetPort, i);
                        NextNode(new NodeAssetAndRuntime { modelAsset = expCurve, runtimeNode = expCurveRuntime });
                        break;
                    case LinearCurveNodeModel linearCurve:
                        var linearCurveRuntime = m_Set.Create<LinearCurveNode>();
                        m_Set.SendMessage(linearCurveRuntime, LinearCurveNode.SimulationPorts.WeightPort, new LinearCurveMessage { offset = linearCurve.Offset, slope = linearCurve.Slope });
                        m_Set.Connect(linearCurveRuntime, LinearCurveNode.KernelPorts.Output, targetNodeHandle, targetPort, i);
                        NextNode(new NodeAssetAndRuntime { modelAsset = linearCurve, runtimeNode = linearCurveRuntime });
                        break;
                    case LogisticCurveNodeModel logisticCurve:
                        var logisticCurveeRuntime = m_Set.Create<LogisticCurveNode>();
                        m_Set.SendMessage(logisticCurveeRuntime, LogisticCurveNode.SimulationPorts.WeightPort, new LogisticCurveMessage { offset = logisticCurve.Offset, logBase = logisticCurve.Steepness });
                        m_Set.Connect(logisticCurveeRuntime, LogisticCurveNode.KernelPorts.Output, targetNodeHandle, targetPort, i);
                        NextNode(new NodeAssetAndRuntime { modelAsset = logisticCurve, runtimeNode = logisticCurveeRuntime });
                        break;

                    case LogitCurveNodeModel logitCurve:
                        var logitCurveRuntime = m_Set.Create<LogitCurveNode>();
                        m_Set.SendMessage(logitCurveRuntime, LogitCurveNode.SimulationPorts.WeightPort, new LogitCurveMessage { logBase = logitCurve.LogBase });
                        m_Set.Connect(logitCurveRuntime, LogitCurveNode.KernelPorts.Output, targetNodeHandle, targetPort, i);
                        NextNode(new NodeAssetAndRuntime { modelAsset = logitCurve, runtimeNode = logitCurveRuntime });
                        break;

                    case SmootherstepCurveNodeModel smootherstepCurve:
                        var smootherstepCurveRuntime = m_Set.Create<SmootherstepCurveNode>();
                        m_Set.Connect(smootherstepCurveRuntime, SmootherstepCurveNode.KernelPorts.Output, targetNodeHandle, targetPort, i);
                        NextNode(new NodeAssetAndRuntime { modelAsset = smootherstepCurve, runtimeNode = smootherstepCurveRuntime });
                        break;

                    case SmoothstepCurveNodeModel smoothstepCurve:
                        var smoothstepCurveRuntime = m_Set.Create<SmoothstepCurveNode>();
                        m_Set.Connect(smoothstepCurveRuntime, SmoothstepCurveNode.KernelPorts.Output, targetNodeHandle, targetPort, i);
                        NextNode(new NodeAssetAndRuntime { modelAsset = smoothstepCurve, runtimeNode = smoothstepCurveRuntime });
                        break;
                    case AverageNodeModel average:
                        var averageRuntime = m_Set.Create<AverageNode>();
                        m_Set.Connect(averageRuntime, AverageNode.KernelPorts.Output, targetNodeHandle, targetPort, i);
                        NextNode(new NodeAssetAndRuntime { modelAsset = average, runtimeNode = averageRuntime });
                        break;

                }
            }
        }
        void ConnectNodes<TNode>(NodeAssetAndRuntime model, DataInput<TNode, float> targetPort) where TNode : NodeDefinition
        {
            Debug.Log(model.modelAsset.Title);
            m_Nodes.Add(model.runtimeNode);
            var assetNodes = model.modelAsset.GetConnectedNodes(PortDirection.Input, PortType.Data).OfType<NormalizedFunctionNodeModel>().ToList();
            var targetNodeHandle = (NodeHandle<TNode>)m_Set.As<TNode>(model.runtimeNode);

            for (int i = 0; i < assetNodes.Count; i++)
            {
                switch (assetNodes[i])
                {
                    case NormalizationNodeModel normNode:
                        var normRuntime = m_Set.Create<NormalizationNode>();
                        m_Set.Connect(normRuntime, NormalizationNode.KernelPorts.Output, targetNodeHandle, targetPort);

                        NextNode(new NodeAssetAndRuntime { modelAsset = normNode, runtimeNode = normRuntime });
                        break;
                    case OneMinusNodeModel oneMinus:
                        var oneMinusRuntime = m_Set.Create<OneMinusNode>();

                        m_Set.Connect(oneMinusRuntime, OneMinusNode.KernelPorts.Output, targetNodeHandle, targetPort);
                        NextNode(new NodeAssetAndRuntime { modelAsset = oneMinus, runtimeNode = oneMinusRuntime });
                        break;
                    case SineCurveNodeModel sineCurve:
                        var sineCurveRuntime = m_Set.Create<SineCurveNode>();
                        m_Set.SendMessage(sineCurveRuntime, SineCurveNode.SimulationPorts.WeightPort, new SineCurveMessage { offset = sineCurve.Offset, steepness = sineCurve.Steepness });
                        m_Set.Connect(sineCurveRuntime, SineCurveNode.KernelPorts.Output, targetNodeHandle, targetPort);
                        NextNode(new NodeAssetAndRuntime { modelAsset = sineCurve, runtimeNode = sineCurveRuntime });
                        break;
                    case CosineCurveNodeModel cosineCurve:
                        var cosineCurveRuntime = m_Set.Create<CosineCurveNode>();
                        m_Set.SendMessage(cosineCurveRuntime, CosineCurveNode.SimulationPorts.WeightPort, new CosineCurveMessage { offset = cosineCurve.Offset, steepness = cosineCurve.Steepness });
                        m_Set.Connect(cosineCurveRuntime, CosineCurveNode.KernelPorts.Output, targetNodeHandle, targetPort);
                        NextNode(new NodeAssetAndRuntime { modelAsset = cosineCurve, runtimeNode = cosineCurveRuntime });
                        break;
                    case ExponentialCurveNodeModel expCurve:
                        var expCurveRuntime = m_Set.Create<ExponentialCurveNode>();
                        m_Set.SendMessage(expCurveRuntime, ExponentialCurveNode.SimulationPorts.WeightPort, new ExponentialCurveMessage { offset = expCurve.Offset, exponent = expCurve.Exponent });
                        m_Set.Connect(expCurveRuntime, ExponentialCurveNode.KernelPorts.Output, targetNodeHandle, targetPort);
                        NextNode(new NodeAssetAndRuntime { modelAsset = expCurve, runtimeNode = expCurveRuntime });
                        break;
                    case LinearCurveNodeModel linearCurve:
                        var linearCurveRuntime = m_Set.Create<LinearCurveNode>();
                        m_Set.SendMessage(linearCurveRuntime, LinearCurveNode.SimulationPorts.WeightPort, new LinearCurveMessage { offset = linearCurve.Offset, slope = linearCurve.Slope });
                        m_Set.Connect(linearCurveRuntime, LinearCurveNode.KernelPorts.Output, targetNodeHandle, targetPort);
                        NextNode(new NodeAssetAndRuntime { modelAsset = linearCurve, runtimeNode = linearCurveRuntime });
                        break;
                    case LogisticCurveNodeModel logisticCurve:
                        var logisticCurveeRuntime = m_Set.Create<LogisticCurveNode>();
                        m_Set.SendMessage(logisticCurveeRuntime, LogisticCurveNode.SimulationPorts.WeightPort, new LogisticCurveMessage { offset = logisticCurve.Offset, logBase = logisticCurve.Steepness });
                        m_Set.Connect(logisticCurveeRuntime, LogisticCurveNode.KernelPorts.Output, targetNodeHandle, targetPort);
                        NextNode(new NodeAssetAndRuntime { modelAsset = logisticCurve, runtimeNode = logisticCurveeRuntime });
                        break;

                    case LogitCurveNodeModel logitCurve:
                        var logitCurveRuntime = m_Set.Create<LogitCurveNode>();
                        m_Set.SendMessage(logitCurveRuntime, LogitCurveNode.SimulationPorts.WeightPort, new LogitCurveMessage {logBase = logitCurve.LogBase });
                        m_Set.Connect(logitCurveRuntime, LogitCurveNode.KernelPorts.Output, targetNodeHandle, targetPort);
                        NextNode(new NodeAssetAndRuntime { modelAsset = logitCurve, runtimeNode = logitCurveRuntime });
                        break;

                    case SmootherstepCurveNodeModel smootherstepCurve:
                        var smootherstepCurveRuntime = m_Set.Create<SmootherstepCurveNode>();                 
                        m_Set.Connect(smootherstepCurveRuntime, SmootherstepCurveNode.KernelPorts.Output, targetNodeHandle, targetPort);
                        NextNode(new NodeAssetAndRuntime { modelAsset = smootherstepCurve, runtimeNode = smootherstepCurveRuntime });
                        break;

                    case SmoothstepCurveNodeModel smoothstepCurve:
                        var smoothstepCurveRuntime = m_Set.Create<SmoothstepCurveNode>();
                        m_Set.Connect(smoothstepCurveRuntime, SmoothstepCurveNode.KernelPorts.Output, targetNodeHandle, targetPort);
                        NextNode(new NodeAssetAndRuntime { modelAsset = smoothstepCurve, runtimeNode = smoothstepCurveRuntime });
                        break;
                    case AverageNodeModel average:
                        var averageRuntime = m_Set.Create<AverageNode>();
                        m_Set.Connect(averageRuntime, AverageNode.KernelPorts.Output, targetNodeHandle, targetPort);
                        NextNode(new NodeAssetAndRuntime { modelAsset = average, runtimeNode = averageRuntime });
                        break;

                }
            }
        }


        void AddVariableNode(NodeHandle<NormalizationNode> handle, IPortModel port, DataInput<NormalizationNode, float> runtimePort)      
        {
            var inputPortEdges = port.GetConnectedEdges().ToList();


            if (inputPortEdges.Count == 0)
            {
                m_Set.SetData(handle, runtimePort, (float)port.EmbeddedValue.ObjectValue);
                return;
            }
                       
            var inputVar = inputPortEdges[0].FromPort.NodeModel as VariableNodeModel;        
            var data = (float)inputVar.VariableDeclarationModel.InitializationModel.ObjectValue;
            Debug.Log($"{inputVar.Title} value={data}");

            if (!m_Variables.TryGetValue(inputVar.Title, out var variable))
            {
               
                var runtimeNode = m_Set.Create<VariableNode>();
                m_Set.SendMessage(runtimeNode, VariableNode.SimulationPorts.SetDataPort, new SetVariableMessage { Value = data });
                m_Set.Connect(runtimeNode, VariableNode.KernelPorts.Output, handle, runtimePort);
                m_Variables.Add(inputVar.Title, runtimeNode);
                m_Nodes.Add(runtimeNode);
                return;
            }

            m_Set.Connect(variable, VariableNode.KernelPorts.Output, handle, runtimePort);



        }

        void NormalizationNodeConverstion(NodeAssetAndRuntime model)
        {
            var normNode = model.modelAsset as NormalizationNodeModel;
            Debug.Log(normNode.Title);
            m_Nodes.Add(model.runtimeNode);
            var handle = (NodeHandle<NormalizationNode>)m_Set.As<NormalizationNode>(model.runtimeNode);


            AddVariableNode(handle, normNode.InputPort, NormalizationNode.KernelPorts.Input);
            AddVariableNode(handle, normNode.MaxPort, NormalizationNode.KernelPorts.Max);
            AddVariableNode(handle, normNode.MinPort, NormalizationNode.KernelPorts.Min);

        }

        void NextNode(NodeAssetAndRuntime model)
        {
           
            switch (model.modelAsset)
            {
                case StateGroupNodeModel stateGroup:

                    StateGroupNodeModelConversion(model);

                    break;
                case StateNodeModel state:
                    //StateNodeModelConversion(model);
                    ConnectNodes<StateNode>(model, StateNode.KernelPorts.Input);

                    break;
                case AverageNodeModel averNode:

                    Debug.Log(averNode.Title);
                    ConnectNodesForPortArray<AverageNode>(model, AverageNode.KernelPorts.Input);
                    break;

                case CosineCurveNodeModel cosineCurveNode:
                    Debug.Log(cosineCurveNode.Title);
                    ConnectNodes<CosineCurveNode>(model, CosineCurveNode.KernelPorts.Input);
                    break;
                case OneMinusNodeModel oneMinus:
                    ConnectNodes<OneMinusNode>(model, OneMinusNode.KernelPorts.Input);
                    //OneMinusNodeConversion(model);
                    break;
                case ExponentialCurveNodeModel expCurveNode:
                    Debug.Log(expCurveNode.Title);
                    ConnectNodes<ExponentialCurveNode>(model, ExponentialCurveNode.KernelPorts.Input);
                    break;
                case LinearCurveNodeModel linearCurveNode:
                    Debug.Log(linearCurveNode.Title);
                    ConnectNodes<LinearCurveNode>(model, LinearCurveNode.KernelPorts.Input);
                    break;

                case LogisticCurveNodeModel logisticNode:
                    Debug.Log(logisticNode.Title);
                    ConnectNodes<LogisticCurveNode>(model, LogisticCurveNode.KernelPorts.Input);
                    break;
                case LogitCurveNodeModel logitNode:
                    Debug.Log(logitNode.Title);
                    ConnectNodes<LogitCurveNode>(model, LogitCurveNode.KernelPorts.Input);
                    break;
                case NormalizationNodeModel normNode:
                    NormalizationNodeConverstion(model);
                   
                    break;
                case SineCurveNodeModel sineCurveNode:
                    Debug.Log(sineCurveNode.Title);
                    ConnectNodes<SineCurveNode>(model, SineCurveNode.KernelPorts.Input);
                    break;
                case SmootherstepCurveNodeModel smootherstepCurveNode:
                    Debug.Log(smootherstepCurveNode.Title);
                    ConnectNodes<SmootherstepCurveNode>(model, SmootherstepCurveNode.KernelPorts.Input);
                    break;
                case SmoothstepCurveNodeModel smoothstepCurveNode:
                    Debug.Log(smoothstepCurveNode.Title);
                    ConnectNodes<SmoothstepCurveNode>(model, SmoothstepCurveNode.KernelPorts.Input);
                    break;

                default:
                    Debug.Log(model);
                    break;
            }


        }

        private void OnEnable()
        {
            m_Set = new NodeSet();
            m_Nodes = new NativeList<NodeHandle>(Allocator.Persistent);
            m_GraphOutputs = new NativeList<GraphValue<int>>(Allocator.Persistent);
            m_Variables = new Dictionary<string, NodeHandle<VariableNode>>();
            var models = m_Model.GraphModel.NodeModels.OfType<AIProcessorNodeModel>().ToList();

            for (int j = 0; j < 100; j++)
            {
                for (int i = 0; i < models.Count; i++)
                {
                    var nodes = models[i].GetConnectedNodes(PortDirection.Input, PortType.Data).OfType<StateGroupNodeModel>().ToList();

                    for (int k = 0; k < nodes.Count; k++)
                    {
                        var resultNode = m_Set.Create<SelectResultNode>();

                        NextNode(new NodeAssetAndRuntime { runtimeNode = resultNode, modelAsset = nodes[k] });
                    }

                }
            }
          
          

            m_Set.Update();

            for (int i = 0; i < m_GraphOutputs.Length; i++)
            {
                var current = m_Set.GetValueBlocking(m_GraphOutputs[i]);

           
                Debug.Log($"Processor output 1: {current}");
            }


        }

        private void OnDisable()
        {
            for (int i = 0; i < m_Nodes.Length; ++i)
                m_Set.Destroy(m_Nodes[i]);

            for (int i = 0; i < m_GraphOutputs.Length; i++)            
                m_Set.ReleaseGraphValue(m_GraphOutputs[i]);        
            
          
            m_Nodes.Dispose();
            m_Set.Dispose();
            m_Variables.Clear();
        }

        private void Update()
        {
            m_Set.Update();
        }
    }
}