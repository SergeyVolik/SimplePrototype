using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SerV112.UtilityAI.Game
{
    public class PistolBulletPoolSingleton : ObjectPoolSingleton<PistolBullet>
    {

        protected override PistolBullet CreateObject()
        {
            var bullet = Instantiate(m_Prefab);
            bullet.gameObject.SetActive(false);
            return bullet;
        }

        protected override void DestroyObject(PistolBullet Obj)
        {
            Destroy(Obj.gameObject);
        }

        protected override void TakeFromPool(PistolBullet Obj)
        {
            Obj.gameObject.SetActive(true);
        }

        protected override void ReturnToPool(PistolBullet Obj)
        {
            Obj.gameObject.SetActive(false);
        }


    }
}