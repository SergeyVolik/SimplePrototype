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

        private IHealthData m_Health;

        private void Awake()
        {
            m_Health = GetComponent<IHealthData>();
        }

        public void TakeDamage(int damage)
        {
            m_Health.Health -= damage;
            print("TakeDamage");
            m_Health.OnHealthChanged.Invoke();
        }


    }
}
