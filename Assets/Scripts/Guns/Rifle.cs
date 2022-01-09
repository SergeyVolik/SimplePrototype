using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    public class Rifle : MonoBehaviour, IGun
    {
        public IGunData GunData => throw new System.NotImplementedException();

        public UnityEvent<int> OnEvent => throw new System.NotImplementedException();

        UnityEvent IEvent.OnEvent => throw new System.NotImplementedException();

        public void Drop()
        {
            throw new System.NotImplementedException();
        }

        public void Drop(int force)
        {
            throw new System.NotImplementedException();
        }

        public void Equip()
        {
            throw new System.NotImplementedException();
        }

        public Vector3 GetPosistion()
        {
            throw new System.NotImplementedException();
        }

        public Quaternion GetRotation()
        {
            throw new System.NotImplementedException();
        }

        public int DoAction()
        {
            throw new System.NotImplementedException();
        }

        void IActionable.DoAction()
        {
            throw new System.NotImplementedException();
        }
    }
}