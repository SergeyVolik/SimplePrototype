using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(IEquipGunEvent))]
    [RequireComponent(typeof(IThrowGunEvent))]
    [RequireComponent(typeof(AIAgentSimpleAI))]
    public class AIActionMoveToAmmo : MonoBehaviour
    {
        IGunData Data;
        IEquipGunEvent m_EquipEvent;
        IThrowGunEvent m_ThrowEvent;
        AIAgentSimpleAI m_AIAgentSimpleAI;
        // Start is called before the first frame update
        void Awake()
        {
            m_EquipEvent = GetComponent<IEquipGunEvent>();
            m_ThrowEvent = GetComponent<IThrowGunEvent>();
            m_AIAgentSimpleAI = GetComponent<AIAgentSimpleAI>();
        }

        private void OnEnable()
        {
            m_EquipEvent.OnEquipGun.AddListener(EquipGun);
            m_ThrowEvent.OnEvent.AddListener(ThrowGun);
        }

       
        private void OnDisable()
        {
            m_EquipEvent.OnEquipGun.RemoveListener(EquipGun);
            m_ThrowEvent.OnEvent.RemoveListener(ThrowGun);
        }

        void EquipGun(IGun gun)
        {
            
        }
        void ThrowGun()
        {

        }


        private void Update()
        {
            if (m_AIAgentSimpleAI.OutData.SimpleAiActions == SimpleAiActions.RunToHealth)
            {

            }
        }


    }

}
