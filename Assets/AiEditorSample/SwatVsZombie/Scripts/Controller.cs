using UnityEngine;
using System.Collections;
using UnityEngine.Animations.Rigging;

public class Controller : MonoBehaviour
{
	[SerializeField]
	private float m_MoveSpeed = 6;
	[SerializeField]
	private float m_RotSpeed = 30f;
	private Rigidbody m_Rigidbody;
	private Camera m_ViewCamera;
	private Vector3 m_Velocity;
	[SerializeField]
	private Animator m_Controller;

	private static readonly int m_AnimatorParamX;
	private static readonly int m_AnimatorParamY;
	private static readonly int m_AimParam;
	private static readonly int m_MoveParam;
	[SerializeField]
	Rig m_AimRig;

	static Controller()
	{
		m_AnimatorParamX = Animator.StringToHash("Horizontal");
		m_AnimatorParamY = Animator.StringToHash("Vertical");
		m_AimParam = Animator.StringToHash("Aim");
		m_MoveParam = Animator.StringToHash("Move");
	}
	void Start()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
		m_ViewCamera = Camera.main;
		m_AimRig.weight = 0;
	}
	public Vector3 mousePos;
	public Vector3 vec;
	bool m_Aim;
	bool m_Move;
	void AimRot()
	{
		m_Rigidbody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vec), Time.fixedDeltaTime * m_RotSpeed);
	}

	void DefaultRot()
	{
		if (m_Velocity != Vector3.zero)
		{
			m_Move = true;
			m_Rigidbody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_Velocity), Time.fixedDeltaTime * m_RotSpeed);
		}
		else {
			m_Move = false;
		
		}

		m_Controller.SetBool(m_MoveParam, m_Move);
	}


	private float m_WeightTo01Duration = 1f;
	IEnumerator SetWeight1()
	{
		float time = 0;
		while (m_WeightTo01Duration > time)
		{
			time += Time.deltaTime;
			yield return null;
			m_AimRig.weight = time;

		}
		
	}
	IEnumerator SetWeight0()
	{
		float time = 0;
		while (m_WeightTo01Duration > time)
		{
			time += Time.deltaTime;
			yield return null;
			m_AimRig.weight = 1 - time;
		}

	}
	Coroutine lastCoroutime;
	void Update()
	{

		if (Input.GetMouseButtonDown(1))
		{
			m_Aim = true;
			m_Controller.SetBool(m_AimParam, m_Aim);

			if(lastCoroutime != null)
				StopCoroutine(lastCoroutime);
			lastCoroutime = StartCoroutine(SetWeight1());
			

		}
		else if (Input.GetMouseButtonUp(1))
		{
			m_Aim = false;
			m_Controller.SetBool(m_AimParam, m_Aim);
			if (lastCoroutime != null)
				StopCoroutine(lastCoroutime);
			lastCoroutime = StartCoroutine(SetWeight0());
		}


		var ray = m_ViewCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
		{
			vec = Vector3.ProjectOnPlane( hit.point - transform.position, Vector3.up).normalized;
		}

		//transform.LookAt(transform.position + vec * 10);

		//transform.rotation = 
		var Horizontal = Input.GetAxisRaw("Horizontal");
		var Vertical = Input.GetAxisRaw("Vertical");
		m_Velocity = new Vector3(Horizontal, 0, Vertical).normalized * m_MoveSpeed;

		var forwardDot = Vector3.Dot(transform.forward, Vector3.forward);
		var rightDot = Vector3.Dot(transform.forward, Vector3.right);
		//Debug.Log($"forwardDot {forwardDot} {rightDot}");
		if (forwardDot > 0.9f && forwardDot > rightDot)
		{
			Debug.Log("Up");
			
		}
		else if (forwardDot < -0.9 && rightDot > forwardDot)
		{
			Debug.Log("Down");
			Horizontal = -Horizontal;
			Vertical = -Vertical;
		}
        else if (rightDot < -0.9 && rightDot < forwardDot)
        {
			var hor = Horizontal;
			Horizontal = Vertical;
			Vertical = hor;
			Debug.Log("Left");
		}

        else if (rightDot > 0.9 && rightDot > forwardDot)
		{
			var hor = -Horizontal;
			Horizontal = -Vertical;
			Vertical = hor;
			Debug.Log("Right");
		}

		m_Controller.SetFloat(m_AnimatorParamX, Horizontal);
		m_Controller.SetFloat(m_AnimatorParamY, Vertical);
	}

	void FixedUpdate()
	{
		m_Rigidbody.MovePosition(m_Rigidbody.position + m_Velocity * Time.fixedDeltaTime);

		if (m_Aim)
		{
			AimRot();
		}
		else {
			DefaultRot();
		}

	}

}