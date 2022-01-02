using UnityEngine;
using System.Collections;
namespace SerV112.UtilityAI.Game
{

	[RequireComponent(typeof(MoveDataComponent))]
	public class MoveSystem : MonoBehaviour
	{

		private Rigidbody m_Rigidbody;
		private Vector3 m_Velocity;
		private ICurrentSpeed m_MoveSpeed;
		private IMoveInputData m_MoveData;

		void Awake()
		{
			m_MoveData = GetComponent<IMoveInputData>();
			m_MoveSpeed = GetComponent<ICurrentSpeed>();
			m_Rigidbody = GetComponent<Rigidbody>();

		}

		void Update()
		{

			m_Velocity = new Vector3(m_MoveData.Horizontal, 0, m_MoveData.Vertical).normalized * m_MoveSpeed.CurrentSpeed;
		}

		void FixedUpdate()
		{
			Move();
		}

		void Move()
		{
			m_Rigidbody.MovePosition(m_Rigidbody.position + m_Velocity * Time.fixedDeltaTime);
		}

	}
}