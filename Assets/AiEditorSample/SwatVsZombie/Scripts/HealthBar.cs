using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private HealthDataComponent m_Health;

        [SerializeField]
        List<MeshRenderer> HealthCubes;
 
        private void Awake()
        {
            OnHealthChanged();
        }
        private void OnEnable()
        {
            m_Health.OnHealthChanged.AddListener(OnHealthChanged);
        }

        private void OnDisable()
        {
            m_Health.OnHealthChanged.RemoveListener(OnHealthChanged);
        }

        void OnHealthChanged()
        {
            var ration = m_Health.Health / (float)m_Health.MaxHealth;
            var numberOfGreenCubes = HealthCubes.Count * ration;

            for (int i = 0; i < HealthCubes.Count; i++)
            {
                if(numberOfGreenCubes > i)
                    HealthCubes[i].material.color = Color.green;
                else HealthCubes[i].material.color = Color.red;
            }




        }

    }

}
