using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(DeathSystem))]
    [RequireComponent(typeof(HandData))]
    [DisallowMultipleComponent]
    public class DropItemsAfterDeath : MonoBehaviour
    {
        HandData HandComponent;
        DeathSystem DeathSystem;
        // Start is called before the first frame update
        void Awake()
        {
            DeathSystem = GetComponent<DeathSystem>();
            HandComponent = GetComponent<HandData>();
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
            
            HandComponent.Drop();
        }
    }

}

