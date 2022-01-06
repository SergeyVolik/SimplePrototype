using System;
using UnityEngine.Audio;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{

    // https://frarees.github.io/default-gist-license
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class MinMaxSliderAttribute : PropertyAttribute
    {
        public float Min { get; set; }
        public float Max { get; set; }
        public bool DataFields { get; set; } = true;
        public bool FlexibleFields { get; set; } = true;
        public bool Bound { get; set; } = true;
        public bool Round { get; set; } = true;

        public MinMaxSliderAttribute() : this(0, 1)
        {
        }

        public MinMaxSliderAttribute(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }

    


    [CreateAssetMenu(menuName = "Sound/SFXEvent")]
    public class SFXSettingsSO : AudioSettingsSO
    {

        [SerializeField]
        private AudioMixerGroup audioOutput;

        [SerializeField]
        [MinMaxSlider(0f, 1f)]
        private Vector2 volume;

        [SerializeField]
        [MinMaxSlider(0f, 1f)]
        private Vector2 pitch;

        public override void Play(AudioSource source)
        {
            if (SFX.Length == 0)
                return;

            source.clip = SFX[UnityEngine.Random.Range(0, SFX.Length)];
            source.volume = UnityEngine.Random.Range(volume.x, volume.y);
            source.pitch = UnityEngine.Random.Range(pitch.x, pitch.y);
            source.outputAudioMixerGroup = audioOutput;
            source.Play();
        }
    }

}

