using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{



    [DisallowMultipleComponent]   
    public class HandHolder : MonoBehaviour
    {
        [SerializeField]
        private Pistol m_Pistol;

        private IGunPlaceholder m_LastGunPlaceholders;
        private IGun m_ActiveGun;
        public bool IsFree => m_ActiveGun == null;
        public IGun ActiveGun => m_ActiveGun;
        public IGunPlaceholder LastGunPlaceholders => m_LastGunPlaceholders;
        public Pistol SetPistol(PistolPlaceholder placeholder)
        {

            m_LastGunPlaceholders = placeholder;
            
            placeholder.gameObject.SetActive(false);

            m_ActiveGun = m_Pistol;
            m_ActiveGun.Equip();
            m_Pistol.GunData.UpdateData(placeholder.Data);
           
          
            return m_Pistol;
        }


        private void UpdateGunProjectileDataAndDrop()
        {
            LastGunPlaceholders.Data.UpdateData(ActiveGun.GunData);
            LastGunPlaceholders.SetPositionAndRot(ActiveGun.GetPosistion(), ActiveGun.GetRotation());
        }
        public void Drop()
        {
            if (ActiveGun != null)
            {
                ActiveGun.Drop();
                LastGunPlaceholders.Drop();
                UpdateGunProjectileDataAndDrop();
                m_ActiveGun = null;
                m_LastGunPlaceholders = null;
            }
        }
        public void ThrowWeapon(float force)
        {
            if (ActiveGun != null)
            {
                UpdateGunProjectileDataAndDrop();
                ActiveGun.Drop();           
                LastGunPlaceholders.Launch(force);
                m_ActiveGun = null;
                m_LastGunPlaceholders = null;
            }
        }

    }
}