namespace SerV112.UtilityAI.Game
{
    public interface IProjectile : IDamage
    {
        void Launch(float force);
    }

}