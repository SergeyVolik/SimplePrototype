using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Burst;
using Unity.DataFlowGraph;

namespace SerV112.UtilityAIRuntime
{
    public class LogisticCurveNode : SimulationKernelNodeDefinition<LogisticCurveNode.SimPorts, LogisticCurveNode.KernelDefs>, IInput
    {

        public struct KernelDefs : IKernelPortDefinition
        {
            public DataInput<LogisticCurveNode, float> Input;
            public DataOutput<LogisticCurveNode, float> Output;
        }

        struct KernelData : IKernelData
        {
            public float logBase;
            public float offset;
        }

        public struct SimPorts : ISimulationPortDefinition
        {
#pragma warning disable 649  // Assigned through internal DataFlowGraph reflection
            public MessageInput<LogisticCurveNode, LogisticCurveMessage> WeightPort;
#pragma warning restore 649
        }
        struct NodeHandler : INodeData, IMsgHandler<LogisticCurveMessage>
        {
            public void HandleMessage(MessageContext ctx, in LogisticCurveMessage msg)
            {
                /*
                 * To update the kernel data from inside the simulation we have the UpdateKernelData() API.
                 */
                ctx.UpdateKernelData(new KernelData { offset = msg.offset, logBase = msg.logBase });
            }
        }

        [BurstCompile]
        struct GraphKernel : IGraphKernel<KernelData, KernelDefs>
        {
            public void Execute(RenderContext ctx, in KernelData data, ref KernelDefs ports)
            {
                ctx.Resolve(ref ports.Output) = CurveUtils.LogisticCurve(ctx.Resolve(ports.Input), data.logBase, data.offset);
            }
        }

        InputPortID ITaskPort<IInput>.GetPort(NodeHandle handle) => (InputPortID)LogisticCurveNode.KernelPorts.Input;
    }
}
