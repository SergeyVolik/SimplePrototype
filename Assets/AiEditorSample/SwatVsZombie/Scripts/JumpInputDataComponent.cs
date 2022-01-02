using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public class JumpInputDataComponent : MonoBehaviour, IJumpInputData
    {
        public bool NeedJump => m_NeedJump;

        public float JumpForce => m_JumpForce;

        [SerializeField]
        private float m_JumpForce = 10;

        [SerializeField]
        private bool m_NeedJump;

        [SerializeField]
        private KeyCode Key = KeyCode.Space;
        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(Key))
                m_NeedJump = true;
        }

        void LateUpdate()
        {
            m_NeedJump = false;
        }
    }

}
