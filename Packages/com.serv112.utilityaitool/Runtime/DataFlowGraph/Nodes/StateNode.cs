using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Burst;
using Unity.DataFlowGraph;
using UnityEngine;

namespace SerV112.UtilityAIRuntime
{
    public class StateNode : KernelNodeDefinition<StateNode.KernelDefs>, IInput
    {

        public struct KernelDefs : IKernelPortDefinition
        {
            public DataInput<StateNode, float> Input;
            public DataOutput<StateNode, float> Output;
        }

        struct KernelData : IKernelData { }

        [BurstCompile]
        struct GraphKernel : IGraphKernel<KernelData, KernelDefs>
        {
            public void Execute(RenderContext ctx, in KernelData data, ref KernelDefs ports)
            {
                ctx.Resolve(ref ports.Output) = ctx.Resolve(ports.Input);
            }
        }

        InputPortID ITaskPort<IInput>.GetPort(NodeHandle handle) => (InputPortID)StateNode.KernelPorts.Input;
    }
}
