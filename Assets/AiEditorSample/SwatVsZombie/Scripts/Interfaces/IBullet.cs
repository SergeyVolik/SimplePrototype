namespace SerV112.UtilityAI.Game
{
    public interface IBullet
    {
        GunFamily Type { get; }
        int Damage { get; }
    }
}