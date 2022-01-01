using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(GunDataComponent))]
    public class PistolPlaceholder : MonoBehaviour, IGunPlaceholder
    {
        Rigidbody m_Rigidbody;
        GunDataComponent m_GunDataComponent;

        void Awake()
        {
            m_GunDataComponent = GetComponent<GunDataComponent>();
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        public GunDataComponent GunDataComponent => m_GunDataComponent;

        public void Drop()
        {
            gameObject.SetActive(true);
            m_Rigidbody.AddForce(m_Rigidbody.transform.forward * m_GunDataComponent.GunThrowForce);
        }

        public void SetPositionAndRot(Vector3 post, Quaternion rot)
        {
            transform.SetPositionAndRotation(post, rot);
        }

       

    }

}