using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{

    public class HitWallParticlePool : ObjectPoolSingleton<HitWallParticlePool, ParticleSystem>
    {
        [SerializeField]
        private float m_TimeToGetBack = 5f;
        protected override ParticleSystem CreateObject()
        {
            var instance = Instantiate(m_Prefab);
            instance.gameObject.SetActive(false);
            return instance;
        }

        protected override void DestroyObject(ParticleSystem pistol)
        {
            Destroy(pistol);
        }
        public void PlayParticleAtPosition(Vector3 Postion)
        {
            var blood = Pool.Get();
            blood.transform.position = Postion;
            blood.Play();
        }
        IEnumerator ReturnToPoolAfterTime(ParticleSystem particle)
        {
            yield return new WaitForSeconds(m_TimeToGetBack);
            Pool.Release(particle);
        }

        protected override void ReturnToPool(ParticleSystem pistol)
        {
            pistol.gameObject.SetActive(false);
        }

        protected override void TakeFromPool(ParticleSystem pistol)
        {
            pistol.gameObject.SetActive(true);
            StartCoroutine(ReturnToPoolAfterTime(pistol));
        }
    }

}
