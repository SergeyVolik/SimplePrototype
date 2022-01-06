using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(GunDataComponent))]
    public class Pistol : MonoBehaviour, IPistol
    {
        [SerializeField]
        private int m_MaxBulletsInGun = 30;

        private GunDataComponent m_Data;

        [SerializeField]
        private Transform m_BulletSpawner;

        [SerializeField]
        private BulletPoolSO m_BulletPool;
        public IGunData GunData => m_Data;

        [SerializeField]
        private UnityEvent m_OnShoot;

        public UnityEvent OnEvent => m_OnShoot;


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

        public int Shoot()
        {
            if (m_Data.CurrentBullets > 0)
            {
                var bullet = m_BulletPool.Request();
                bullet.transform.SetPositionAndRotation(m_BulletSpawner.position, m_BulletSpawner.rotation);           
                bullet.Launch(m_Data.GunThrowForce);
                m_Data.CurrentBullets--;

            }

            OnEvent.Invoke();

            return m_Data.CurrentBullets;
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