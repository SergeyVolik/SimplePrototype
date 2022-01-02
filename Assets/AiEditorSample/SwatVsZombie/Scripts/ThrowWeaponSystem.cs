using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(HandComponent))]
    public class ThrowWeaponSystem : MonoBehaviour
    {
        private HandComponent m_HandComponent;

        void Awake()
        {
            m_HandComponent = GetComponent<HandComponent>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G) && m_HandComponent.ActiveGun != null)
            {

                m_HandComponent.ThrowWeapon();

            }
        }

    }
}