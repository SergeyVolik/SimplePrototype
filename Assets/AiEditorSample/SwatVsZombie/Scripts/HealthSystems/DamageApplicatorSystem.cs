using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(IHealthData))]
    public class DamageApplicatorSystem : MonoBehaviour, IDamageApplicator
    {

        //[SerializeField]
        //private float m_InvulnerabilityTime = 0.1f;
        private IHealthData m_Health;

        private void Awake()
        {
            m_Health = GetComponent<IHealthData>();
            lastElapsedTime = Time.time;
        }

        float lastElapsedTime;
        public void DoDamage(int damage)
        {
            //if (Time.time - lastElapsedTime > m_InvulnerabilityTime)
            //{
                //lastElapsedTime = Time.time;
                m_Health.Health -= damage;
                m_Health.OnHealthChanged.Invoke();
            //}
        }

        


    }
}
