using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{


    [DisallowMultipleComponent]
    public class AmmoBox : MonoBehaviour, IBusterSelectedSoundEvent
    {
        [SerializeField]
        private int Ammo = 20;

        public UnityEvent OnEvent => m_OnEvent;

        [SerializeField]
        private UnityEvent m_OnEvent;
        private void OnTriggerEnter(Collider other)
        {

            var gundata = other.GetComponentInChildren<IGunData>();


            if (gundata != null)
            {
                gundata.CurrentBullets += Ammo;

                OnEvent.Invoke();
                Destroy(gameObject);
            }

        }
    }

}
