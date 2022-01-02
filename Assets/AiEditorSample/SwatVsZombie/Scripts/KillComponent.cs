using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(DamageApplicatorSystem))]
    [RequireComponent(typeof(HealthDataComponent))]
    public class KillComponent : MonoBehaviour, IKillable
    {
        private IDamageable m_DamageApplicator;
        private IHealthData m_HealthData;

        [SerializeField]
        private UnityEvent m_OnKilled;
        public UnityEvent OnKilled => m_OnKilled;

        // Start is called before the first frame update

        private void Awake()
        {
            m_DamageApplicator = GetComponent<IDamageable>();
            m_HealthData = GetComponent<IHealthData>();
        }
        private void OnEnable()
        {
            m_DamageApplicator.OnTakeDamage.AddListener(CheckToKill);
        }

        private void OnDisable()
        {
            m_DamageApplicator.OnTakeDamage.RemoveListener(CheckToKill);
        }
        private void CheckToKill(int damage)
        {
            if (m_HealthData.Health == 0)
                m_OnKilled.Invoke();
        }
        public void ForceDead()
        {
            Destroy(gameObject);
        }
    }

}