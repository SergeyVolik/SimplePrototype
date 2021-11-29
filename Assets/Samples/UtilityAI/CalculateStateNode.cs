//using System.Collections;
//using System.Collections.Generic;
//using Unity.Animation;
//using Unity.Collections;
//using Unity.DataFlowGraph;
//using Unity.Entities;
//using UnityEngine;

//namespace SV.UtilityAI
//{
 
//    public class CalculateStateNode : KernelNodeDefinition<CalculateStateNode.KernelDefs>
//    {

//        public struct KernelDefs : IKernelPortDefinition
//        {
//#pragma warning disable 649  // Assigned through internal DataFlowGraph reflection
//            public PortArray<DataInput<CalculateStateNode, BlobAssetReference<AnimationCurveBlob>>> InputCurves;
//            public PortArray<DataInput<CalculateStateNode, float>> InputCurvesValues;

//            public DataOutput<CalculateStateNode, float> Output;
//#pragma warning restore 649
//        }

//        struct KernelData : IKernelData {
//        }

//        struct GraphKernel : IGraphKernel<KernelData, KernelDefs>
//        {
//            public void Execute(RenderContext ctx, in KernelData data, ref KernelDefs ports)
//            {

//                var portArray = ctx.Resolve(ports.InputCurves);
//                var portArray2 = ctx.Resolve(ports.InputCurvesValues);


//                var result = 1f;
     

//                for (int i = 0; i < portArray.Length; ++i)
//                {
//                    result*= AnimationCurveEvaluator.Evaluate(portArray2[i], portArray[i]);
//                }


//                ctx.Resolve(ref ports.Output) = result;
//            }
//        }
//    }
//}


