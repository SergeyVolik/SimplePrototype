using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(AIAgentSimpleAI))]
    [RequireComponent(typeof(NavMeshAgent))]
    [DisallowMultipleComponent]
    public class AIPatrol : MonoBehaviour
    {
        [SerializeField]
        List<Transform> m_PatrolPoints = new List<Transform>();
        NavMeshAgent m_Agent;
        AIAgentSimpleAI m_AgentBrain;
        private int m_CurrentTargetIndex = 0;

        private void Awake()
        {
            m_Agent = GetComponent<NavMeshAgent>();
            m_AgentBrain = GetComponent<AIAgentSimpleAI>();
            m_CurrentTargetIndex = Random.Range(0, m_PatrolPoints.Count);
        }

        bool Move = false;
        // Update is called once per frame
        void Update()
        {
            //DOTO FIX
            var data = m_AgentBrain.OutData;

            if (m_Agent.remainingDistance < 0.5f)
            {
                Move = false;

            }
            if (data.SimpleAiActions == SimpleAiActions.Patrol)
            {


                if (!Move)
                {
                    m_Agent.SetDestination(m_PatrolPoints[m_CurrentTargetIndex].position);
                    Move = true;
                    m_CurrentTargetIndex++;
                    m_CurrentTargetIndex = (m_CurrentTargetIndex) % (m_PatrolPoints.Count - 1);
                }

            }
            else if (data.SimpleAiActions != SimpleAiActions.Patrol && Move)
            {
                m_Agent.ResetPath();
            }
        }


    }

}
