using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(IEffectEvent))]
    public abstract class PlayEffectComponent<T> : MonoBehaviour where T : IEffectEvent
    {
        T m_Effect;
        [SerializeField]
        protected ParticlePoolSO m_Pool;
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
        private IEnumerator DoParticleBehaviourWithPos(ParticleSystem particle, ParticlePoolSO pool)
        {
           
            particle.Play();
            yield return new WaitForSeconds(particle.main.duration);
            particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            pool.Return(particle);
        }

        protected void PlayWithPos(ParticleSystem particle, Vector3 pos, ParticlePoolSO pool)
        {
            particle.transform.position = pos;
            StartCoroutine(DoParticleBehaviourWithPos(particle, pool));

        }
        protected void PlayWithPosAndRot(ParticleSystem particle, Vector3 pos, Quaternion rot, ParticlePoolSO pool)
        {
            particle.transform.rotation = rot;
            PlayWithPos(particle, pos, pool);
        }

        protected virtual void Play() {
            PlayWithPos(m_Pool.Request(), transform.position, m_Pool);
        }


    }

}
