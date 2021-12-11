//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Unity.Entities;
//using UnityEngine;

//namespace SerV112.UtilityAIRuntime
//{
//    [UpdateInGroup(typeof(SimulationSystemGroup))]
//    public class TestSystem : SystemBase
//    {
//        protected override void OnUpdate()
//        {
//            Entities.ForEach((in UtilityAIGraph ai) =>
//            {
//                Debug.Log(ai.Ref.Value.Value);
//            }).WithoutBurst().Run();
//        }
//    }
//}
