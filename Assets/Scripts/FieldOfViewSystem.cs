using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{

	public interface IEnemyDetectedEvent
	{
		UnityEvent<Transform> OnTargetDetected { get; }
		UnityEvent<Transform> OnTargetLost { get; }
	}

	public class FieldOfViewSystem : MonoBehaviour, IEnemyDetectedEvent
	{
		[SerializeField]
		private float m_viewRadius;

		[SerializeField]
		[Range(0, 360)]
		private float m_viewAngle;
		[SerializeField]
		private LayerMask m_targetMask;
		[SerializeField]
		private LayerMask m_obstacleMask;

		public float viewAngle => m_viewAngle;
		public float viewRadius => m_viewRadius;
		public LayerMask targetMask => m_targetMask;
		public LayerMask obstacleMask => m_obstacleMask;

        public UnityEvent<Transform> OnTargetDetected => m_OnDetected;

        public UnityEvent<Transform> OnTargetLost => m_OnTargetLost;

        public UnityEvent<Transform> m_OnDetected;
		public UnityEvent<Transform> m_OnTargetLost;
		//[HideInInspector]
		private List<Transform> visibleTargets = new List<Transform>();
		public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
		{
			if (!angleIsGlobal)
			{
				angleInDegrees += transform.eulerAngles.y;
			}
			return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
		}

		private void Start()
		{ 
			StartCoroutine("FindTargetsWithDelay", .2f);
		}


		private IEnumerator FindTargetsWithDelay(float delay)
		{
			while (true)
			{
				yield return new WaitForSeconds(delay);
				FindVisibleTargets();
			}
		}


		Transform lastTarget;
		bool targetLost = true;
		bool preTargetLost = true;
		void FindVisibleTargets()
		{
			visibleTargets.Clear();
			Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
			targetLost = true;
			

			for (int i = 0; i < targetsInViewRadius.Length; i++)
			{

				Transform target = targetsInViewRadius[i].transform;

				Vector3 dirToTarget = (target.position - transform.position).normalized;
				if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
				{
					float dstToTarget = Vector3.Distance(transform.position, target.position);
					if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
					{
						if (lastTarget == target)
						{
							targetLost = false;
						}

						else visibleTargets.Add(target);
					}
				}

			}

			if (targetLost)
			{
				if(preTargetLost != targetLost)
					m_OnTargetLost.Invoke(lastTarget);
				lastTarget = null;
				if (visibleTargets.Count > 0)
				{
					lastTarget = visibleTargets[0];
					m_OnDetected.Invoke(lastTarget);
				}
				
				
				
			}

			preTargetLost = targetLost;
		}


	}
}
