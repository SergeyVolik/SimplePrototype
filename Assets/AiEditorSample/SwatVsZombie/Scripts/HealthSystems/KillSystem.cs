using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(IHealthData))]
    public class KillSystem : MonoBehaviour, IKillable
    {
        private IHealthData m_HealthData;

        [SerializeField]
        private UnityEvent m_OnKilled;
        public UnityEvent OnKilled => m_OnKilled;

        // Start is called before the first frame update

        private void Awake()
        {
            m_HealthData = GetComponent<IHealthData>();
        }
        private void OnEnable()
        {
            m_HealthData.OnHealthChanged.AddListener(CheckToKill);
        }

        private void OnDisable()
        {
            m_HealthData.OnHealthChanged.RemoveListener(CheckToKill);
        }
        private void CheckToKill()
        {
            if (m_HealthData.Health <= 0)
            {
                print("Kill");
                m_OnKilled.Invoke();
                ForceDead();
            }
        }
        public void ForceDead()
        {
            Destroy(gameObject);
        }
    }

}