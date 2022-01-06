using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    public class PlayerRunInput : AbstractPressDownAndUpInputComponent, IRunInputData {


        [SerializeField]
        protected InputReader m_InputReader;

        public UnityEvent PressUp => m_PressUp;
        [SerializeField]
        protected UnityEvent m_PressUp;

        public UnityEvent PressDown => m_PressDown;
        [SerializeField]
        protected UnityEvent m_PressDown;

        private void OnEnable()
        {
            m_InputReader.RunningStartedEvent += M_InputReader_RunningStartedEvent;
            m_InputReader.RunningCanceledEvent += M_InputReader_RunningCanceledEvent;

        }

        private void OnDisable()
        {
            m_InputReader.RunningStartedEvent -= M_InputReader_RunningStartedEvent;
            m_InputReader.RunningCanceledEvent -= M_InputReader_RunningCanceledEvent;
        }

        private void M_InputReader_RunningCanceledEvent()
        {
            m_PressUp.Invoke();
        }

        private void M_InputReader_RunningStartedEvent()
        {
            PressDown.Invoke();
        }

       
    }

}

