using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(IHealthData))]
    [RequireComponent(typeof(AIAgentSimpleAI))]
    [RequireComponent(typeof(HandComponent))]
    [RequireComponent(typeof(FieldOfViewSystem))]
    public class UtilityAiAgetDataUpdater : MonoBehaviour
    {
        IHealthData m_Health;
        HandComponent m_Hand;
        AIAgentSimpleAI Agent;
        FieldOfViewSystem m_FieldOfViewSystem;
        AISimulationSimpleAI.AgentInData m_inData;
        // Start is called before the first frame update
        void Awake()
        {
            m_FieldOfViewSystem = GetComponent<FieldOfViewSystem>();
            m_Hand = GetComponent<HandComponent>();
            Agent = GetComponent<AIAgentSimpleAI>();
            m_Health = GetComponent<IHealthData>();

            m_inData.Helath = m_Health.Health;

        }

        private void OnEnable()
        {
            m_Health.OnHealthChanged.AddListener(UpdateHealth);
        }

        private void OnDisable()
        {
            m_Health.OnHealthChanged.RemoveListener(UpdateHealth);
        }
        void UpdateHealth()
        {
            m_inData.Helath = m_Health.Health;
        }

        private void Update()
        {

        }

    }

}
