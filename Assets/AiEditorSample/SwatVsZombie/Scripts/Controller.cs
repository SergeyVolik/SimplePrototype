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
	private FieldOfView EyeSensor;

	
	
	public Vector3 mousePos;
	public Vector3 vec;
	bool m_Aim;
	bool m_Move;
	public bool Move => m_Move;
	public bool Aim => m_Aim;
	public float Horizontal => m_Horizontal;
	public float Vertical => m_Vertical;
	public float m_Vertical;
	public float m_Horizontal;
	void Start()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
		m_ViewCamera = Camera.main;

	}

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
			vec = Vector3.ProjectOnPlane( hit.point - transform.position, Vector3.up).normalized;
		}

		//transform.LookAt(transform.position + vec * 10);

		//transform.rotation = 
		m_Horizontal = Input.GetAxisRaw("Horizontal");
		m_Vertical = Input.GetAxisRaw("Vertical");
		m_Velocity = new Vector3(m_Horizontal, 0, Vertical).normalized * m_MoveSpeed;

		var forwardDot = Vector3.Dot(transform.forward, Vector3.forward);
		var rightDot = Vector3.Dot(transform.forward, Vector3.right);
		//Debug.Log($"forwardDot {forwardDot} {rightDot}");
		if (forwardDot > 0.9f && forwardDot > rightDot)
		{
			//Debug.Log("Up");
			
		}
		else if (forwardDot < -0.9 && rightDot > forwardDot)
		{
			//Debug.Log("Down");
			m_Horizontal = -m_Horizontal;
			m_Vertical = -Vertical;
		}
        else if (rightDot < -0.9 && rightDot < forwardDot)
        {
			var hor = m_Horizontal;
			m_Horizontal = Vertical;
			m_Vertical = hor;
			//Debug.Log("Left");
		}

        else if (rightDot > 0.9 && rightDot > forwardDot)
		{
			var hor = -m_Horizontal;
			m_Horizontal = -Vertical;
			m_Vertical = hor;
			//Debug.Log("Right");
		}

	
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