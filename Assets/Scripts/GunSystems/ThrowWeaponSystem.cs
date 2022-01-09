using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{



    [DisallowMultipleComponent]
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(IThrowInput))]
    public class ThrowWeaponSystem : MonoBehaviour, IThrowGunEvent
    {

        Player Player;
        IThrowInput input;

        public UnityEvent OnEvent => m_OnThrow;
        [SerializeField]
        private UnityEvent m_OnThrow;
        [SerializeField]
        private float force = 1000;
        void Awake()
        {
            input = GetComponent<IThrowInput>();
            Player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            input.PressDown.AddListener(Throw);
        }

        private void OnDisable()
        {
            input.PressDown.RemoveListener(Throw);
        }

        void Throw()
        {
            var result = Player.ThrowEquipedItems(force);

            if (result)
            {
                m_OnThrow.Invoke();

            }
        }

    }
}