using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PrepareRotation))]
    public class AnimationActivator : MonoBehaviour
    {
        private Rigidbody m_RB;
        private PrepareRotation m_PrepareRot;
        private Collider m_Collider;
        [SerializeField]
        private AnimatedWeapon m_AnimWeapon;
        private void Awake()
        {
            m_Collider = GetComponent<Collider>();
            m_RB = GetComponent<Rigidbody>();
            m_PrepareRot = GetComponent<PrepareRotation>();
        }

        void PrepareFinished()
        {
            m_AnimWeapon.Animator.enabled = true;
            m_AnimWeapon.Collider.isTrigger = true;
            m_Collider.isTrigger = true;
        }
        void OnEnable()
        {
            m_PrepareRot.PrepareFinished.AddListener(PrepareFinished);

            m_RB.useGravity = true;
            m_RB.isKinematic = false;
            m_AnimWeapon.Collider.isTrigger = false;
            m_Collider.isTrigger = false;
            m_AnimWeapon.Animator.enabled = false;
            m_AnimWeapon.Animator.transform.localPosition = Vector3.zero;
            m_AnimWeapon.Animator.transform.localRotation = Quaternion.identity;
            StartCoroutine(CheckRBSpeed());
        }

        private void OnDisable()
        {
            m_PrepareRot.PrepareFinished.RemoveListener(PrepareFinished);
        }

        IEnumerator CheckRBSpeed()
        {
            yield return null;

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