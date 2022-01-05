using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
  
   

    [DisallowMultipleComponent]
    public class PlayerInputDataComponent : MonoBehaviour, IMoveInputData
    {
        public float Horizontal => m_Horizontal;

        public float Vertical => m_Vertical;

        public bool IsMove => m_Move;


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

        }
    }
}
