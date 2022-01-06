using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace SerV112.UtilityAI.Game
{
    public interface IAimDirection
    {
        Vector3 Direction { get; }
    }
    [DisallowMultipleComponent]
    public class PlayerAimInput : AbstractPressDownAndUpInputComponent, IAimInputData, IAimDirection
    {
        public Vector3 Direction => m_AimDir;
        private Camera m_ViewCamera;

        [SerializeField]
        private Vector3 m_AimDir;

        [SerializeField]
        protected InputReader m_InputReader;
        public UnityEvent PressUp => m_PressUp;
        [SerializeField]
        protected UnityEvent m_PressUp;

        public UnityEvent PressDown => m_PressDown;
        [SerializeField]
        protected UnityEvent m_PressDown;

        void Awake()
        {
            m_ViewCamera = Camera.main;
        }
       
        private void OnEnable()
        {
            m_InputReader.AimingStartedEvent += M_InputReader_AimingStartedEvent;
            m_InputReader.AimingCanceledEvent += M_InputReader_AimingCanceledEvent;

        }

        private void M_InputReader_AimingCanceledEvent()
        {
            m_PressUp.Invoke();
        }

        private void M_InputReader_AimingStartedEvent()
        {
            PressDown.Invoke();
        }

        private void OnDisable()
        {
            m_InputReader.AimingStartedEvent -= M_InputReader_AimingStartedEvent;
            m_InputReader.AimingCanceledEvent -= M_InputReader_AimingCanceledEvent;
        }


        void CalcAimDir()
        {
            var ray = m_ViewCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
            {
                m_AimDir = Vector3.ProjectOnPlane(hit.point - transform.position, Vector3.up).normalized;
            }
        }


        void Update()
        {
            CalcAimDir();

        }
    }

}
