using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(IHealthData))]
    public class DamageApplicatorSystem : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private UnityEvent<int> m_OnTakeDamage;

        private IHealthData m_Health;

        private void Awake()
        {
            m_Health = GetComponent<IHealthData>();
        }

        public UnityEvent<int> OnTakeDamage => m_OnTakeDamage;
        public void TakeDamage(int damage)
        {
            m_Health.Health -= damage;
            print("TakeDamage");
            m_OnTakeDamage.Invoke(damage);
        }


    }
}
