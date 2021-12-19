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

    public class AverageNode : KernelNodeDefinition<AverageNode.KernelDefs>
    {

        public struct KernelDefs : IKernelPortDefinition
        {
            public PortArray<DataInput<AverageNode, float>> Input;
            public DataOutput<AverageNode, float> Output;
        }

        struct KernelData : IKernelData { }

        [BurstCompile]
        struct GraphKernel : IGraphKernel<KernelData, KernelDefs>
        {
            public void Execute(RenderContext ctx, in KernelData data, ref KernelDefs ports)
            {               
                var portArray = ctx.Resolve(ports.Input);

                float average = 0;
                if (portArray.Length > 0)
                {
                    
                    for (int i = 0; i < portArray.Length; ++i)
                    {
                        average += portArray[i];
                    }

                    average = average / portArray.Length;
                }

                ctx.Resolve(ref ports.Output) = average;
            }
        }

    }

}
