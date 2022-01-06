using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(IShootSoundEvent))]
    [RequireComponent(typeof(IGunData))]
    public class ShootSFXSystem : PlaySoundComponent<IShootSoundEvent>
    {
     
        IGunData m_GunData;

        protected override void Awake()
        {
            base.Awake();

            m_GunData = GetComponent<IGunData>();
        }

        protected override void PlaySFX()
        {
            if (m_GunData.CurrentBullets > 0)
            {
                PlayAtPos(transform.position);


            }
        }


    }

}
