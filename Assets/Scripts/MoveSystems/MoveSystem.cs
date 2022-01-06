using UnityEngine;
using System.Collections;
namespace SerV112.UtilityAI.Game
{

	[RequireComponent(typeof(CharacterController))]
	[RequireComponent(typeof(IMoveInputData))]
	[RequireComponent(typeof(ICurrentSpeed))]
	public class MoveSystem : MonoBehaviour
	{

		private CharacterController m_CharacterController;
		private ICurrentSpeed m_MoveSpeed;
		private IMoveInputData m_MoveData;
		private Vector3 m_Velocity;
		void Awake()
		{
			m_MoveData = GetComponent<IMoveInputData>();
			m_MoveSpeed = GetComponent<ICurrentSpeed>();
			m_CharacterController = GetComponent<CharacterController>();

		}

		void Update()
		{

			m_Velocity = new Vector3(m_MoveData.Horizontal, 0, m_MoveData.Vertical).normalized * m_MoveSpeed.CurrentSpeed;
			m_CharacterController.Move(m_Velocity * Time.deltaTime);
			
		}
    }
}