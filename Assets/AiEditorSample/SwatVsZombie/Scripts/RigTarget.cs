using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace SerV112.UtilityAI.Game
{
    public class RigTarget : MonoBehaviour
    {
        [SerializeReference]
        List<MultiAimConstraint> Rigs = new List<MultiAimConstraint>();
        [SerializeField]
        Rig m_AimRig;

        [SerializeField]
        private float m_WeightTo01Duration = 1f;

        [SerializeField]
        RigBuilder buider;
        private Coroutine m_LastCoroutime;
        private void Start()
        {
            m_AimRig.weight = 0;
        }
        public void SetTarget(Transform target)
        {
            for (int i = 0; i < Rigs.Count; i++)
            {

                var data = Rigs[i].data.sourceObjects;
                data.SetTransform(0, target);
                Rigs[i].data.sourceObjects = data;
            }
            m_AimRig.weight = 1;
            //Set1Weight();
            buider.Build();
        }
        public void ClearTarget()
        {
            Set0Weight();
        }

        private void Set1Weight()
        {
            if (m_LastCoroutime != null)
                StopCoroutine(m_LastCoroutime);
            m_LastCoroutime = StartCoroutine(SetWeight1());
        }

        private void Set0Weight()
        {
            if (m_LastCoroutime != null)
                StopCoroutine(m_LastCoroutime);
            m_LastCoroutime = StartCoroutine(SetWeight0());
        }

        IEnumerator SetWeight1()
        {
            float time = 0;
            while (m_WeightTo01Duration > time)
            {
                time += Time.deltaTime;
                yield return null;

                m_AimRig.weight = time;

            }

        }
        IEnumerator SetWeight0()
        {
            float time = 0;
            while (m_WeightTo01Duration > time)
            {
                time += Time.deltaTime;
                yield return null;
                m_AimRig.weight = 1 - time;
            }

        }

    }
}