using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    public class GunDataComponent : MonoBehaviour, IGunData
    {
        [SerializeField]
        int m_MaxBulletsInGun;
        public int MaxBulletsInGun => m_MaxBulletsInGun;

        [SerializeField]
        int m_CurrentBullets;
        public int CurrentBullets { get => m_CurrentBullets; set  { 
                m_CurrentBullets = value;
                m_OnCurrentBulletsChanged.Invoke(m_CurrentBullets);
            } 
        }

        [SerializeField]
        private int m_GunThrowForce = 1000;
        public int GunThrowForce => m_GunThrowForce;

        public UnityEvent<int> OnCurrentBulletsChanged => m_OnCurrentBulletsChanged;
        [SerializeField]
        private UnityEvent<int> m_OnCurrentBulletsChanged;
        public void UpdateData(IGunData newData)
        {
            m_MaxBulletsInGun = newData.MaxBulletsInGun;
            m_CurrentBullets = newData.CurrentBullets;
            m_GunThrowForce = newData.GunThrowForce;
        }
    }
}
