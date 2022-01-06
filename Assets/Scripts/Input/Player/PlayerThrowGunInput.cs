using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    public class PlayerThrowGunInput : AbstractPressDownInputComponent, IThrowInput
    {
        [SerializeField]
        protected InputReader m_InputReader;

        public UnityEvent PressDown => m_PressDown;
        [SerializeField]
        protected UnityEvent m_PressDown;

        private void OnEnable()
        {
            m_InputReader.ThrowEvent += M_InputReader_ShootingStartedEvent;


        }


        private void M_InputReader_ShootingStartedEvent()
        {
            PressDown.Invoke();
        }

        private void OnDisable()
        {
            m_InputReader.ThrowEvent -= M_InputReader_ShootingStartedEvent;
        }
    }

}
