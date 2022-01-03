using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public class GunDataComponent : MonoBehaviour, IGunData
    {
        [SerializeField]
        int m_MaxBulletsInGun = 30;
        public int MaxBulletsInGun => m_MaxBulletsInGun;

        [SerializeField]
        int m_CurrentBullets = 30;
        public int CurrentBullets { get => m_CurrentBullets; set => m_CurrentBullets = value; }

        [SerializeField]
        private int m_GunThrowForce = 1000;
        public int GunThrowForce => m_GunThrowForce;

        public void UpdateData(IGunData newData)
        {
            m_MaxBulletsInGun = newData.MaxBulletsInGun;
            m_CurrentBullets = newData.CurrentBullets;
            m_GunThrowForce = newData.GunThrowForce;
        }
    }
}
