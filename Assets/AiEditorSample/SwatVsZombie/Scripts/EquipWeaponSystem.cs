using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(HandComponent))]
    [DisallowMultipleComponent]
    public class EquipWeaponSystem : MonoBehaviour
    {
        HandComponent m_Hand;
        // Start is called before the first frame update
        void Awake()
        {
            m_Hand = GetComponent<HandComponent>();
        }

        const string WeaponTag = "Weapon";
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(WeaponTag))
            {
                if (m_Hand.ActiveGun == null)
                {
                    var ph = other.GetComponentInParent<IGunPlaceholder>();

                    switch (ph)
                    {
                        case PistolPlaceholder wg:

                            m_Hand.SetPistol(wg);

                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}