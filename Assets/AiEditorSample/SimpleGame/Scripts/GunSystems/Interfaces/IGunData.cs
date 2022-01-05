

using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    public interface IGunData
    {
        UnityEvent<int> OnCurrentBulletsChanged { get; }
        int MaxBulletsInGun { get; }
        int CurrentBullets { get; set; }
        int GunThrowForce { get; }

        void UpdateData(IGunData data);
    }
}
