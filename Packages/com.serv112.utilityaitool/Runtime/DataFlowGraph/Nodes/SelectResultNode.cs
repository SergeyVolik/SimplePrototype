using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Burst;
using Unity.DataFlowGraph;
using Unity.Mathematics;
using UnityEngine;

namespace SerV112.UtilityAIRuntime
{

    public class SelectResultNode : KernelNodeDefinition<SelectResultNode.KernelDefs>
    {

        public struct KernelDefs : IKernelPortDefinition
        {
            public PortArray<DataInput<SelectResultNode, float>> Input;
            public DataOutput<SelectResultNode, int> Output;
        }

        struct KernelData : IKernelData { }

        [BurstCompile]
        struct GraphKernel : IGraphKernel<KernelData, KernelDefs>
        {
            public void Execute(RenderContext ctx, in KernelData data, ref KernelDefs ports)
            {
                var portArray = ctx.Resolve(ports.Input);

                int index = 0;
                float max = -math.INFINITY;
                if (portArray.Length > 0)
                {

                    for (int i = 0; i < portArray.Length; ++i)
                    {
                        if (max < portArray[i])
                        {
                            index = i;
                            max = portArray[i];
                        }
                    }

                }

                ctx.Resolve(ref ports.Output) = index;
            }
        }

    }
}
