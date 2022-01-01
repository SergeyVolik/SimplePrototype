using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(HandComponent))]
    public class ThrowWeaponComponent : MonoBehaviour
    {
        private HandComponent m_HandComponent;
        // Start is called before the first frame update
        void Awake()
        {
            m_HandComponent = GetComponent<HandComponent>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G) && m_HandComponent.ActiveGun != null)
            {
                m_HandComponent.ActiveGun.Drop();
                m_HandComponent.LastGunPlaceholders.SetPositionAndRot(m_HandComponent.ActiveGun.GetPosistion(), m_HandComponent.ActiveGun.GetRotation());
                m_HandComponent.LastGunPlaceholders.Drop();

            }
        }

    }
}