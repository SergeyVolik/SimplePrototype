namespace SerV112.UtilityAI.Game
{
    public interface IDamage
    {
        int Damage { get; }
    }
    public interface IBullet : IDamage
    {

    }

    public interface IPistolBullet : IBullet
    {
        
    }

    public interface IShotgunBullet : IBullet
    {

    }

    public interface IRifleBullet : IBullet
    {

    }
}