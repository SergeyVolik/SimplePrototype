using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using System.Collections;

namespace SerV112.UtilityAI.Base
{
    public interface IUtilityAIJob<InT, OutT> : IJobParallelFor 
        where InT : struct
        where OutT : struct
    {
        
        NativeArray<InT> InAgentDataArray { get; set; }
        NativeArray<OutT> OutAgentDataArray { get; set; }
    }

    public class AISimulationData<InT, OutT, SimDataT> : ScriptableObject where InT : struct where OutT : struct where SimDataT : AISimulationData<InT, OutT, SimDataT>
    {
        public NativeList<InT> InDataArray;
        public NativeList<OutT> OutDataArray;

        public List<AIAgentJobSystemBase<InT, OutT, SimDataT>> m_Agents = new List<AIAgentJobSystemBase<InT, OutT, SimDataT>> ();

        public void AddAgent(AIAgentJobSystemBase<InT, OutT, SimDataT> agent)
        {
            if (InDataArray.IsCreated)
            {
                agent.Index = m_Agents.Count;
                m_Agents.Add(agent);
                InDataArray.Add(agent.InData);
                OutDataArray.Add(agent.OutData);
            }

        }
        public void RemoveAgent(AIAgentJobSystemBase<InT, OutT, SimDataT> agent)
        {
            if (InDataArray.IsCreated)
            {
                m_Agents.Remove(agent);
                InDataArray.RemoveAtSwapBack(agent.Index);
                OutDataArray.RemoveAtSwapBack(agent.Index);


                for (int i = agent.Index; i < m_Agents.Count; i++)
                {
                    m_Agents[i].Index = i;
                    InDataArray[i] = m_Agents[i].InData;
                }

                agent.Index = -1;
            }

        }

        
        private void OnDestroy()
        {
            if (InDataArray.IsCreated)
                InDataArray.Dispose();

            if (OutDataArray.IsCreated)
                OutDataArray.Dispose();
        }
       

        public void UpdateInputData(InT Data, int index)
        {

            InDataArray[index] = Data;
        }


    }

    public class AIAgentJobSystemBase<InT, OutT, SimDataT> : MonoBehaviour
        where InT : struct
        where OutT : struct
        where SimDataT : AISimulationData<InT, OutT, SimDataT>
    {
        public int Index;


        [SerializeField]
        public InT InData;
        [SerializeField]
        public OutT OutData;

        [SerializeField]
        private AISimulationData<InT, OutT, SimDataT> m_Data;

        public void ChangeAgentData(InT data)
        {
            InData = data;
            m_Data.UpdateInputData(data, Index);
        }

        void OnEnable()
        {
            if(m_Data != null)
                m_Data.AddAgent(this);
        }
        void OnDisable()
        {
            if (m_Data != null)
                m_Data.RemoveAgent(this);
        }
#if UNITY_EDITOR


        bool Awaked;
        private void Awake()
        {
            Awaked = true;
        }
        private void OnValidate()
        {
            if (Application.isPlaying && Awaked)
            {
                m_Data.UpdateInputData(InData, Index);
            }
        }

#endif
    }

    public class AISimulationWithJobSystemBase<InT, OutT, JobT, SimDataT> : MonoBehaviour 
        where InT : struct where
        OutT : struct
        where JobT : struct, IUtilityAIJob<InT, OutT>
        where SimDataT : AISimulationData<InT, OutT, SimDataT>
    {
        [SerializeField]
        private AISimulationData<InT, OutT, SimDataT> m_Data;

      
        [SerializeField]
        private int m_GrowhSizeIfOverflow = 10;

        [SerializeField]
        private float m_UpdateRateInSec = 0.1f;

        public static AISimulationWithJobSystemBase<InT, OutT, JobT, SimDataT> Instance { get; private set; }

        Coroutine m_Coroutine;

        private void OnEnable()
        {
            m_Data.InDataArray = new NativeList<InT>(m_GrowhSizeIfOverflow, Allocator.Persistent);
            m_Data.OutDataArray = new NativeList<OutT>(m_GrowhSizeIfOverflow, Allocator.Persistent);

            m_Coroutine = StartCoroutine(UpdateSimulation());
        }

        private void OnDisable()
        {
            m_Data.OutDataArray.Dispose();
            m_Data.InDataArray.Dispose();

            StopCoroutine(m_Coroutine);
        }

        private IEnumerator UpdateSimulation()
        {
            while (true)
            {
                if (m_Data.m_Agents.Count > 0)
                {
                    var job = new JobT();
                    job.InAgentDataArray = m_Data.InDataArray.AsDeferredJobArray();
                    job.OutAgentDataArray = m_Data.OutDataArray.AsDeferredJobArray();
                    job.Run(m_Data.m_Agents.Count);
                    //var handle = job.Schedule(InDataArray.Length, 64);
                    //handle.Complete();

                    for (int i = 0; i < m_Data.m_Agents.Count; i++)
                    {
                        m_Data.m_Agents[i].OutData = m_Data.OutDataArray[i];
                    }



                    print("Update simulation");
                    yield return new WaitForSeconds(m_UpdateRateInSec);
                }
                else yield return null;

              
            }
            
        }

    }
}
