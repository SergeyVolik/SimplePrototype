using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(IEffectEvent))]
    public abstract class PlayEffectComponent<T> : MonoBehaviour where T : IEffectEvent
    {
        T m_Effect;
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

        protected abstract void Play();


    }

}
