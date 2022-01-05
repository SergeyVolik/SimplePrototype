using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public class AmmoBox : MonoBehaviour
    {
        [SerializeField]
        private int Ammo = 20;

        private void OnTriggerEnter(Collider other)
        {

            var heal = other.GetComponentInChildren<IGunData>();


            if (heal != null)
            {
                heal.CurrentBullets += Ammo;
                
                Destroy(gameObject);
            }

        }
    }

}
