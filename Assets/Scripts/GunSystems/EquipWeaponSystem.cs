using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{

    public interface IEquipGunEvent
    {
        UnityEvent<IGun> OnEquipGun { get; }
    }

    [RequireComponent(typeof(Player))]
    [DisallowMultipleComponent]
    public class EquipWeaponSystem : MonoBehaviour, IEquipGunEvent
    {

        public UnityEvent<IGun> OnEquipGun => m_OnEquipGun;
        [SerializeField]
        private UnityEvent<IGun> m_OnEquipGun;

        Player Player;
        void Start()
        {
            Player = GetComponent<Player>();
        }

        

        const string WeaponTag = "Weapon";


        private void OnTriggerEnter(Collider other)
        {
            if (this.enabled == true && other.CompareTag(WeaponTag))
            {
               
                if (other.TryGetCompontInParent<IGunPlaceholder>(out var item))
                {
                    
                    if(Player.TryEquipGun(item, out var gun))
                        m_OnEquipGun.Invoke(gun);
                    
                }       
                   
                
            }
        }
    }
}