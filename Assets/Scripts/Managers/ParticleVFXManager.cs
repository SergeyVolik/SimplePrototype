using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerV112.UtilityAI.Game.Channels;

namespace SerV112.UtilityAI.Game.Managers
{
    public class ParticleVFXManager : MonoBehaviour
    {
        [Header("Particle Pools")]
        [SerializeField]
        private ParticlePoolSO m_BodyDamagePool;
        [SerializeField]
        private ParticlePoolSO m_WallHitPool;
        [SerializeField]
        private ParticlePoolSO m_MuzzleFlashPool;
        [SerializeField]
        private ParticlePoolSO m_GunShellPool;
        [SerializeField]
        private ParticlePoolSO m_BleedingPool;
        [SerializeField]
        private ParticlePoolSO m_DeathBloodPool;

        [Header("Listening to")]
        [SerializeField]
        private PositionAndRotationEventChannelSO m_BodyDamagePoolChanel;
        [SerializeField]
        private PositionAndRotationEventChannelSO m_WallHitPoolChanel;
        [SerializeField]
        private PositionAndRotationEventChannelSO m_MuzzleFlashPoolChanel;
        [SerializeField]
        private PositionAndRotationEventChannelSO m_GunShellPoolChanel;
        [SerializeField]
        private PositionAndRotationEventChannelSO m_BleedingPoolChanel;
        [SerializeField]
        private PositionAndRotationEventChannelSO m_DeathBloodPoolChanel;

        private void Awake()
        {
            m_BodyDamagePool.SetParent(transform);
            m_WallHitPool.SetParent(transform);
            m_MuzzleFlashPool.SetParent(transform);
            m_GunShellPool.SetParent(transform);
            m_BleedingPool.SetParent(transform);
            m_DeathBloodPool.SetParent(transform);
        }
        private void OnEnable()
        {
            m_BodyDamagePoolChanel.OnPositionEventRaised += BodyDamageParticles;
            m_WallHitPoolChanel.OnPositionEventRaised += WallHitParticles;
            m_MuzzleFlashPoolChanel.OnPositionEventRaised += MuzzleFlashParticles;
            m_GunShellPoolChanel.OnPositionEventRaised += GunShellParticles;
            m_BleedingPoolChanel.OnPositionEventRaised += BleedingParticles;
            m_DeathBloodPoolChanel.OnPositionEventRaised += DeathBloodParticles;

            m_BodyDamagePoolChanel.OnPosAndRotEventRaised += BodyDamageParticles;
            m_WallHitPoolChanel.OnPosAndRotEventRaised += WallHitParticles;
            m_MuzzleFlashPoolChanel.OnPosAndRotEventRaised += MuzzleFlashParticles;
            m_GunShellPoolChanel.OnPosAndRotEventRaised += GunShellParticles;
            m_BleedingPoolChanel.OnPosAndRotEventRaised += BleedingParticles;
            m_DeathBloodPoolChanel.OnPosAndRotEventRaised += DeathBloodParticles;
        }

        private void OnDisable()
        {
            m_BodyDamagePoolChanel.OnPositionEventRaised -= BodyDamageParticles;
            m_WallHitPoolChanel.OnPositionEventRaised -= WallHitParticles;
            m_MuzzleFlashPoolChanel.OnPositionEventRaised -= MuzzleFlashParticles;
            m_GunShellPoolChanel.OnPositionEventRaised -= GunShellParticles;
            m_BleedingPoolChanel.OnPositionEventRaised -= BleedingParticles;
            m_DeathBloodPoolChanel.OnPositionEventRaised -= DeathBloodParticles;

            m_BodyDamagePoolChanel.OnPosAndRotEventRaised -= BodyDamageParticles;
            m_WallHitPoolChanel.OnPosAndRotEventRaised -= WallHitParticles;
            m_MuzzleFlashPoolChanel.OnPosAndRotEventRaised -= MuzzleFlashParticles;
            m_GunShellPoolChanel.OnPosAndRotEventRaised -= GunShellParticles;
            m_BleedingPoolChanel.OnPosAndRotEventRaised -= BleedingParticles;
            m_DeathBloodPoolChanel.OnPosAndRotEventRaised -= DeathBloodParticles;
        }

        Transform PlayParticlesWithPos(Vector3 pos, ParticlePoolSO pool)
        {
            var particle = pool.Request();
            particle.transform.position = pos;
            StartCoroutine(WaitEnd(particle, pool));
            return particle.transform;
        }

        void PlayParticlesWithPosAndRot(Vector3 pos, Quaternion rot, ParticlePoolSO pool)
        {
            var trans = PlayParticlesWithPos(pos, pool);
            trans.rotation = rot;
        }

        void BodyDamageParticles(Vector3 pos)
        {
            PlayParticlesWithPos(pos, m_BodyDamagePool);
        }

        void BodyDamageParticles(Vector3 pos, Quaternion rot)
        {
            PlayParticlesWithPosAndRot(pos, rot, m_BodyDamagePool);
        }

        void WallHitParticles(Vector3 pos)
        {
            PlayParticlesWithPos(pos, m_WallHitPool);
        }

        void WallHitParticles(Vector3 pos, Quaternion rot)
        {
            PlayParticlesWithPosAndRot(pos, rot, m_WallHitPool);
        }
        void MuzzleFlashParticles(Vector3 pos)
        {
            PlayParticlesWithPos(pos, m_MuzzleFlashPool);
        }

        void MuzzleFlashParticles(Vector3 pos, Quaternion rot)
        {
            PlayParticlesWithPosAndRot(pos, rot, m_MuzzleFlashPool);
        }
        void GunShellParticles(Vector3 pos)
        {
            PlayParticlesWithPos(pos, m_GunShellPool);
        }
        void GunShellParticles(Vector3 pos, Quaternion rot)
        {
            PlayParticlesWithPosAndRot(pos, rot, m_GunShellPool);
        }
        void BleedingParticles(Vector3 pos)
        {
            PlayParticlesWithPos(pos, m_BleedingPool);
        }

        void BleedingParticles(Vector3 pos, Quaternion rot)
        {
            PlayParticlesWithPosAndRot(pos, rot, m_BleedingPool);
        }

        void DeathBloodParticles(Vector3 pos)
        {
            PlayParticlesWithPos(pos, m_DeathBloodPool);
        }

        void DeathBloodParticles(Vector3 pos, Quaternion rot)
        {
            PlayParticlesWithPosAndRot(pos, rot, m_DeathBloodPool);
        }

        IEnumerator WaitEnd(ParticleSystem ps, ParticlePoolSO pool)
        {
            ps.Play();
            yield return new WaitForSeconds(ps.main.duration);
            ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            pool.Return(ps);

        }
    }

}
