using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{



    [DisallowMultipleComponent]
    [RequireComponent(typeof(HandData))]
    [RequireComponent(typeof(IThrowInput))]
    public class ThrowWeaponSystem : MonoBehaviour, IThrowGunEvent
    {
        private HandData m_HandComponent;
        IThrowInput input;

        public UnityEvent OnEvent => m_OnThrow;
        [SerializeField]
        private UnityEvent m_OnThrow;
        [SerializeField]
        private float force = 1000;
        void Awake()
        {
            input = GetComponent<IThrowInput>();
            m_HandComponent = GetComponent<HandData>();
        }

        private void OnEnable()
        {
            input.PressDown.AddListener(Throw);
        }

        private void OnDisable()
        {
            input.PressDown.RemoveListener(Throw);
        }

        void Throw()
        {
            if (m_HandComponent.ActiveGun != null)
            {
                m_HandComponent.ThrowWeapon(force);
                m_OnThrow.Invoke();

            }
        }

    }
}