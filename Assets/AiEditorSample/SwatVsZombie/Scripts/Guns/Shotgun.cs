using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace SerV112.UtilityAI.Game
{
    public class Shotgun : MonoBehaviour, IGun
    {
        public IEnumerable<IBullet> Bullets { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public int MaxBulletsInGun => throw new System.NotImplementedException();

        public int CurrentBullets { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }


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