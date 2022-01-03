using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public class SoundComponent : MonoBehaviour, IAudioClip
    {
        public AudioClip Clip => m_Clip;

        [SerializeField]
        private AudioClip m_Clip;

    }

}
