using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(GunDataComponent))]
    public class Pistol : MonoBehaviour, IPistol
    {
        [SerializeField]
        private int m_MaxBulletsInGun = 30;

        [SerializeField]
        GunDataComponent m_Data;

        [SerializeField]
        Transform m_BulletSpawner;

        public IGunData GunData => m_Data; 

        void Awake()
        {
            m_Data = GetComponent<GunDataComponent>();
        }
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
            if (m_Data.CurrentBullets > 0)
            {
                var bullet = PistolBulletPoolSingleton.Instance.Pool.Get();
                bullet.transform.SetPositionAndRotation(m_BulletSpawner.position, m_BulletSpawner.rotation);
                bullet.Push(m_Data.GunThrowForce);
                m_Data.CurrentBullets--;
            }
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