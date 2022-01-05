using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{

    public class AmmoBoxPool : ObjectPoolMonoBehaviour<AmmoBox>
    {
        protected override AmmoBox CreateObject()
        {
            return Instantiate(m_Prefab);
            m_Prefab.gameObject.SetActive(false);
        }

        protected override void DestroyObject(AmmoBox pistol)
        {
            Destroy(pistol);
        }

        protected override void ReturnToPool(AmmoBox pistol)
        {
            pistol.gameObject.SetActive(false);
        }

        protected override void TakeFromPool(AmmoBox pistol)
        {
            pistol.gameObject.SetActive(true);
        }
    }
}

