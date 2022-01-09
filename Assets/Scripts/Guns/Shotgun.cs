using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    public class Shotgun : MonoBehaviour, IGun
    {
        public IEnumerable<IBullet> Bullets { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public int MaxBulletsInGun => throw new System.NotImplementedException();

        public int CurrentBullets { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public IGunData GunData => throw new System.NotImplementedException();

        public UnityEvent<int> OnEvent => throw new System.NotImplementedException();

        UnityEvent IEvent.OnEvent => throw new System.NotImplementedException();

        public void DoAction()
        {
            throw new System.NotImplementedException();
        }

        public void Drop()
        {
            gameObject.SetActive(false);
        }

        public void Equip()
        {
            gameObject.SetActive(true);
        }

        public Vector3 GetPosistion()
        {
            throw new System.NotImplementedException();
        }

        public Quaternion GetRotation()
        {
            throw new System.NotImplementedException();
        }

        public void Reload()
        {
            throw new System.NotImplementedException();
        }

        public void Shoot()
        {
            throw new System.NotImplementedException();
        }

        
    }
}