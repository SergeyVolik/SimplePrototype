using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Burst;
using Unity.DataFlowGraph;

namespace SerV112.UtilityAIRuntime
{
    public class VariableNode : SimulationKernelNodeDefinition<VariableNode.SimPorts, VariableNode.KernelDefs>
    {

        public struct KernelDefs : IKernelPortDefinition
        {
            public DataOutput<VariableNode, float> Output;
        }

        struct KernelData : IKernelData {
            public float Value;
        }

        public struct SimPorts : ISimulationPortDefinition
        {
#pragma warning disable 649  // Assigned through internal DataFlowGraph reflection
            public MessageInput<VariableNode, SetVariableMessage> SetDataPort;
#pragma warning restore 649
        }

        struct NodeHandler : INodeData, IMsgHandler<SetVariableMessage>
        {
            public void HandleMessage(MessageContext ctx, in SetVariableMessage msg) =>
                ctx.UpdateKernelData(new KernelData { Value = msg.Value });

        }

        [BurstCompile]
        struct GraphKernel : IGraphKernel<KernelData, KernelDefs>
        {
            public void Execute(RenderContext ctx, in KernelData data, ref KernelDefs ports)
            {
                ctx.Resolve(ref ports.Output) = data.Value;
            }
        }
    }
}
