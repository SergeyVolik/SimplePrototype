using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
  
   

    [DisallowMultipleComponent]
    public class PlayerInputDataComponent : MonoBehaviour, IMoveInputData, IAimData
    {
        public float Horizontal => m_Horizontal;

        public float Vertical => m_Vertical;

        public bool IsMove => m_Move;

        public bool Aim => m_Aim;

        [SerializeField]
        private bool m_Aim;
        [SerializeField]
        private bool m_Move;

        [SerializeField]
        private float m_Horizontal;

        [SerializeField]
        private float m_Vertical;

        private void Update()
        {
            m_Horizontal = Input.GetAxisRaw("Horizontal");
            m_Vertical = Input.GetAxisRaw("Vertical");
            m_Move = m_Horizontal != 0 || m_Vertical != 0 ? true : false;
            if (Input.GetMouseButtonDown(1))
            {
                m_Aim = true;

            }
            else if (Input.GetMouseButtonUp(1))
            {
                m_Aim = false;
            }
        }
    }
}
