using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(DeathSystem))]
    [RequireComponent(typeof(Player))]
    [DisallowMultipleComponent]
    public class DropItemsAfterDeath : MonoBehaviour
    {
        Player Player;
        DeathSystem DeathSystem;
        // Start is called before the first frame update
        void Awake()
        {
            DeathSystem = GetComponent<DeathSystem>();
            Player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            DeathSystem.OnEvent.AddListener(OnDeath);
        }

        private void OnDisable()
        {
            DeathSystem.OnEvent.RemoveListener(OnDeath);
        }

        void OnDeath()
        {

            Player.Drop();
        }
    }

}

