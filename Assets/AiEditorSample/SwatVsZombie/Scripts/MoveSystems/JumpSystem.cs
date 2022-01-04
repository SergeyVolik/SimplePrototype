using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(IGravityData))]
    [RequireComponent(typeof(IJumpInputData))]
    public class JumpSystem : MonoBehaviour
    {

      
        private IGravityData m_GravityData;
        private IVelocityY m_YVelocity;
        private GravitySystem m_GravitySystem;
        private IJumpInputData m_JumpInput;


        private void Awake()
        {
            m_JumpInput = GetComponent<IJumpInputData>();
            m_GravitySystem = GetComponent<GravitySystem>();
            m_GravityData = GetComponent<IGravityData>();
            m_YVelocity = GetComponent<IVelocityY>();
        }

        private void OnEnable()
        {
            m_JumpInput.PressDown.AddListener(Jump);
        }

        private void OnDisable()
        {
            m_JumpInput.PressDown.RemoveListener(Jump);
        }

        void Jump()
        {
            if (m_GravitySystem.ground)
            {
                m_YVelocity.VelocityY += Mathf.Sqrt(m_JumpInput.JumpForce * m_GravityData.Gravity);
            }
        }


    }

}
