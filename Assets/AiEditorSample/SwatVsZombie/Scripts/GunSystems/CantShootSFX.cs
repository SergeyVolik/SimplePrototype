using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(IShootEvent))]
    public class CantShootSFX : MonoBehaviour
    {
        [SerializeField]
        AudioClip m_AudioClip;
        IShootEvent m_Event;
        // Start is called before the first frame update
        void Awake()
        {
            m_Event = GetComponent<IShootEvent>();
           
        }

        private void Play(int ammo)
        {
            if(ammo == 0)
                OneShotAudioPool.Instance.PlayClipAtPoint(m_AudioClip, transform.position);
        }
        private void OnEnable()
        {
            m_Event.OnShoot.AddListener(Play);
        }

        private void OnDisable()
        {
            m_Event.OnShoot.RemoveListener(Play);
        }

    }

}
