using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{

    public class HealthBoxPool : ObjectPoolMonoBehaviour<HealthBox>
    {
        protected override HealthBox CreateObject()
        {
            return Instantiate(m_Prefab);
        }

        protected override void DestroyObject(HealthBox pistol)
        {
            Destroy(pistol);
        }

        protected override void ReturnToPool(HealthBox pistol)
        {
            pistol.gameObject.SetActive(false);
        }

        protected override void TakeFromPool(HealthBox pistol)
        {
            pistol.gameObject.SetActive(true);
        }
    }

}
