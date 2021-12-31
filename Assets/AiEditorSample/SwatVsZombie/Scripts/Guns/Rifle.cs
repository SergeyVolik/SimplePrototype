using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Rifle : MonoBehaviour, IGun
{
    public IEnumerable<IBullet> Bullets { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public int MaxBulletsInGun => throw new System.NotImplementedException();

    public int CurrentBullets { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Drop()
    {
        throw new System.NotImplementedException();
    }

    public void Equip()
    {
        throw new System.NotImplementedException();
    }

    public void Reload()
    {
        throw new System.NotImplementedException();
    }

    public void Shoot()
    {
        throw new System.NotImplementedException();
    }
}
