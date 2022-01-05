using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(DeathSystem))]
    public class DeathSFX : MonoBehaviour
    {
        DeathSystem m_KillSystem;

        [SerializeField]
        AudioClip m_Clip;
        private void Awake()
        {
            m_KillSystem = GetComponent<DeathSystem>();
        }

        private void OnEnable()
        {
            m_KillSystem.OnDeadth.AddListener(Play);
        }

        private void OnDisable()
        {
            m_KillSystem.OnDeadth.RemoveListener(Play);
        }

        void Play()
        {
            OneShotAudioPool.Instance.PlayClipAtPoint(m_Clip, transform.position);
        }
    }

}
