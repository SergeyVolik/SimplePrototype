namespace SerV112.UtilityAI.Game
{
    public interface IDamage
    {
        int Damage { get; }
    }
    public interface IBullet : IDamage
    {
        void Push(int force);
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