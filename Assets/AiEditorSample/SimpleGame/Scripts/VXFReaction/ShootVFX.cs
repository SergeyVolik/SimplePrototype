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
        [SerializeField]
        protected ParticlePoolSO m_MuzzleFlash;
        protected override void Awake()
        {
            base.Awake();
            IGunData = GetComponent<IGunData>();
        }
        protected override void Play()
        {
            if (IGunData.CurrentBullets != 0)
            {

                PlayWithPos(m_MuzzleFlash.Request(), MuzzleFlashSpawnPoint.position, m_MuzzleFlash);
                PlayWithPosAndRot(m_Pool.Request(), transform.position, transform.rotation, m_Pool);


            }
        }
    }
}
