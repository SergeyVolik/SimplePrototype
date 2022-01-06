using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public class AudioSettingsSO : DescriptionBaseSO
    {
        [SerializeField]
        protected AudioClip[] SFX;
        public virtual void Play(AudioSource source)
        {
            source.clip = SFX[Random.Range(0, SFX.Length)];
            source.Play();
        }
    }
}
