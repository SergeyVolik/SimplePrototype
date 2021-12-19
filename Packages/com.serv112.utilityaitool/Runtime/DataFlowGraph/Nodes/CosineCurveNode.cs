using Unity.Burst;
using Unity.DataFlowGraph;

namespace SerV112.UtilityAIRuntime
{

    public class CosineCurveNode : SimulationKernelNodeDefinition<CosineCurveNode.SimPorts, CosineCurveNode.KernelDefs>, IInput
    {

        public struct KernelDefs : IKernelPortDefinition
        {
            public DataInput<CosineCurveNode, float> Input;
            public DataOutput<CosineCurveNode, float> Output;
        }

        struct KernelData : IKernelData {
            public float steepness;
            public float offset;
        }

        public struct SimPorts : ISimulationPortDefinition
        {
#pragma warning disable 649  // Assigned through internal DataFlowGraph reflection
            public MessageInput<CosineCurveNode, CosineCurveMessage> WeightPort;
#pragma warning restore 649
        }

        struct NodeHandler : INodeData, IMsgHandler<CosineCurveMessage>
        {
            public void HandleMessage(MessageContext ctx, in CosineCurveMessage msg) =>
                ctx.UpdateKernelData(new KernelData {  offset = msg.offset, steepness = msg.steepness });
            
        }

        [BurstCompile]
        struct GraphKernel : IGraphKernel<KernelData, KernelDefs>
        {
            public void Execute(RenderContext ctx, in KernelData data, ref KernelDefs ports)
            { 
                ctx.Resolve(ref ports.Output) = CurveUtils.CosineCurve(ctx.Resolve(ports.Input), data.steepness, data.offset);
            }
        }

        InputPortID ITaskPort<IInput>.GetPort(NodeHandle handle) => (InputPortID)CosineCurveNode.KernelPorts.Input;

    }
}
