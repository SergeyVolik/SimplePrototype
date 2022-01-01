using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBulletPoolSingleton : ObjectPoolSingleton<PistolBullet>
{

    protected override PistolBullet CreateObject()
    {
        var bullet = Instantiate(m_Prefab);
        bullet.gameObject.SetActive(false);
        return bullet;
    }

    protected override void DestroyObject(PistolBullet pistol)
    {
        Destroy(pistol.gameObject);
    }

    protected override void TakeFromPool(PistolBullet pistol)
    {

    }

    protected override void ReturnToPool(PistolBullet pistol)
    {

    }


}
