using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace SerV112.UtilityAI.Game
{
    public class PrepareRotation : MonoBehaviour
    {
        [SerializeField]
        private Vector3 m_TargetRot;

        [SerializeField]
        private float m_Speed = 1f;

        [SerializeField]
        private UnityEvent m_PrepareFinished;

        public UnityEvent PrepareFinished => m_PrepareFinished;
        public void Prepare()
        {
            if (!started)
            {
                StartCoroutine(Rotate());
            }
        }

        bool started = false;
        IEnumerator Rotate()
        {
            started = true;
            float t = 0;
            var startRot = transform.rotation;
            var target = Quaternion.Euler(m_TargetRot);
            while (t < 1)
            {
                t += Time.deltaTime * m_Speed;
                transform.rotation = Quaternion.Lerp(startRot, target, t);
                yield return null;
            }

            t = 1;
            transform.rotation = Quaternion.Lerp(startRot, target, t);

            yield return null;
            started = false;

            m_PrepareFinished.Invoke();

        }
    }
}