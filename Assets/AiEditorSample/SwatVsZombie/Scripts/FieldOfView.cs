using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
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
	//[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();
	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

	void Start()
	{
		StartCoroutine("FindTargetsWithDelay", .2f);
	}


	IEnumerator FindTargetsWithDelay(float delay)
	{
		while (true)
		{
			yield return new WaitForSeconds(delay);
			FindVisibleTargets();
		}
	}



	void FindVisibleTargets()
	{
		visibleTargets.Clear();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++)
		{
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
			{
				float dstToTarget = Vector3.Distance(transform.position, target.position);
				if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
				{
					visibleTargets.Add(target);
				}
			}
		}
	}

	


	

}