using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(IHealthData))]
    public class HitSFX : MonoBehaviour
    {
        IHealthData m_Health;

        [SerializeField]
        AudioClip m_Clip;
        private void Awake()
        {
            m_Health = GetComponent<IHealthData>();
            prevHealth = m_Health.Health;
        }
        private int prevHealth;
        private void OnEnable()
        {
            m_Health.OnHealthChanged.AddListener(Play);
        }

        private void OnDisable()
        {
            m_Health.OnHealthChanged.RemoveListener(Play);
        }

        void Play()
        {
            if (m_Health.Health < prevHealth)
            {
                prevHealth = m_Health.Health;
                Debug.Log("HitSFX");
                AudioSource.PlayClipAtPoint(m_Clip, transform.position);
            }
        }
    }

}
