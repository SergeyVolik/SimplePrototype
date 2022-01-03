

using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public interface IGunData
    {
        int MaxBulletsInGun { get; }
        int CurrentBullets { get; set; }
        int GunThrowForce { get; }

        void UpdateData(IGunData data);
    }
}
