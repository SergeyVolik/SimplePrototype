using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(IGunData))]
    public class ShootVFX : PlayEffectComponent<IShootEffectEvent> {
        IGunData IGunData;

        [SerializeField]
        private Transform MuzzleFlashSpawnPoint;
        protected override void Awake()
        {
            base.Awake();
            IGunData = GetComponent<IGunData>();
        }
        protected override void Play()
        {
            if (IGunData.CurrentBullets != 0)
            {
                DropShellParticlePool.Instance.PlayParticleWithPositionAndRotation(transform.position, transform.rotation);
                MuzzleFlashParticlePool.Instance.PlayParticleAtPosition(MuzzleFlashSpawnPoint.position);
            }
        }
    }
}
