using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(INoTragetBulletEffectEvent))]
    public class WallHitVFX : PlayEffectComponent<INoTragetBulletEffectEvent>
    {
        protected override void Play()
        {
            HitWallParticlePool.Instance.PlayParticleAtPosition(transform.position);
        }

    }

}
