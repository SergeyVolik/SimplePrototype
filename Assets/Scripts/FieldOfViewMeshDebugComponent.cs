using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
	[RequireComponent(typeof(FieldOfViewSystem))]
	public class FieldOfViewMeshDebugComponent : MonoBehaviour
	{


		[SerializeField]
		private float m_MeshResolution = 1;
		[SerializeField]
		private int m_EdgeResolveIterations = 4;
		[SerializeField]
		private float m_EdgeDstThreshold = 0.5f;

		[SerializeField]
		private MeshFilter m_ViewMeshFilter;

		[SerializeField]
		private FieldOfViewSystem m_FOW;
		public FieldOfViewSystem FOW => m_FOW;
		private Mesh m_ViewMesh;

		[SerializeField]
		private bool NeedUpdate;


		// Start is called before the first frame update
		void Start()
		{
			//m_FOW = GetComponent<FieldOfViewSystem>();
			m_ViewMesh = new Mesh();
			m_ViewMesh.name = "View Mesh";
			m_ViewMeshFilter.mesh = m_ViewMesh;
		}

		void LateUpdate()
		{
			if (NeedUpdate)
			{
				DrawFieldOfView();
			}
		}


        private void OnBecameInvisible()
        {
			NeedUpdate = false;
		}

        private void OnBecameVisible()
        {
			NeedUpdate = true;		
		}



		void DrawFieldOfView()
		{
			int stepCount = Mathf.RoundToInt(m_FOW.viewAngle * m_MeshResolution);
			float stepAngleSize = m_FOW.viewAngle / stepCount;
			List<Vector3> viewPoints = new List<Vector3>();
			ViewCastInfo oldViewCast = new ViewCastInfo();
			for (int i = 0; i <= stepCount; i++)
			{
				float angle = transform.eulerAngles.y - m_FOW.viewAngle / 2 + stepAngleSize * i;
				ViewCastInfo newViewCast = ViewCast(angle);

				if (i > 0)
				{
					bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > m_EdgeDstThreshold;
					if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
					{
						EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
						if (edge.pointA != Vector3.zero)
						{
							viewPoints.Add(edge.pointA);
						}
						if (edge.pointB != Vector3.zero)
						{
							viewPoints.Add(edge.pointB);
						}
					}

				}


				viewPoints.Add(newViewCast.point);
				oldViewCast = newViewCast;
			}

			int vertexCount = viewPoints.Count + 1;
			Vector3[] vertices = new Vector3[vertexCount];
			int[] triangles = new int[(vertexCount - 2) * 3];

			vertices[0] = Vector3.zero;
			for (int i = 0; i < vertexCount - 1; i++)
			{
				vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

				if (i < vertexCount - 2)
				{
					triangles[i * 3] = 0;
					triangles[i * 3 + 1] = i + 1;
					triangles[i * 3 + 2] = i + 2;
				}
			}

			m_ViewMesh.Clear();

			m_ViewMesh.vertices = vertices;
			m_ViewMesh.triangles = triangles;
			m_ViewMesh.RecalculateNormals();
		}

		EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
		{
			float minAngle = minViewCast.angle;
			float maxAngle = maxViewCast.angle;
			Vector3 minPoint = Vector3.zero;
			Vector3 maxPoint = Vector3.zero;

			for (int i = 0; i < m_EdgeResolveIterations; i++)
			{
				float angle = (minAngle + maxAngle) / 2;
				ViewCastInfo newViewCast = ViewCast(angle);

				bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > m_EdgeDstThreshold;
				if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
				{
					minAngle = angle;
					minPoint = newViewCast.point;
				}
				else
				{
					maxAngle = angle;
					maxPoint = newViewCast.point;
				}
			}

			return new EdgeInfo(minPoint, maxPoint);
		}


		ViewCastInfo ViewCast(float globalAngle)
		{
			Vector3 dir = m_FOW.DirFromAngle(globalAngle, true);
			RaycastHit hit;

			if (Physics.Raycast(transform.position, dir, out hit, m_FOW.viewRadius, m_FOW.obstacleMask))
			{
				return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
			}
			else
			{
				return new ViewCastInfo(false, transform.position + dir * m_FOW.viewRadius, m_FOW.viewRadius, globalAngle);
			}
		}



		public struct ViewCastInfo
		{
			public bool hit;
			public Vector3 point;
			public float dst;
			public float angle;

			public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
			{
				hit = _hit;
				point = _point;
				dst = _dst;
				angle = _angle;
			}
		}

		public struct EdgeInfo
		{
			public Vector3 pointA;
			public Vector3 pointB;

			public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
			{
				pointA = _pointA;
				pointB = _pointB;
			}
		}
	}
}