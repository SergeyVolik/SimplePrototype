using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SerV112.UtilityAI.Game
{

    public class PistolPoolSingleton : ObjectPoolSingleton<Pistol>
    {

        protected override Pistol CreateObject()
        {
            Debug.Log("create pistol");
            var pistol = Instantiate(m_Prefab);
            pistol.gameObject.SetActive(false);
            return pistol;
        }

        protected override void DestroyObject(Pistol pistol)
        {
            Debug.Log("destroy pistol");
            Destroy(pistol.gameObject);
        }

        protected override void TakeFromPool(Pistol pistol)
        {
            Debug.Log("Take from pool");
        }

        protected override void ReturnToPool(Pistol pistol)
        {
            Debug.Log("return to pool");
        }


    }
}