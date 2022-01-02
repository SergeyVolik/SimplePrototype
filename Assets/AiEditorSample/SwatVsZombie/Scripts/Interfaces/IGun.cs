using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public interface IGunData
    {
        int MaxBulletsInGun { get; }
        int CurrentBullets { get; set; }
        int GunThrowForce { get; }

        void UpdateData(IGunData data);
    }
    public interface IGun : IReloadeable, IShootable, IDropable, IEquipable/*, IGunFamily,*/ /*IBulletConteiner*/
    {
        IGunData GunData { get; }
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