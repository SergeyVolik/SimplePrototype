
using Unity.Burst;
using Unity.DataFlowGraph;

namespace SerV112.UtilityAIRuntime
{

    public class ExponentialCurveNode : SimulationKernelNodeDefinition<ExponentialCurveNode.SimPorts, ExponentialCurveNode.KernelDefs>, IInput
    {

        public struct KernelDefs : IKernelPortDefinition
        {
            public DataInput<ExponentialCurveNode, float> Input;
            public DataOutput<ExponentialCurveNode, float> Output;
        }

        struct KernelData : IKernelData
        {
            public float exponent;
            public float offset;
        }
        public struct SimPorts : ISimulationPortDefinition
        {
#pragma warning disable 649  // Assigned through internal DataFlowGraph reflection
            public MessageInput<ExponentialCurveNode, ExponentialCurveMessage> WeightPort;
#pragma warning restore 649
        }

        struct NodeHandler : INodeData, IMsgHandler<ExponentialCurveMessage>
        {
            public void HandleMessage(MessageContext ctx, in ExponentialCurveMessage msg)
            {
                /*
                 * To update the kernel data from inside the simulation we have the UpdateKernelData() API.
                 */
                ctx.UpdateKernelData(new KernelData { offset = msg.offset, exponent = msg.exponent });
            }
        }

        [BurstCompile]
        struct GraphKernel : IGraphKernel<KernelData, KernelDefs>
        {
            public void Execute(RenderContext ctx, in KernelData data, ref KernelDefs ports)
            {
                ctx.Resolve(ref ports.Output) = CurveUtils.ExponentialCurve(ctx.Resolve(ports.Input), data.exponent, data.offset);
            }
        }

        InputPortID ITaskPort<IInput>.GetPort(NodeHandle handle) => (InputPortID)ExponentialCurveNode.KernelPorts.Input;

    }
}
