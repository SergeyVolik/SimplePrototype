using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Zenject;

namespace SerV112.UtilityAI.Game
{
    public interface IAimDirection
    {
        Vector3 Direction { get; }
    }
    [RequireComponent(typeof(FieldOfViewSystem))]
    [DisallowMultipleComponent]
    public class PlayerAimInput : AbstractPressDownAndUpInputComponent, IAimInputData, IAimDirection, IUpdate
    {
        public Vector3 Direction => m_AimDir;
        private Camera m_ViewCamera;

        [SerializeField]
        private Vector3 m_AimDir;


        protected InputReader m_InputReader;

        [Inject]
        void Construct(InputReader input)
        {
            m_InputReader = input;
        }
        public UnityEvent PressUp => m_PressUp;
        [SerializeField]
        protected UnityEvent m_PressUp;

        public UnityEvent PressDown => m_PressDown;
        [SerializeField]
        protected UnityEvent m_PressDown;

        FieldOfViewSystem FieldOfViewSystem;

        void Awake()
        {
            FieldOfViewSystem = GetComponent<FieldOfViewSystem>();
            m_ViewCamera = Camera.main;
        }
       
        private void OnEnable()
        {
            SubscribeToUpdate(this);
            m_InputReader.AimingStartedEvent += M_InputReader_AimingStartedEvent;
            m_InputReader.AimingCanceledEvent += M_InputReader_AimingCanceledEvent;
            FieldOfViewSystem.OnTargetDetected.AddListener(TargetDetected);
            FieldOfViewSystem.OnTargetLost.AddListener(TargetLost);

        }

        private void OnDisable()
        {
            UnsubscribeFromUpdate(this);
            m_InputReader.AimingStartedEvent -= M_InputReader_AimingStartedEvent;
            m_InputReader.AimingCanceledEvent -= M_InputReader_AimingCanceledEvent;
            FieldOfViewSystem.OnTargetDetected.RemoveListener(TargetDetected);
            FieldOfViewSystem.OnTargetLost.RemoveListener(TargetLost);
        }
        Transform enemy;
        private void TargetDetected(Transform trans)
        {
            print("Detected");
            enemy = trans;
            PressDown.Invoke();
        }

        private void TargetLost(Transform trans)
        {
            print("Lost");
            enemy = null;

            
            m_PressUp.Invoke();
        }
        private void M_InputReader_AimingCanceledEvent()
        {
            m_PressUp.Invoke();
        }

        private void M_InputReader_AimingStartedEvent()
        {
            PressDown.Invoke();
        }



      
        void CalcAimDir()
        {
            if (enemy == null)
            {
                var ray = m_ViewCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
                {
                    m_AimDir = Vector3.ProjectOnPlane(hit.point - transform.position, Vector3.up).normalized;
                }
            }
            else {

                m_AimDir = Vector3.ProjectOnPlane(enemy.position - transform.position, Vector3.up).normalized;

            }
        }



        public void OnUpdate()
        {
            CalcAimDir();
        }
    }

}
