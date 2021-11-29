//using System.Collections;
//using System.Collections.Generic;
//using Unity.Animation;
//using Unity.DataFlowGraph;
//using Unity.Entities;
//using UnityEngine;
//using System.Linq;

//namespace SV.UtilityAI
//{

//    public class SelectBetterStateNode : KernelNodeDefinition<SelectBetterStateNode.KernelDefs>
//    {


//        public struct KernelDefs : IKernelPortDefinition
//        {

//            public PortArray<DataInput<SelectBetterStateNode, float>> Input;
//            public DataOutput<SelectBetterStateNode, int> Output;

//        }

//        struct KernelData : IKernelData {
            
//        }

//        struct GraphKernel : IGraphKernel<KernelData, KernelDefs>
//        {
//            public void Execute(RenderContext ctx, in KernelData data, ref KernelDefs ports)
//            {

//                var portArray = ctx.Resolve(ports.Input);

//                float max = float.MinValue;
//                int outIndex = -1;

//                ref var output = ref ctx.Resolve(ref ports.Output);

//                for (int i = 0; i < portArray.Length; ++i)
//                {
//                    var value = portArray[i];

//                    if (max < value)
//                    {
//                        max = value;
//                        outIndex = i;
//                    }
//                }


//                ctx.Resolve(ref ports.Output) = outIndex;
//            }
//        }

//    }
//}


