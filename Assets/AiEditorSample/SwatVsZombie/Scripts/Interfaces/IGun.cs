using System.Collections.Generic;

public interface IGun
{
    IEnumerable<IBullet> Bullets { get; set; }

    int MaxBulletsInGun { get; }
    int CurrentBullets { get; set; }
    void Shoot();
    void Reload();
    void Drop();
    void Equip();
}

public enum GunFamily
{
    Pistol,
    Shotgun,
    Rifle
}