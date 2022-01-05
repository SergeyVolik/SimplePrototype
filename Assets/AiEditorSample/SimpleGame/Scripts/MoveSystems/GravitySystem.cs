using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CharacterController))]
    public class GravitySystem : MonoBehaviour, IVelocityY, IGravityData
    {

        public float VelocityY { get => m_VelocityY; set => m_VelocityY = value; }

        [SerializeField]
        private float m_VelocityY;

        public float Gravity { get => m_Gravity; set => m_Gravity = value; }

        [SerializeField]
        private float m_Gravity = 9.81f;

        CharacterController controller;
        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        Vector3 playerVelocity;
        public bool ground;
        void FixedUpdate()
        {
          
            //Debug.Log(groundedPlayer);
            if (ground && m_VelocityY < 0)
            {
                m_VelocityY = 0f;
            }


            m_VelocityY += -Gravity * Time.fixedDeltaTime;
            controller.Move(new Vector3(0, m_VelocityY, 0) * Time.fixedDeltaTime);
            ground = controller.isGrounded;

        }
    }

}
