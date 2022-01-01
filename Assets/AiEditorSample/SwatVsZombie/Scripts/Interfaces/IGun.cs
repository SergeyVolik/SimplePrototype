using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public interface GunData
    {
        int MaxBulletsInGun { get; }
        int CurrentBullets { get; set; }
    }
    public interface IGun : IReloadeable, IShootable, IDropable, IEquipable, GunData/*, IGunFamily,*/ /*IBulletConteiner*/
    {

        Vector3 GetPosistion();
        Quaternion GetRotation();
    }

    public interface IPistol : IGun
    {

    }

    public interface IRifle : IGun
    {

    }

    public interface IShotgun : IGun
    {

    }
}