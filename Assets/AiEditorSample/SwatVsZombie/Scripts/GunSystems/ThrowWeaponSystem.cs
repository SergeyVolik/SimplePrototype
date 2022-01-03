using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(HandComponent))]
    [RequireComponent(typeof(IThrowInput))]
    public class ThrowWeaponSystem : MonoBehaviour
    {
        private HandComponent m_HandComponent;
        IThrowInput input;
        void Awake()
        {
            input = GetComponent<IThrowInput>();
            m_HandComponent = GetComponent<HandComponent>();
        }

        private void Update()
        {
            if (input.PressDown && m_HandComponent.ActiveGun != null)
            {
                m_HandComponent.ThrowWeapon();

            }
        }

    }
}