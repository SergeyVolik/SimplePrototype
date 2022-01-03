using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(IAudioClip))]
    [RequireComponent(typeof(IShootEvent))]
    public class ShootSFXSystem : MonoBehaviour
    {
        IAudioClip m_AudioClip;
        IShootEvent m_Event;
        // Start is called before the first frame update
        void Awake()
        {
            m_Event = GetComponent<IShootEvent>();
            m_AudioClip = GetComponent<IAudioClip>();
           
        }

        private void Play()
        {
            AudioSource.PlayClipAtPoint(m_AudioClip.Clip, transform.position);
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
