using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public interface IAimAIInpuData : IAimInputData
    {
        void UpdateInput(bool value);
    }

    public interface IAIAimDirection : IAimDirection
    {
        void UpdateDirection(Vector3 dir);
    }

    [DisallowMultipleComponent]
    public class AIAimInputDataComponent : MonoBehaviour, IAimAIInpuData, IAIAimDirection, IRotationSpeed
    {
        public bool PressDown => m_Value;

        public Vector3 Direction => m_Dir;

        public float RotationSpeed { get => m_RotSpeed; set => m_RotSpeed = value; }
        [SerializeField]
        private float m_RotSpeed = 30;

        [SerializeField]
        private Vector3 m_Dir;
        public void UpdateInput(bool value)
        {
            m_Value = value;
        }

        public void UpdateDirection(Vector3 dir)
        {
            m_Dir = dir;
        }

        [SerializeField]
        private bool m_Value;
    }

}
