using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{

	[Range(2,10)]
	public float ZoomSpeed = 2;

	[Range(1, 10)]
	public float ScrollSpeed = 1;

	private Vector3 TargetPosition;
	private Vector3 lastPosition;

	// Use this for initialization
	void Start()
	{
		TargetPosition = transform.position;
	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetMouseButtonDown(2))
		{
			lastPosition = Input.mousePosition;
		}

		if (Input.GetMouseButton(2))
		{
			Vector3 delta = -(Input.mousePosition - lastPosition);
			transform.Translate(delta.x * 0.05f, delta.y * 0.05f, 0);
			lastPosition = Input.mousePosition;
			TargetPosition = transform.position;
			return;
		}

		TargetPosition.y -= Input.mouseScrollDelta.y;
		TargetPosition.x += Input.GetAxis("Horizontal") * 0.1f * ScrollSpeed;
		TargetPosition.z += Input.GetAxis("Vertical") * 0.1f * ScrollSpeed;

		if (Vector3.Distance(transform.position, TargetPosition) > 0.01f)
		{
			transform.position = Vector3.MoveTowards(transform.position, TargetPosition, 0.7f);
		}
	}
}
