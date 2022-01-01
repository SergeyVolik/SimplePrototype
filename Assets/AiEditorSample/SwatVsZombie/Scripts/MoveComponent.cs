using UnityEngine;
using System.Collections;
namespace SerV112.UtilityAI.Game
{
	public class MoveComponent : MonoBehaviour, IPlayer
	{
		[SerializeField]
		private float m_MoveSpeed = 6;
		[SerializeField]
		private float m_RotSpeed = 30f;

		private Rigidbody m_Rigidbody;
		private Camera m_ViewCamera;
		private Vector3 m_Velocity;

		private Vector3 m_AimRotation;
		bool m_Aim;
		bool m_Move;

		public bool IsMove => m_Move;
		public bool Aim => m_Aim;


		IMoveData m_MoveData;
		void Start()
		{
			m_MoveData = GetComponent<IMoveData>();

			m_Rigidbody = GetComponent<Rigidbody>();
			m_ViewCamera = Camera.main;

		}

		void AimRot()
		{
			m_Rigidbody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_AimRotation), Time.fixedDeltaTime * m_RotSpeed);
		}


		void DefaultRot()
		{
			if (m_Velocity != Vector3.zero)
			{
				m_Move = true;
				m_Rigidbody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_Velocity), Time.fixedDeltaTime * m_RotSpeed);
			}
			else
			{
				m_Move = false;

			}

		}




		void Update()
		{

			if (Input.GetMouseButtonDown(1))
			{
				m_Aim = true;

			}
			else if (Input.GetMouseButtonUp(1))
			{
				m_Aim = false;
			}


			var ray = m_ViewCamera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
			{
				m_AimRotation = Vector3.ProjectOnPlane(hit.point - transform.position, Vector3.up).normalized;
			}

			//transform.LookAt(transform.position + vec * 10);

			//transform.rotation = 

			m_Velocity = new Vector3(m_MoveData.Horizontal, 0, m_MoveData.Vertical).normalized * m_MoveSpeed;




		}

		void FixedUpdate()
		{
			Move();
		}

		void Move()
		{
			m_Rigidbody.MovePosition(m_Rigidbody.position + m_Velocity * Time.fixedDeltaTime);

			if (m_Aim)
			{
				AimRot();
			}
			else
			{
				DefaultRot();
			}
		}

	}
}