using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(GravitySystem))]
    [RequireComponent(typeof(GravityDataComponent))]
    [RequireComponent(typeof(VelocityYDataComponent))]
    [RequireComponent(typeof(IJumpInputData))]
    public class JumpSystem : MonoBehaviour
    {

      
        private IGravityData m_GravityData;
        private IVelocityY m_YVelocity;
        private GravitySystem m_GravitySystem;
        private IJumpInputData m_JumpInput;
        private bool m_Jump;

        private void Start()
        {
            m_JumpInput = GetComponent<IJumpInputData>();
            m_GravitySystem = GetComponent<GravitySystem>();
            m_GravityData = GetComponent<IGravityData>();
            m_YVelocity = GetComponent<IVelocityY>();
        }

       
        private void Update()
        {

            if (m_JumpInput.PressDown && m_GravitySystem.ground)
            {
                m_Jump = true;
            }

        }
        private void FixedUpdate()
        {

            if (m_Jump)
            { 
                m_Jump = false;
                m_YVelocity.VelocityY += Mathf.Sqrt(m_JumpInput.JumpForce * m_GravityData.Gravity);

            }
        }

    }

}
