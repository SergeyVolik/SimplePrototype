using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(MoveDataComponent))]
    [RequireComponent(typeof(Rigidbody))]
    public class AimRotateSystem : MonoBehaviour
    {
        private IRotationSpeed data;
        private IAimData aimData;
        private Rigidbody m_Rigidbody;
        private Vector3 m_AimRotation;     
        private Camera m_ViewCamera;


        private void Awake()
        {
            data = GetComponent<IRotationSpeed>();
            m_Rigidbody = GetComponent<Rigidbody>();
            aimData = GetComponent<IAimData>();
            m_ViewCamera = Camera.main;
        }

        // Update is called once per frame
        private void Update()
        {
            AimRot();
        }

        private void FixedUpdate()
        {
            if (aimData.Aim)
            {
                m_Rigidbody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_AimRotation), Time.fixedDeltaTime * data.RotationSpeed);
            }
        }

        private void AimRot()
        {
            if (aimData.Aim)
            {
                var ray = m_ViewCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
                {
                    m_AimRotation = Vector3.ProjectOnPlane(hit.point - transform.position, Vector3.up).normalized;
                }     
            }
        }
    }

}
