using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(DamageApplicatorSystem))]
    public class HitSFX : MonoBehaviour
    {
        DamageApplicatorSystem m_DamageApplicatorSystem;

        [SerializeField]
        AudioClip m_Clip;
        private void Awake()
        {
            m_DamageApplicatorSystem = GetComponent<DamageApplicatorSystem>();
        }

        private void OnEnable()
        {
            m_DamageApplicatorSystem.OnTakeDamage.AddListener(Play);
        }

        private void OnDisable()
        {
            m_DamageApplicatorSystem.OnTakeDamage.RemoveListener(Play);
        }

        void Play(int damage)
        {
            Debug.Log("HitSFX");
            AudioSource.PlayClipAtPoint(m_Clip, transform.position);
        }
    }

}
