using SerV112.UtilityAI.Game.Channels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game.Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSourcePoolSO m_Pool;

        [SerializeField]
        private SFXEventChannelSO m_SfxEvent;

        [SerializeField]
        private int PrewarmPoolSize = 10;
        private void Awake()
        {
            m_Pool.SetParent(transform);
            m_Pool.Prewarm(PrewarmPoolSize);

        }

        private void OnEnable()
        {
            m_SfxEvent.OnLoadingRequested += SFXRequeted;
        }

        private void OnDisable()
        {
            m_SfxEvent.OnLoadingRequested -= SFXRequeted;
        }

        void SFXRequeted(SFXSettingsSO settings, Vector3 position)
        {
            var source = m_Pool.Request();
            source.transform.position = position;
            settings.Play(source);
            StartCoroutine(WaintEnd(source));
        }

        IEnumerator WaintEnd(AudioSource source)
        {
            yield return new WaitWhile(() => source.isPlaying);
            m_Pool.Return(source);
        }
    }

}
