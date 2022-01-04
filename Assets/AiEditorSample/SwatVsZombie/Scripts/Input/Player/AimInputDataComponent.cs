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
    public class AimInputDataComponent : AbstractPressDownInputComponent, IAimInputData, IAimDirection
    {
        public Vector3 Direction => m_AimDir;
        private Camera m_ViewCamera;
        private Vector3 m_AimDir;
        void Awake()
        {
            m_ViewCamera = Camera.main;
        }
        protected override void Update()
        {
            if (Input.GetKeyDown(m_Key))
            {
                m_PressDown = true;

            }
            else if (Input.GetKeyUp(m_Key))
            {
                m_PressDown = false;
            }

            if (m_PressDown)
            {
                var ray = m_ViewCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
                {
                    m_AimDir = Vector3.ProjectOnPlane(hit.point - transform.position, Vector3.up).normalized;
                }
            }
        }
    }

}
