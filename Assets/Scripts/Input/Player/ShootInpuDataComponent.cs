using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    public class ShootInpuDataComponent : AbstractPressDownInputComponent, IShootInpuData
    {
        protected InputReader m_InputReader;
        public UnityEvent PressUp => m_PressUp;
        [SerializeField]
        protected UnityEvent m_PressUp;

        public UnityEvent PressDown => m_PressDown;
        [SerializeField]
        protected UnityEvent m_PressDown;

        [Inject]
        void Construct(InputReader input)
        {
            m_InputReader = input;
        }

        private void OnEnable()
        {
            m_InputReader.ShootingStartedEvent += M_InputReader_ShootingStartedEvent;
            m_InputReader.ShootingCanceledEvent += M_InputReader_ShootingCanceledEvent;

        }

        private void M_InputReader_ShootingCanceledEvent()
        {
            m_PressUp.Invoke();
        }

        private void M_InputReader_ShootingStartedEvent()
        {
            PressDown.Invoke();
        }

        private void OnDisable()
        {
            m_InputReader.ShootingStartedEvent -= M_InputReader_ShootingStartedEvent;
            m_InputReader.ShootingCanceledEvent -= M_InputReader_ShootingCanceledEvent;
        }

    }

}
