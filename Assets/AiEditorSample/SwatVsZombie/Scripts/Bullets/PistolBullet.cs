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
        private UnityEvent<IDamageable> m_OnHit;

        public UnityEvent<IDamageable> OnHit => m_OnHit;

        void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        public void Push(int force)
        {
            m_Rigidbody.AddForce(m_Rigidbody.transform.forward * force);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var com = collision.gameObject.GetComponent<IDamageable>();
            if (com != null)
                com.TakeDamage(m_Damage);

            PistolBulletPoolSingleton.Instance.Pool.Release(this);
            m_Rigidbody.velocity = Vector3.zero;
            m_Rigidbody.angularVelocity = Vector3.zero;
 
            OnHit.Invoke(com);
        }

        [SerializeField]
        private int m_Damage = 10;
        public int Damage => m_Damage;
    }
}