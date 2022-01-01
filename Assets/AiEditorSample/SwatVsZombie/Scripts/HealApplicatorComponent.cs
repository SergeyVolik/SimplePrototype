using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(HealthDataComponent))]
    public class HealApplicatorComponent : MonoBehaviour, IHealable
    {
        private IHealthData m_Health;

        [SerializeField]
        private UnityEvent<int> m_OnHeal;
        public UnityEvent<int> OnHeal => m_OnHeal;

        void Awake()
        {
            m_Health = GetComponent<IHealthData>();
        }

        public void Heal(int value)
        {
            var newHealth = m_Health.Health + value;

            if (newHealth > m_Health.MaxHealth)
            {
                value = value - newHealth - m_Health.MaxHealth;
                m_Health.Health = m_Health.MaxHealth;
            }

            if (value > 0)
                m_OnHeal.Invoke(value);

        }
    }
}