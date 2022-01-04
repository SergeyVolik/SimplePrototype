using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AIAgentSimpleAI))]
    [RequireComponent(typeof(IEquipGunEvent))]
    public class AIActionMoveToGun : MonoBehaviour
    {

        [SerializeField]
        List<PistolPlaceholder> Pistol;
        NavMeshAgent m_NavAgent;
        AIAgentSimpleAI m_AgentBrain;
        IEquipGunEvent m_EquipEvent;
        private bool m_Move;
        private void Awake()
        {
            m_EquipEvent = GetComponent<IEquipGunEvent>();
            m_AgentBrain = GetComponent<AIAgentSimpleAI>();
            m_NavAgent = GetComponent<NavMeshAgent>();

        }

        private void OnEnable()
        {
            m_EquipEvent.OnEquipGun.AddListener(UpdateHasGunDataTrue);
        }
        private void OnDisable()
        {
            m_EquipEvent.OnEquipGun.RemoveListener(UpdateHasGunDataFalse);
        }
        void UpdateHasGunDataTrue()
        {
            var m_inData = m_AgentBrain.GetInData();
            m_inData.HasGun = 1f;
            m_AgentBrain.ChangeAgentData(m_inData);
        }

        void UpdateHasGunDataFalse()
        {
            var m_inData = m_AgentBrain.GetInData();
            m_inData.HasGun = 0f;
            m_AgentBrain.ChangeAgentData(m_inData);
        }



        void Update()
        {
            var data = m_AgentBrain.GetOutData();


            if (data.SimpleAiActions == SimpleAiActions.SearchGun && !m_Move)
            {

                print(SimpleAiActions.SearchGun);
                m_Move = true;
                m_NavAgent.isStopped = false;
                m_NavAgent.SetDestination(Pistol[Random.Range(0, Pistol.Count)].transform.position);
                
            }
            else if (m_NavAgent.remainingDistance < 0.1f)
            {
                m_Move = false;
            }

        }
    }

}
