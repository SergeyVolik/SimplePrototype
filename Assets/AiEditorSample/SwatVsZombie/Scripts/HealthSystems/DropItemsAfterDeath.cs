using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(DeathSystem))]
    [RequireComponent(typeof(HandComponent))]
    
    public class DropItemsAfterDeath : MonoBehaviour
    {
        HandComponent HandComponent;
        DeathSystem DeathSystem;
        // Start is called before the first frame update
        void Awake()
        {
            DeathSystem = GetComponent<DeathSystem>();
            HandComponent = GetComponent<HandComponent>();
        }

        private void OnEnable()
        {
            DeathSystem.OnDeadth.AddListener(OnDeath);
        }

        private void OnDisable()
        {
            DeathSystem.OnDeadth.RemoveListener(OnDeath);
        }

        void OnDeath()
        {
            
            HandComponent.Drop();
        }
    }

}

