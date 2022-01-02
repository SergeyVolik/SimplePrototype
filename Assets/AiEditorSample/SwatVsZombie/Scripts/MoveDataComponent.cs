using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SerV112.UtilityAI.Game
{
    public class MoveDataComponent : MonoBehaviour, IMoveSettingsData
    {
        [SerializeField]
        private float m_Speed = 6;
        [SerializeField]
        private float m_RotSpeed = 30;
        [SerializeField]
        private float m_RunSpeed = 10;
        public float MoveSpeed { get => m_Speed; set => m_Speed = value; }
        public float RotationSpeed { get => m_RotSpeed; set => m_RotSpeed = value; }
        public float RunSpeed { get => m_RunSpeed; set => m_RunSpeed = value; }
    }

}

