using Unity.Burst;
using Unity.DataFlowGraph;

namespace SerV112.UtilityAIRuntime
{

    public class LogitCurveNode : SimulationKernelNodeDefinition<LogitCurveNode.SimPorts, LogitCurveNode.KernelDefs>, IInput
    {

        public struct KernelDefs : IKernelPortDefinition
        {
            public DataInput<LogitCurveNode, float> Input;
            public DataOutput<LogitCurveNode, float> Output;
        }

        struct KernelData : IKernelData {
            public float baseLog;
        }

        public struct SimPorts : ISimulationPortDefinition
        {
#pragma warning disable 649  // Assigned through internal DataFlowGraph reflection
            public MessageInput<LogitCurveNode, LogitCurveMessage> WeightPort;
#pragma warning restore 649
        }

        struct NodeHandler : INodeData, IMsgHandler<LogitCurveMessage>
        {
            public void HandleMessage(MessageContext ctx, in LogitCurveMessage msg) =>
                ctx.UpdateKernelData(new KernelData {  baseLog = msg.logBase });
            
        }

        [BurstCompile]
        struct GraphKernel : IGraphKernel<KernelData, KernelDefs>
        {
            public void Execute(RenderContext ctx, in KernelData data, ref KernelDefs ports)
            { 
                ctx.Resolve(ref ports.Output) = CurveUtils.LogitCurve(ctx.Resolve(ports.Input), data.baseLog);
            }
        }

        InputPortID ITaskPort<IInput>.GetPort(NodeHandle handle) => (InputPortID)CosineCurveNode.KernelPorts.Input;

    }
}
