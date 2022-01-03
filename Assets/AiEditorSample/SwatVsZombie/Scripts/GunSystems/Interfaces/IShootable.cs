using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    public interface IShootable
    {
        void Shoot();


    }

    public interface IShootEvent
    {
        UnityEvent OnShoot { get; }
    }
}