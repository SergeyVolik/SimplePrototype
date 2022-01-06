using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public class AnimatedWeapon : MonoBehaviour
    {
        [SerializeField]
        private Animator m_Animator;

        [SerializeField]
        private Collider m_Collider;

        public Animator Animator => m_Animator;
        public Collider Collider => m_Collider;

    }
}
