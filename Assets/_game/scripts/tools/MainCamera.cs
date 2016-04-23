using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{

	public float ZoomSpeed = 2;

	private Vector3 TargetPosition;

	// Use this for initialization
	void Start()
	{
		TargetPosition = transform.position;
	}

	// Update is called once per frame
	void Update()
	{

		TargetPosition.y -= Input.mouseScrollDelta.y;
		TargetPosition.x += Input.GetAxis("Horizontal") * 0.1f;
		TargetPosition.z += Input.GetAxis("Vertical") * 0.1f;

		if (Vector3.Distance(transform.position, TargetPosition) > 0.01f)
		{
			transform.position = Vector3.MoveTowards(transform.position, TargetPosition, 0.3f);
		}
	}
}
