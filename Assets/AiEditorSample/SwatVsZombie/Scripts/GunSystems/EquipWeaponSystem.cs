using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{

    public interface IEquipGunEvent
    {
        UnityEvent OnEquipGun { get; }
    }

    [RequireComponent(typeof(HandComponent))]
    [DisallowMultipleComponent]
    public class EquipWeaponSystem : MonoBehaviour, IEquipGunEvent
    {
        HandComponent m_Hand;

        void Start()
        {
            m_Hand = GetComponent<HandComponent>();
        }

        

        const string WeaponTag = "Weapon";

        public UnityEvent OnEquipGun => m_OnEquipGun;
        [SerializeField]
        private UnityEvent m_OnEquipGun;
        private void OnTriggerEnter(Collider other)
        {
            if (this.enabled == true && other.CompareTag(WeaponTag))
            {
                if (m_Hand.ActiveGun == null)
                {
                    var ph = other.GetComponentInParent<IGunPlaceholder>();

                    switch (ph)
                    {
                        case PistolPlaceholder wg:

                            m_Hand.SetPistol(wg);
                            m_OnEquipGun.Invoke();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}