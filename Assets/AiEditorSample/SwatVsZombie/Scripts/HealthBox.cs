using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(BoxCollider))]
    public class HealthBox : MonoBehaviour
    {
        [SerializeField]
        private int Health = 7;

        private void OnTriggerEnter(Collider other)
        {
            var healer = other.GetComponent<IHealable>();

            if (healer != null)
            {
                var health = other.GetComponent<IHealthData>();
                if (health.MaxHealth != health.Health)
                {
                    healer.Heal(Health);
                    Destroy(gameObject);
                }

            }

         

        }

    }

}
