//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Unity.Burst;
//using Unity.Collections;
//using Unity.DataFlowGraph;

//namespace SerV112.UtilityAIRuntime
//{


//    public class AIProcessorNode : KernelNodeDefinition<AIProcessorNode.KernelDefs>
//    {
//        public struct AIProcessorOutput
//        {

//        }

//        public struct KernelDefs : IKernelPortDefinition
//        {
//            public PortArray<DataInput<AIProcessorNode, int>> Input;
//            public DataOutput<AIProcessorNode, Buffer<int>> Output;
//        }

//        struct KernelData : IKernelData { }

//        [BurstCompile]
//        struct GraphKernel : IGraphKernel<KernelData, KernelDefs>
//        {
//            public void Execute(RenderContext ctx, in KernelData data, ref KernelDefs ports)
//            {
//                var input = ctx.Resolve(ports.Input);


//                var output = ctx.Resolve(ref ports.Output);

//                for (int i = 0; i < output.Length; i++)
//                {
//                    output[i] = i;
//                }
//                //for (int i = 0; i < input.Length; i++)
//                //{
//                //    array[i] = input[i];
//                //}
//            }
//        }

//    }
//}
