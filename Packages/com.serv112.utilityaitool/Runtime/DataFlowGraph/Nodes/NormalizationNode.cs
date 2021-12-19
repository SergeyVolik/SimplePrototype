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
   

    public class NormalizationNode : KernelNodeDefinition<NormalizationNode.KernelDefs>
    {
        struct KernelData : IKernelData { }

        public struct KernelDefs : IKernelPortDefinition
        {
            public DataInput<NormalizationNode, float> Max;
            public DataInput<NormalizationNode, float> Min;
            public DataInput<NormalizationNode, float> Input;

            public DataOutput<NormalizationNode, float> Output;
        }


        [BurstCompile]
        struct GraphKernel : IGraphKernel<KernelData, KernelDefs>
        {
            public void Execute(RenderContext ctx, in KernelData data, ref KernelDefs ports)
            {

                ctx.Resolve(ref ports.Output) = MathEx.normalizeclamp01(ctx.Resolve(ports.Input), ctx.Resolve(ports.Min), ctx.Resolve(ports.Max));
            }
        }
    }
}
