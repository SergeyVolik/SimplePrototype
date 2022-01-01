using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(HealthDataComponent))]
    public class DamageApplicatorComponent : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private UnityEvent<int> m_OnTakeDamage;


        private IHealthData m_Health;

        void Awake()
        {
            m_Health = GetComponent<IHealthData>();
        }

        public UnityEvent<int> OnTakeDamage => m_OnTakeDamage;
        public void TakeDamage(int damage)
        {
            m_Health.Health -= damage;
            m_OnTakeDamage.Invoke(damage);
        }


    }
}
