using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(HandComponent))]
    [RequireComponent(typeof(GunDataComponent))]
    [DisallowMultipleComponent]
    public class EquipWeaponComponent : MonoBehaviour
    {
        HandComponent m_Hand;
        GunDataComponent m_GunData;
        // Start is called before the first frame update
        void Awake()
        {
            m_Hand = GetComponent<HandComponent>();
            m_GunData = GetComponent<GunDataComponent>();
        }

        const string WeaponTag = "Weapon";
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(WeaponTag))
            {
                var ph = other.GetComponentInParent<IGunPlaceholder>();

                switch (ph)
                {
                    case PistolPlaceholder wg:
                        m_GunData.Setup(wg.GunDataComponent);
                        m_Hand.SetPistol(wg);

                        break;
                    default:
                        break;
                }
            }
        }
    }
}