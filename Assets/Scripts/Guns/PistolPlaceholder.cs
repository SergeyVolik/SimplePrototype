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
    public class PistolPlaceholder : Projectile, IGunPlaceholder, IItem
    {
        GunDataComponent m_GunDataComponent;

        protected override void Awake()
        {
            base.Awake();
            m_GunDataComponent = GetComponent<GunDataComponent>();
        }
        

        public IGunData Data => m_GunDataComponent;


        public void Drop()
        {
            gameObject.SetActive(true);
        }

        
        public void SetPositionAndRot(Vector3 post, Quaternion rot)
        {
            transform.SetPositionAndRotation(post, rot);
        }

    }

}