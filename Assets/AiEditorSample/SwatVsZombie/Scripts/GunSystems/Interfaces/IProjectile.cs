namespace SerV112.UtilityAI.Game
{
    public interface IProjectile : IDamage
    {
        void Push(int force);
    }

}