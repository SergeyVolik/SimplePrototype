using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(KillSystem))]
    public class DeathSFX : MonoBehaviour
    {
        KillSystem m_KillSystem;

        [SerializeField]
        AudioClip m_Clip;
        private void Awake()
        {
            m_KillSystem = GetComponent<KillSystem>();
        }

        private void OnEnable()
        {
            m_KillSystem.OnKilled.AddListener(Play);
        }

        private void OnDisable()
        {
            m_KillSystem.OnKilled.RemoveListener(Play);
        }

        void Play()
        {
 
            AudioSource.PlayClipAtPoint(m_Clip, transform.position);
        }
    }

}
