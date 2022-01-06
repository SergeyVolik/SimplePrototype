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
        protected AudioSourcePoolSO m_Pool;
        // Start is called before the first frame update
        protected virtual void Awake()
        {
            Event = GetComponent<T>();
        }
        private void OnEnable()
        {
            Event?.OnEvent.AddListener(PlaySFX);

            var toDelete = new List<AudioSource>();
            for (int i = 0; i < WaitSources.Count; i++)
            {
                if (!WaitSources[i].isPlaying)
                {
                    toDelete.Add(WaitSources[i]);
                    m_Pool.Return(WaitSources[i]);
                }

            }

            for (int i = 0; i < toDelete.Count; i++)
            {
                WaitSources.Remove(toDelete[i]);
            }
        }
        private void OnDisable()
        {
            Event?.OnEvent.RemoveListener(PlaySFX);

           
        }

        static readonly List<AudioSource> WaitSources = new List<AudioSource>();
        protected virtual void PlaySFX()
        {
            PlayAtPos(transform.position);
        }

        protected void PlayAtPos(Vector3 pos)
        {
            var source = m_Pool.Request();
            m_SFX.Play(source);
            source.transform.position = pos;
            StartCoroutine(WaitEnd(source));
        }

        IEnumerator WaitEnd(AudioSource source)
        {
            WaitSources.Add(source);
            yield return new WaitForSeconds(2f);//new WaitWhile(() => source.isPlaying);
           
            m_Pool.Return(source);
            WaitSources.Remove(source);
        }
    }
}
