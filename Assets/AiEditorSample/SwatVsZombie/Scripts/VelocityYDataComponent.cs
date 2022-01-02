using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public class VelocityYDataComponent : MonoBehaviour, IVelocityY
    {
        public float VelocityY { get => m_VelocityY; set => m_VelocityY = value; }

        [SerializeField]
        private float m_VelocityY;
    }

}
