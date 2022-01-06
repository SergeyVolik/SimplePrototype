using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public class OneShotAudioPool : ObjectPoolSingleton<OneShotAudioPool, AudioSource>
    {
        [SerializeField]
        private float m_TimeToGetBack = 5f;
        protected override AudioSource CreateObject()
        {
            var instance = Instantiate(m_Prefab);
            instance.gameObject.SetActive(false);
            return instance;
        }

        public void PlayClipAtPoint(AudioClip clip, Vector3 postion, float volume = 1f)
        {
            var source = Pool.Get();
            source.clip = clip;
            source.transform.position = postion;
            source.volume = volume;
            source.Play();
        }

        public void PlaySFXWithPosition(SFXSettingsSO evertData, Vector3 vector3)
        {
            var source = Pool.Get();
            source.transform.position = vector3;
            evertData.Play(source);
        }
        protected override void DestroyObject(AudioSource source)
        {
            Destroy(source);
        }

        IEnumerator ReturnToPoolAfterTime(AudioSource source)
        {
            yield return new WaitForSeconds(m_TimeToGetBack);
            Pool.Release(source);
        }

        protected override void ReturnToPool(AudioSource source)
        {
            source.gameObject.SetActive(false);
        }

        protected override void TakeFromPool(AudioSource pistol)
        {
            pistol.gameObject.SetActive(true);
            StartCoroutine(ReturnToPoolAfterTime(pistol));
        }

    }


}
