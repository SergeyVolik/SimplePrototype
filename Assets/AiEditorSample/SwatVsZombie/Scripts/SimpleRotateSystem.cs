using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
	[RequireComponent(typeof(MoveDataComponent))]
	[RequireComponent(typeof(Rigidbody))]
	public class SimpleRotateSystem : MonoBehaviour
	{
		private Rigidbody m_Rigidbody;
		private IMoveInputData m_MoveData;
		private IRotationSpeed data;

		private void Awake()
		{
			data = GetComponent<IRotationSpeed>();
			m_Rigidbody = GetComponent<Rigidbody>();
			m_MoveData = GetComponent<IMoveInputData>();

		}
		private void FixedUpdate()
		{
			DefaultRot();
		}

		private void DefaultRot()
		{
			if (m_MoveData.IsMove)
			{
				m_Rigidbody.rotation = Quaternion.Slerp(
					transform.rotation,
					Quaternion.LookRotation(new Vector3(m_MoveData.Horizontal, 0, m_MoveData.Vertical).normalized),
					Time.fixedDeltaTime * data.RotationSpeed
					);
			}

		}
	}

}
