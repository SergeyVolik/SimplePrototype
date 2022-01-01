public interface IGun : IReloadeable, IShootable, IDropable, IEquipable, IGunFamily
{

    int MaxBulletsInGun { get; }
    int CurrentBullets { get; set; }
   
}

public enum GunFamily
{
    Pistol,
    Shotgun,
    Rifle
}