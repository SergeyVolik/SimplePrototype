using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(IHealthData))]
    public class DeathSystem : MonoBehaviour, IKillable
    {
        private IHealthData m_HealthData;

        [SerializeField]
        private UnityEvent m_OnDeath;
        public UnityEvent OnEvent => m_OnDeath;

        // Start is called before the first frame update

        private void Awake()
        {
            m_HealthData = GetComponent<IHealthData>();
        }
        private void OnEnable()
        {
            m_HealthData.OnEvent.AddListener(CheckToKill);
        }

        private void OnDisable()
        {
            m_HealthData.OnEvent.RemoveListener(CheckToKill);
        }
        private void CheckToKill()
        {
            if (m_HealthData.Health <= 0)
            {
                m_OnDeath.Invoke();
                ForceDead();
            }
        }
        public void ForceDead()
        {
            Destroy(gameObject);
        }
    }

}