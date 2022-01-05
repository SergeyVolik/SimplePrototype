using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{

    public interface IThrowItemEvent
    {
        UnityEvent OnThrow { get; }
    }
    [DisallowMultipleComponent]
    [RequireComponent(typeof(HandComponent))]
    [RequireComponent(typeof(IThrowInput))]
    public class ThrowWeaponSystem : MonoBehaviour, IThrowItemEvent
    {
        private HandComponent m_HandComponent;
        IThrowInput input;

        public UnityEvent OnThrow => m_OnThrow;
        [SerializeField]
        private UnityEvent m_OnThrow;
        [SerializeField]
        private float force = 1000;
        void Awake()
        {
            input = GetComponent<IThrowInput>();
            m_HandComponent = GetComponent<HandComponent>();
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