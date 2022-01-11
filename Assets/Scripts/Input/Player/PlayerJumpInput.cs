using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace SerV112.UtilityAI.Game
{

    [DisallowMultipleComponent]
    public class PlayerJumpInput : AbstractPressDownInputComponent, IJumpInputData
    {
      
        protected InputReader m_InputReader;
        public UnityEvent PressUp => m_PressUp;
        [SerializeField]
        protected UnityEvent m_PressUp;

        public UnityEvent PressDown => m_PressDown;
        [SerializeField]
        protected UnityEvent m_PressDown;

        public float JumpForce => m_JumpForce;

        [SerializeField]
        private float m_JumpForce = 10;

        [Inject]
        void Construct(InputReader input)
        {
            m_InputReader = input;
        }

        private void OnEnable()
        {
            m_InputReader.JumpEvent += M_InputReader_AimingStartedEvent;

        }

        private void M_InputReader_AimingStartedEvent()
        {
            PressDown.Invoke();
        }

        private void OnDisable()
        {
            m_InputReader.JumpEvent -= M_InputReader_AimingStartedEvent;
        }

    }

}
