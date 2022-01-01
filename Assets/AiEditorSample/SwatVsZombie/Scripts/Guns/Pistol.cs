using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SerV112.UtilityAI.Game
{
    public class Pistol : MonoBehaviour, IPistol
    {
        [SerializeField]
        private int m_MaxBulletsInGun = 30;
        public int MaxBulletsInGun => m_MaxBulletsInGun;

        public int m_CurrentBullets;
        public int CurrentBullets { get => m_CurrentBullets; set => m_CurrentBullets = value; }

        public void Drop()
        {
            gameObject.SetActive(false);
        }

        public void Equip()
        {
            gameObject.SetActive(true);
        }

        public void Reload()
        {
            throw new System.NotImplementedException();
        }

        public void Shoot()
        {
            throw new System.NotImplementedException();
        }

        public Vector3 GetPosistion()
        {
            return transform.position;
        }

        public Quaternion GetRotation()
        {
            return transform.rotation;
        }
    }
}