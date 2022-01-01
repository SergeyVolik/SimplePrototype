namespace SerV112.UtilityAI.Game
{
    public interface IGun : IReloadeable, IShootable, IDropable, IEquipable,/* IGunFamily,*/ IBulletConteiner
    {

        int MaxBulletsInGun { get; }
        int CurrentBullets { get; set; }

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
    public enum GunFamily
    {
        Pistol,
        Shotgun,
        Rifle
    }
}