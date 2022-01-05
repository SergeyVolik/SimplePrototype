using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    public class HealthDataComponent : MonoBehaviour, IHealthData
    {
        [SerializeField]
        private int m_Health = 100;
        [SerializeField]
        private int m_MaxHealth = 100;

        public int Health { get => m_Health; set => m_Health = value; }
        public int MaxHealth { get => m_MaxHealth; set => m_MaxHealth = value; }

        public UnityEvent OnEvent => m_OnHealthChanged;


        [SerializeField]
        private UnityEvent m_OnHealthChanged;
    }
}
