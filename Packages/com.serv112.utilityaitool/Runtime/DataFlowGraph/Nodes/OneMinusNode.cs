using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Burst;
using Unity.DataFlowGraph;

namespace SerV112.UtilityAIRuntime
{
    public class OneMinusNode : KernelNodeDefinition<OneMinusNode.KernelDefs>, IInput
    {

        public struct KernelDefs : IKernelPortDefinition
        {
            public DataInput<OneMinusNode, float> Input;
            public DataOutput<OneMinusNode, float> Output;
        }

        struct KernelData : IKernelData { }

        [BurstCompile]
        struct GraphKernel : IGraphKernel<KernelData, KernelDefs>
        {
            public void Execute(RenderContext ctx, in KernelData data, ref KernelDefs ports)
            {
                ctx.Resolve(ref ports.Output) = 1 - ctx.Resolve(ports.Input);
            }
        }

        InputPortID ITaskPort<IInput>.GetPort(NodeHandle handle) => (InputPortID)OneMinusNode.KernelPorts.Input;
    }

}
