using SerV112.UtilityAI.Game.Channels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public abstract class PlaySoundComponent<T> : MonoBehaviour where T : ISoundEvent
    {

        T Event;

        [SerializeField]
        protected SFXSettingsSO m_SFX;

        [SerializeField]
        protected  SFXEventChannelSO m_SfxEvent;
        // Start is called before the first frame update
        protected virtual void Awake()
        {
            Event = GetComponent<T>();
        }
        private void OnEnable()
        {
            Event?.OnEvent.AddListener(PlaySFX);

        }
        private void OnDisable()
        {
            Event?.OnEvent.RemoveListener(PlaySFX);

           
        }

        protected virtual void PlaySFX()
        {
            PlayAtPos(transform.position);
        }

        protected void PlayAtPos(Vector3 pos)
        {
            m_SfxEvent.RaiseEvent(m_SFX, pos);
        }

    }
}
