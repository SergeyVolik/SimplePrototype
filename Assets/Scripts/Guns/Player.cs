using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    public class Player : MonoBehaviour, IActionable
    {

        [SerializeField]
        private HandHolder m_RightHand;

        public HandHolder RightHand => m_RightHand;

        [SerializeField]
        private HandHolder m_LeftHand;
        public HandHolder LeftHand => m_LeftHand;

        public int CurrentBullets;
        public void DoAction()
        {
            if (!RightHand.IsFree)
                RightHand.ActiveGun.DoAction();

            if (!LeftHand.IsFree)
                LeftHand.ActiveGun.DoAction();
        }

        public bool ThrowEquipedItems(float force)
        {
            bool result = false;
            if (!RightHand.IsFree)
            {
                RightHand.ThrowWeapon(force);
                result = true;
            }
            if (!LeftHand.IsFree)
            {
                LeftHand.ThrowWeapon(force);
                result = true;
            }
            return result;
        }

        public void Drop()
        {
            if (!RightHand.IsFree)
                RightHand.Drop();

            if (!LeftHand.IsFree)
                LeftHand.Drop();

        }
        public bool TryEquipGun(IGunPlaceholder item, out IGun gun)
        {
            gun = null;
            switch (item)
            {
                case PistolPlaceholder pl:
                    if (RightHand.IsFree)
                    {
                        gun = RightHand.SetPistol(pl);
                        return true;
                    }

                    if (LeftHand.IsFree)
                    {
                        gun = LeftHand.SetPistol(pl);
                        return true;
                    }
                    break;
                default:
                    break;


            }
           
            return false;

        }




    }
}
