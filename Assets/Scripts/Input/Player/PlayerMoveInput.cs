using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace SerV112.UtilityAI.Game
{  
    [DisallowMultipleComponent]
    public class PlayerMoveInput : MonoBehaviour, IMoveInputData
    {
        [SerializeField]
        private InputReader m_InputReader;

        [SerializeField]
        private bool m_Move;

        [SerializeField]
        private float m_Horizontal;

        [SerializeField]
        private float m_Vertical;

        public float Horizontal => m_Horizontal;

        public float Vertical => m_Vertical;

        public bool IsMove => m_Move;


        private void OnEnable()
        {
            m_InputReader.MoveEvent += M_InputReader_MoveEvent;
        }

        private void OnDisable()
        {
            m_InputReader.MoveEvent -= M_InputReader_MoveEvent;
        }

        private void M_InputReader_MoveEvent(Vector2 arg0)
        {
            m_Horizontal = arg0.x;
            m_Vertical = arg0.y;
            m_Move = m_Horizontal != 0 || m_Vertical != 0 ? true : false;
        }





    }
}
