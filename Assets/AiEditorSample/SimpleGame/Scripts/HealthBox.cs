using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(BoxCollider))]
    public class HealthBox : MonoBehaviour, IBusterSelectedSoundEvent
    {
        [SerializeField]
        private int Health = 7;

        [SerializeField]
        private UnityEvent m_OnEvent;
        public UnityEvent OnEvent => m_OnEvent;

        private void OnTriggerEnter(Collider other)
        {
            var healer = other.GetComponent<IHealable>();

            if (healer != null)
            {
                var health = other.GetComponent<IHealthData>();
                if (health.MaxHealth != health.Health)
                {
                    healer.Heal(Health);
                    m_OnEvent.Invoke();
                    Destroy(gameObject);
                }

            }

         

        }

    }

}
