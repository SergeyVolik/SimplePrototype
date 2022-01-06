namespace SerV112.UtilityAI.Game
{
    public interface IBoxOfAmmo
    {
        IBullet Type { get; set; }
        int NumberOfBullets { get; set; }
    }
}