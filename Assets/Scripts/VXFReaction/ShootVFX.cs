using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerV112.UtilityAI.Game.Channels;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(IGunData))]
    public class ShootVFX : PlayEffectComponent<IShootEffectEvent> {
        IGunData IGunData;

        [SerializeField]
        private Transform MuzzleFlashSpawnPoint;
        [SerializeField]
        protected PositionAndRotationEventChannelSO m_MuzzleFlashChannel;
        protected override void Awake()
        {
            base.Awake();
            IGunData = GetComponent<IGunData>();
        }
        protected override void Play()
        {
            if (IGunData.CurrentBullets != 0)
            {
                m_Channel.RaiseEvent(transform.position, transform.rotation);
                m_MuzzleFlashChannel.RaiseEvent(MuzzleFlashSpawnPoint.position, MuzzleFlashSpawnPoint.rotation);


            }
        }
    }
}
