using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    public interface IShootable
    {
        int Shoot();


    }

    public interface IShootEvent
    {
        UnityEvent<int> OnShoot { get; }
    }
}