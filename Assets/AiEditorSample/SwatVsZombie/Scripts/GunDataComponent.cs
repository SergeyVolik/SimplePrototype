using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public class GunDataComponent : MonoBehaviour
    {
        [SerializeField]
        int m_MaxBulletsInGun = 30;
        public int MaxBulletsInGun => m_MaxBulletsInGun;

        [SerializeField]
        int m_CurrentBullets = 30;
        public int CurrentBullets { get => m_CurrentBullets; set => m_CurrentBullets = value; }

        [SerializeField]
        private float m_GunThrowForce = 1000;
        public float GunThrowForce => m_GunThrowForce;
        public void Setup(GunDataComponent newData)
        {
            m_MaxBulletsInGun = newData.m_MaxBulletsInGun;
            m_CurrentBullets = newData.m_CurrentBullets;
            m_GunThrowForce = newData.m_GunThrowForce;
        }
    }
}
