using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        void Awake()
        {
            m_ViewCamera = Camera.main;
        }



        void CalcAimDir()
        {
            var ray = m_ViewCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
            {
                m_AimDir = Vector3.ProjectOnPlane(hit.point - transform.position, Vector3.up).normalized;
            }
        }


        protected override void Update()
        {
            base.Update();

          
            CalcAimDir();

        }
    }

}
