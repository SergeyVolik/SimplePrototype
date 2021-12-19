using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Burst;
using Unity.DataFlowGraph;

namespace SerV112.UtilityAIRuntime
{
    public class LinearCurveNode : SimulationKernelNodeDefinition<LinearCurveNode.SimPorts, LinearCurveNode.KernelDefs>, IInput

    {

        public struct KernelDefs : IKernelPortDefinition
        {
            public DataInput<LinearCurveNode, float> Input;
            public DataOutput<LinearCurveNode, float> Output;
        }

        struct KernelData : IKernelData
        {
            public float slope;
            public float offset;
        }

        public struct SimPorts : ISimulationPortDefinition
        {
#pragma warning disable 649  // Assigned through internal DataFlowGraph reflection
            public MessageInput<LinearCurveNode, LinearCurveMessage> WeightPort;
#pragma warning restore 649
        }
        struct NodeHandler : INodeData, IMsgHandler<LinearCurveMessage>
        {
            public void HandleMessage(MessageContext ctx, in LinearCurveMessage msg)
            {
                /*
                 * To update the kernel data from inside the simulation we have the UpdateKernelData() API.
                 */
                ctx.UpdateKernelData(new KernelData { offset = msg.offset, slope = msg.slope });
            }
        }

        [BurstCompile]
        struct GraphKernel : IGraphKernel<KernelData, KernelDefs>
        {
            public void Execute(RenderContext ctx, in KernelData data, ref KernelDefs ports)
            {
                ctx.Resolve(ref ports.Output) = CurveUtils.LinearCurve(ctx.Resolve(ports.Input), data.slope, data.offset);
            }
        }

        InputPortID ITaskPort<IInput>.GetPort(NodeHandle handle) => (InputPortID)LinearCurveNode.KernelPorts.Input;
    }
}
