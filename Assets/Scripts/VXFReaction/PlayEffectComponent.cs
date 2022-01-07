using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerV112.UtilityAI.Game.Channels;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(IEffectEvent))]
    public abstract class PlayEffectComponent<T> : MonoBehaviour where T : IEffectEvent
    {
        T m_Effect;

        
        [SerializeField]
        protected PositionAndRotationEventChannelSO m_Channel;
        // Start is called before the first frame update
        protected virtual void Awake()
        {
            m_Effect = GetComponent<T>();
        }

        private void OnEnable()
        {
            m_Effect.OnEvent.AddListener(Play);
        }

        private void OnDisable()
        {
            m_Effect.OnEvent.RemoveListener(Play);
        }
       
        protected virtual void Play() {
            m_Channel.RaiseEvent(transform.position);
        }


    }

}
