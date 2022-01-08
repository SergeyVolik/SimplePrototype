using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AIAgentSimpleAI))]
    [RequireComponent(typeof(IEquipGunEvent))]
    [RequireComponent(typeof(IThrowGunEvent))]
    public class AIActionMoveToGun : MonoBehaviour
    {

        [SerializeField]
        List<PistolPlaceholder> Pistol;
        NavMeshAgent m_NavAgent;
        AIAgentSimpleAI m_AgentBrain;
        IThrowGunEvent m_Event;
        IEquipGunEvent m_EquipEvent;

        private bool m_IsMoving;
        private void Awake()
        {
            m_Event = GetComponent<IThrowGunEvent>();
            m_EquipEvent = GetComponent<IEquipGunEvent>();
            m_AgentBrain = GetComponent<AIAgentSimpleAI>();
            m_NavAgent = GetComponent<NavMeshAgent>();

           
        }
        private void Start()
        {
            StartCoroutine(UpdateWithDelay(0.5f));
        }


        private void OnEnable()
        {
            m_Event.OnEvent.AddListener(UpdateHasGunDataFalse);
            m_EquipEvent.OnEquipGun.AddListener(UpdateHasGunDataTrue);
        }
        private void OnDisable()
        {
            m_Event.OnEvent.RemoveListener(UpdateHasGunDataFalse);
            m_EquipEvent.OnEquipGun.RemoveListener(UpdateHasGunDataTrue);
        }
        void UpdateHasGunDataTrue(IGun gundata)
        {

            var m_inData = m_AgentBrain.InData;
            m_inData.HasGun = 1f;
            m_inData.Ammo = gundata.GunData.CurrentBullets;
            m_AgentBrain.ChangeAgentData(m_inData);
        }

        void UpdateHasGunDataFalse()
        {
            //DOTO FIX
            m_IsMoving = false;
            var m_inData = m_AgentBrain.InData;
            m_inData.HasGun = 0f;
            m_AgentBrain.ChangeAgentData(m_inData);
        }


        PistolPlaceholder GetClosest(List<PistolPlaceholder> pistols)
        {
            float max = float.MaxValue;
            int index = 0;
            for (int i = 0; i < pistols.Count; i++)
            {
                var dist = Vector3.Distance(transform.position, pistols[i].transform.position);

                if (dist < max)
                {
                    max = dist;
                    index = i;
                }
            }

            return pistols[index];
        }


        PistolPlaceholder tragetGun;
        IEnumerator UpdateWithDelay(float delay)
        {
            while (true)
            {
                //DOTO FIX
                var data = m_AgentBrain.OutData;


                if (data.SimpleAiActions == SimpleAiActions.SearchGun)
                {
                    if (!m_IsMoving)
                    {

                        m_IsMoving = true;
                        m_NavAgent.isStopped = false;
                        m_NavAgent.ResetPath();
                        var pistols = Pistol.Where(e => e.gameObject.activeSelf == true).ToList();

                        pistols.Remove(tragetGun);
                        if (pistols.Count != 0)
                        {

                            tragetGun = GetClosest(pistols);
                            m_NavAgent.SetDestination(tragetGun.transform.position);
                        }

                    }

                    else if (tragetGun.gameObject.activeSelf)
                    {



                        m_NavAgent.ResetPath();
                        var result = m_NavAgent.SetDestination(tragetGun.transform.position);
                        if (!result)
                            m_IsMoving = false;


                    }
                    else
                    {

                        tragetGun = null;
                        m_IsMoving = false;
                    }


                }

                yield return new WaitForSeconds(delay);
            }
           

          


        }
    }

}
