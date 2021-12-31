//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Unity.Animation.Hybrid;
//using Unity.DataFlowGraph;
//using Unity.Entities;
//using UnityEngine;

//namespace SV.UtilityAI
//{
    

//    public class UtilityAITest : MonoBehaviour
//    {
       
//        [SerializeField]
//        private AnimationCurve m_HealthCurve;
//        [SerializeField]
//        private float m_HealthStartValue = 100;
//        [SerializeField]
//        private float m_MaxHealthValue = 100;
//        [SerializeField]
//        private float m_DecreaseHealthPerHit = 10;
//        [SerializeField]
//        private AnimationCurve m_HungryCurve;
//        [SerializeField]
//        private float m_HungryStartValue = 0;
//        [SerializeField]
//        private float m_MaxHungryStartValue = 100;
//        [SerializeField]
//        private float m_IncreaseHungryPerSec = 2;


//        bool m_IsDead = false;

//        float m_Time = 0;
      

//        NodeSet m_Set;
//        NodeHandle<CalculateStateNode> m_CalcHealthNode;
//        NodeHandle<CalculateStateNode> m_CalcHungryNode;
//        NodeHandle<SelectBetterStateNode> m_BetterNode;
//        BlobAssetStore m_Store;

//        GraphValue<int> Result;

//        private void OnEnable()
//        {
//            m_Set = new NodeSet();
//            m_Store = new BlobAssetStore();

//            var healthCurve = m_Store.GetAnimationCurve(m_HealthCurve);

//            m_CalcHealthNode = m_Set.Create<CalculateStateNode>();
         
//            m_Set.SetPortArraySize(m_CalcHealthNode, CalculateStateNode.KernelPorts.InputCurves, 1);
//            m_Set.SetPortArraySize(m_CalcHealthNode, CalculateStateNode.KernelPorts.InputCurvesValues, 1);

//            m_Set.SetData(m_CalcHealthNode, CalculateStateNode.KernelPorts.InputCurves, 0, healthCurve);
//            m_Set.SetData(m_CalcHealthNode, CalculateStateNode.KernelPorts.InputCurvesValues, 0, m_HealthStartValue);


//            var hungryCurve = m_Store.GetAnimationCurve(m_HungryCurve);

//            m_CalcHungryNode = m_Set.Create<CalculateStateNode>();

//            m_Set.SetPortArraySize(m_CalcHungryNode, CalculateStateNode.KernelPorts.InputCurves, 1);
//            m_Set.SetPortArraySize(m_CalcHungryNode, CalculateStateNode.KernelPorts.InputCurvesValues, 1);

//            m_Set.SetData(m_CalcHungryNode, CalculateStateNode.KernelPorts.InputCurves, 0, hungryCurve);
//            m_Set.SetData(m_CalcHungryNode, CalculateStateNode.KernelPorts.InputCurvesValues, 0, m_HungryStartValue);

          

//            m_BetterNode = m_Set.Create<SelectBetterStateNode>();

//            m_Set.SetPortArraySize(m_BetterNode, SelectBetterStateNode.KernelPorts.Input, 2);

//            m_Set.Connect(m_CalcHungryNode, CalculateStateNode.KernelPorts.Output, m_BetterNode, SelectBetterStateNode.KernelPorts.Input, 0);
//            m_Set.Connect(m_CalcHungryNode, CalculateStateNode.KernelPorts.Output, m_BetterNode, SelectBetterStateNode.KernelPorts.Input, 1);

//            Result = m_Set.CreateGraphValue(m_BetterNode, SelectBetterStateNode.KernelPorts.Output);

//        }

//        private void Update()
//        {
//            m_Time += Time.deltaTime;

//            if (m_Time > 1)
//            {
//                m_Time = 0;

//                m_HungryStartValue += m_IncreaseHungryPerSec;

//            }


           

//            m_Set.Update();

//            m_Set.SetData(m_CalcHealthNode, CalculateStateNode.KernelPorts.InputCurvesValues, 0, m_HealthStartValue);
//            m_Set.SetData(m_CalcHungryNode, CalculateStateNode.KernelPorts.InputCurvesValues, 0, m_HungryStartValue);

//            Debug.Log($"{m_Set.GetValueBlocking(Result)}");
//        }

//        private void OnDisable()
//        {
//            m_Set.ReleaseGraphValue(Result);
//            m_Set.Destroy(m_CalcHealthNode, m_CalcHungryNode, m_BetterNode);
//            m_Set.Dispose();
//            m_Store.Dispose();
//        }

//    }
//}
