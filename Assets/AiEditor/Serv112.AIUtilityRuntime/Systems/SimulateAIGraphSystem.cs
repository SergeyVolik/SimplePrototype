using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine;
using Unity.DataFlowGraph;
using System.Collections.Generic;

namespace SerV112.UtilityAIRuntime
{
   
    //public struct GeneralPurposeComponentA : IComponentData
    //{
    //    public int Lifetime;
    //}

    public struct UtilityAIGraphState : ISystemStateComponentData
    {
        public int State;
    }

    public partial class SimulateAiGraphSystem : SystemBase
    {
        private EntityCommandBufferSystem ecbSource;
        List<NodeSet> m_Sets;
        List<NodeHandle<MyNode>> m_Nodes;
        protected override void OnCreate()
        {
            ecbSource = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();

        }

        protected override void OnUpdate()
        {
            EntityCommandBuffer.ParallelWriter parallelWriterECB = ecbSource.CreateCommandBuffer().AsParallelWriter();

            Entities
                .WithNone<UtilityAIGraphState>()
                .ForEach(
                    (Entity entity, int entityInQueryIndex, in UtilityAIGraph gpA) =>
                    {
                        
                    // Add an ISystemStateComponentData instance
                            parallelWriterECB.AddComponent<UtilityAIGraphState>
                                (
                                    entityInQueryIndex,
                                    entity,
                                    new UtilityAIGraphState() { State = 1 }
                                );
                        Debug.Log("state has been added");
                    })
                .WithoutBurst().ScheduleParallel();

            ecbSource.AddJobHandleForProducer(this.Dependency);

            // Create new command buffer
            parallelWriterECB = ecbSource.CreateCommandBuffer().AsParallelWriter();

            // Entities with both GeneralPurposeComponentA and StateComponentB
            Entities
                .WithAll<UtilityAIGraphState>()
                .ForEach(
                    (Entity entity,
                     int entityInQueryIndex,
                     ref UtilityAIGraph gpA) =>
                    {


                    })
                .WithoutBurst().ScheduleParallel();
            ecbSource.AddJobHandleForProducer(this.Dependency);

            // Create new command buffer
            parallelWriterECB = ecbSource.CreateCommandBuffer().AsParallelWriter();

            // Entities with StateComponentB but not GeneralPurposeComponentA
            Entities
                .WithAll<UtilityAIGraphState>()
                .WithNone<UtilityAIGraph>()
                .ForEach(
                    (Entity entity, int entityInQueryIndex) =>
                    {
                    // This system is responsible for removing any ISystemStateComponentData instances it adds
                    // Otherwise, the entity is never truly destroyed.
                    parallelWriterECB.RemoveComponent<UtilityAIGraphState>(entityInQueryIndex, entity);
                    })
                .ScheduleParallel();
            ecbSource.AddJobHandleForProducer(this.Dependency);

        }

        protected override void OnDestroy()
        {
            //for (int i = 0; i < m_Sets.Count; i++)
            //{
            //    m_Sets[i].Dispose();
            //}
            //m_Sets.Clear();

            //for (int i = 0; i < m_Nodes.Count; i++)
            //{
            //    m_Nodes[i].Dispose();
            //}
            //m_Nodes.Clear();
            
        }
    }

    public class MyNode : SimulationNodeDefinition<MyNode.SimPorts>
    {
        /*
         * The simulation port definition is a struct containing declarations of all of the simulation-type ports
         * that your node supports. This is a part of your node's contract to the outside world.
         * It can also be viewed as the "event"-based blackboard for your node.
         */
        public struct SimPorts : ISimulationPortDefinition
        {
            /*
             * A simulation port definition will contain a range of XYZInput<> and XYZOutput<>.
             * Here we declared two message input of type float.
             * The first generic argument is the enclosing node. This helps in assisting producing compiler errors
             * if you connect something together in the wrong way, or declare message inputs that your node doesn't
             * actually support.
             */
            public MessageInput<MyNode, double> MyFirstInput;
            public MessageInput<MyNode, double> MySecondInput;
        }

        struct MyInstanceData :
            INodeData
            /*
             * For any kind of message that our node can receive, we need to implement the message handler interface
             * for that type. This needs to be done on our nested INodeData struct type.
             */
            , IMsgHandler<double>
        {
            /*
             * Here is our implementation of the message handler for float types. The actual message comes in as a 
             * readonly in parameter (the last argument). The context provides additional information, like which
             * port it arrived on, which is useful if you have multiple port declarations of the same type.
             */
            public void HandleMessage(MessageContext ctx, in double msg)
            {
                if (ctx.Port == SimulationPorts.MyFirstInput)
                {
                    Debug.Log($"{nameof(MyNode)} received a float message of value {msg} on the first input");
                }
                else if (ctx.Port == SimulationPorts.MySecondInput)
                {
                    Debug.Log($"{nameof(MyNode)} received a float message of value {msg} on the second input");
                }
            }
        }
    }
}
