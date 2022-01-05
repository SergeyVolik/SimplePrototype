using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(Rigidbody))]
    public class PistolBullet : MonoBehaviour, IBullet
    {
        Rigidbody m_Rigidbody;
        [SerializeField]
        private UnityEvent<IDamageApplicator> m_OnHit;

        public UnityEvent<IDamageApplicator> OnHit => m_OnHit;

        void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        public void Launch(float force)
        {
            m_Rigidbody.AddForce(m_Rigidbody.transform.forward * force);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var com = collision.gameObject.GetComponent<IDamageApplicator>();
            if (com != null)
            {
                com.DoDamage(m_Damage);
                HitBloodParticlePool.Instance.PlayParticleAtPosition(transform.position);

            }
            else {
                HitWallParticlePool.Instance.PlayParticleAtPosition(transform.position);
            }
            PistolBulletPoolSingleton.Instance.Pool.Release(this);
           
            OnHit.Invoke(com);
        }

        [SerializeField]
        private int m_Damage = 10;
        public int Damage => m_Damage;
    }
}