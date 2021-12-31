using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{

	public float moveSpeed = 6;

	Rigidbody rigidbody;
	Camera viewCamera;
	Vector3 velocity;
	public float m_RotSpeed = 30f;

	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		viewCamera = Camera.main;
	}
	public Vector3 mousePos;
	public Vector3 vec;
	void Update()
	{

		var ray = viewCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
		{
			vec = Vector3.ProjectOnPlane( hit.point - transform.position, Vector3.up).normalized;
		}

		//transform.LookAt(transform.position + vec * 10);

		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vec), Time.deltaTime* m_RotSpeed);
		velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * moveSpeed;
	}

	void FixedUpdate()
	{
		rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
		
	}

    private void OnDrawGizmosSelected()
    {
		if (Application.isPlaying)
		{
			//Gizmos.color = Color.yellow;

			//Gizmos.DrawLine(transform.position, transform.position + transform.forward * 10);
			//Gizmos.DrawLine(transform.position, mousePos);

			//Gizmos.color = Color.red;
			//Gizmos.DrawLine(transform.position, transform.position + vec*10);

			//Gizmos.color = Color.yellow;
			//Gizmos.DrawSphere(mousePos, 0.1F);
		}
	}

}