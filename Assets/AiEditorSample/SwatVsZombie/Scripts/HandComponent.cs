using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{

    [DisallowMultipleComponent]
    public class HandComponent : MonoBehaviour
    {
        [SerializeField]
        private Pistol m_Pistol;
        [SerializeField]
        private Shotgun m_Shotgun;
        [SerializeField]
        private Rifle m_Rifle;
        private IGunPlaceholder m_LastGunPlaceholders;
        private IGun m_ActiveGun;

        public IGun ActiveGun => m_ActiveGun;
        public IGunPlaceholder LastGunPlaceholders => m_LastGunPlaceholders;
        public Pistol SetPistol(PistolPlaceholder placeholder)
        {

            m_LastGunPlaceholders = placeholder;
            placeholder.gameObject.SetActive(false);
            
            m_ActiveGun = m_Pistol;
            m_ActiveGun.Equip();
            return m_Pistol;
        }

        public Shotgun SetShotgun()
        {
            m_ActiveGun = m_Shotgun;
            return m_Shotgun;
        }

        public Rifle SetRifle()
        {
            m_ActiveGun = m_Rifle;
            return m_Rifle;
        }

    }
}