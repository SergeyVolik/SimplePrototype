using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    public class GravityDataComponent : MonoBehaviour, IGravityData
    {

        public float Gravity { get => m_Gravity; set => m_Gravity = value; }

        [SerializeField]
        private float m_Gravity = -9.81f;

    }

}
