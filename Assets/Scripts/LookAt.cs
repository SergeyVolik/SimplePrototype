using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public class LookAt : UpdatableMonoBehaviour, IUpdate
    {
        [SerializeField]
        private Transform Target;

        public void OnUpdate()
        {
            transform.LookAt(Target);
        }

        private void OnEnable()
        {
            SubscribeToUpdate(this);
        }

        private void OnDisable()
        {
            UnsubscribeFromUpdate(this);
        }
    }

}
