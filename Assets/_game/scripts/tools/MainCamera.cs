using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{

	[Range(2,10)]
	public float ZoomSpeed = 2;

	[Range(1, 10)]
	public float ScrollSpeed = 1;

	private Vector3 _targetPosition;
	private Vector3 _lastPosition;
	private float _targetZoom;

	private Camera Camera;

	// Use this for initialization
	void Start()
	{
		Camera = GetComponent<Camera>();

		_targetPosition = transform.position;
		_targetZoom = Camera.fieldOfView;

	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetMouseButtonDown(2))
		{
			_lastPosition = Input.mousePosition;
		}

		if (Input.GetMouseButton(2))
		{
			Vector3 delta = -(Input.mousePosition - _lastPosition);
			transform.Translate(delta.x * 0.05f, delta.y * 0.05f, 0);
			_lastPosition = Input.mousePosition;
			_targetPosition = transform.position;
			return;
		}


		_targetPosition.x += Input.GetAxis("Horizontal") * 0.1f * ScrollSpeed;
		_targetPosition.z += Input.GetAxis("Vertical") * 0.1f * ScrollSpeed;

		if (Vector3.Distance(transform.position, _targetPosition) > 0.01f)
		{
			transform.position = Vector3.MoveTowards(transform.position, _targetPosition, 0.7f);
		}

		_targetZoom -= Input.mouseScrollDelta.y * ZoomSpeed;

		if (Mathf.Abs(_targetZoom - Camera.fieldOfView) > 0.1f)
		{
			Camera.fieldOfView = Mathf.Lerp(Camera.fieldOfView, _targetZoom, 0.1f);
		}
		else
		{
			Camera.fieldOfView = _targetZoom;
		}

	}
}
