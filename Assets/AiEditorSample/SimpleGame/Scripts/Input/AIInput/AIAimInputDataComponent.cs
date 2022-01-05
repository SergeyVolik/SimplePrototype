using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public interface IAimAIInpuData : IAimInputData
    {
    }

    public interface IAIAimDirection : IAimDirection
    {
        void UpdateDirection(Vector3 dir);
    }

    [DisallowMultipleComponent]
    public class AIAimInputDataComponent : AbstractAIInputStartEnd, IAIAimDirection, IRotationSpeed, IAimInputData
    {
      
        public Vector3 Direction => m_AimDirection;

        public float RotationSpeed { get => m_RotSpeed; set => m_RotSpeed = value; }
        [SerializeField]
        private float m_RotSpeed = 30;

        [SerializeField]
        private Vector3 m_AimDirection;

        public void UpdateDirection(Vector3 dir)
        {
            m_AimDirection = dir;
        }
    }

}
