using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Burst;
using Unity.DataFlowGraph;

namespace SerV112.UtilityAIRuntime
{

    public class SineCurveNode: SimulationKernelNodeDefinition<SineCurveNode.SimPorts, SineCurveNode.KernelDefs>, IInput
    {

        public struct KernelDefs : IKernelPortDefinition
        {
            public DataInput<SineCurveNode, float> Input;
            public DataOutput<SineCurveNode, float> Output;
        }

        struct KernelData : IKernelData
        {
            public float steepness;
            public float offset;
        }

        public struct SimPorts : ISimulationPortDefinition
        {
#pragma warning disable 649  // Assigned through internal DataFlowGraph reflection
            public MessageInput<SineCurveNode, SineCurveMessage> WeightPort;
#pragma warning restore 649
        }

        struct NodeHandler : INodeData, IMsgHandler<SineCurveMessage>
        {
            public void HandleMessage(MessageContext ctx, in SineCurveMessage msg)
            {
                /*
                 * To update the kernel data from inside the simulation we have the UpdateKernelData() API.
                 */
                ctx.UpdateKernelData(new KernelData { offset = msg.offset, steepness = msg.steepness });
            }
        }

        [BurstCompile]
        struct GraphKernel : IGraphKernel<KernelData, KernelDefs>
        {
            public void Execute(RenderContext ctx, in KernelData data, ref KernelDefs ports)
            {
                ctx.Resolve(ref ports.Output) = CurveUtils.SineCurve(ctx.Resolve(ports.Input), data.steepness, data.offset);
            }
        }

        InputPortID ITaskPort<IInput>.GetPort(NodeHandle handle) => (InputPortID)SineCurveNode.KernelPorts.Input;
    }
}
