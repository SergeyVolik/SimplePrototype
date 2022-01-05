using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
	[RequireComponent(typeof(MoveData))]
	[RequireComponent(typeof(CharacterController))]
	public class SimpleRotateSystem : MonoBehaviour
	{
		private CharacterController m_Rigidbody;
		private IMoveInputData m_MoveData;
		private IRotationSpeed data;
		private IAimInputData aimData;

		private void Awake()
		{
			aimData = GetComponent<IAimInputData>();
			data = GetComponent<IRotationSpeed>();
			m_Rigidbody = GetComponent<CharacterController>();
			m_MoveData = GetComponent<IMoveInputData>();

		}

		bool NeedRotate = true;

        private void OnEnable()
        {
			aimData.PressDown.AddListener(DisableRotation);
			aimData.PressUp.AddListener(EnableRotation);

		}

        private void OnDisable()
        {
			aimData.PressDown.RemoveListener(DisableRotation);
			aimData.PressUp.RemoveListener(EnableRotation);
		}

		void EnableRotation()
		{
			NeedRotate = true;
		}

		void DisableRotation()
		{
			NeedRotate = false;
		}

		private void FixedUpdate()
		{
			DefaultRot();
		}

		private void DefaultRot()
		{
			if (m_MoveData.IsMove && NeedRotate)
			{

				transform.rotation = Quaternion.Slerp(
					transform.rotation,
					Quaternion.LookRotation(new Vector3(m_MoveData.Horizontal, 0, m_MoveData.Vertical).normalized),
					Time.fixedDeltaTime * data.RotationSpeed
					);
			}

		}
	}

}
