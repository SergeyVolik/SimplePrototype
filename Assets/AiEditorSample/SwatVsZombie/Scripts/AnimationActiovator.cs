using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PrepareRotation))]
    public class AnimationActiovator : MonoBehaviour
    {
        private Rigidbody m_RB;
        private PrepareRotation m_PrepareRot;
        [SerializeField]
        private AnimatedWeapon m_AnimWeapon;
        private void Awake()
        {
            m_RB = GetComponent<Rigidbody>();
            m_PrepareRot = GetComponent<PrepareRotation>();
            m_PrepareRot.PrepareFinished.AddListener(() =>
            {
                m_AnimWeapon.Animator.enabled = true;
                m_AnimWeapon.Collider.isTrigger = true;
            });

            StartCoroutine(CheckRBSpeed());
        }

        IEnumerator CheckRBSpeed()
        {
            while (!m_RB.IsSleeping())
            {
                yield return null;
            }

            m_RB.useGravity = false;
            m_RB.isKinematic = true;

            m_PrepareRot.Prepare();

        }


    }
}