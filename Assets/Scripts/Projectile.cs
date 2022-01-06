using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Projectile : MonoBehaviour, IProjectile
    {
        protected Rigidbody m_Rigidbody;
        public int Damage => m_Damage;

        [SerializeField]
        private int m_Damage = 9999;
        [SerializeField]
        private float m_VelocitySpeedToDamage = 1;
        protected virtual void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }
        public void Launch(float force)
        {
            gameObject.SetActive(true);
            m_Rigidbody.AddForce(m_Rigidbody.transform.forward * force);
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {


            var com = collision.gameObject.GetComponent<IDamageApplicator>();

            if (com != null && m_Rigidbody.velocity.magnitude > m_VelocitySpeedToDamage)
            {

                com.DoDamage(m_Damage);

            }

        }
    }
}
