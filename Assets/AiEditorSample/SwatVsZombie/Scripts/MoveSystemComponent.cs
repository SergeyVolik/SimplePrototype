using UnityEngine;
using System.Collections;
namespace SerV112.UtilityAI.Game
{

	[RequireComponent(typeof(MoveDataComponent))]
	public class MoveSystemComponent : MonoBehaviour
	{
		

		private Rigidbody m_Rigidbody;
		private Vector3 m_Velocity;

		private IMoveSpeed m_MoveSpeed;
		private IMoveInputData m_MoveData;
		void Start()
		{
			Debug.Log("Start");
			m_MoveData = GetComponent<IMoveInputData>();
			m_MoveSpeed = GetComponent<IMoveSpeed>();
			m_Rigidbody = GetComponent<Rigidbody>();

		}


		void Update()
		{

			m_Velocity = new Vector3(m_MoveData.Horizontal, 0, m_MoveData.Vertical).normalized * m_MoveSpeed.MoveSpeed;
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