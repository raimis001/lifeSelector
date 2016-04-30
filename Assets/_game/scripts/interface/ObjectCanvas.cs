using UnityEngine;
using System.Collections;

public class ObjectCanvas : MonoBehaviour
{

	public Transform Parent;
	public Vector3 Delta;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		transform.eulerAngles = Vector3.zero;
		transform.position = Parent.position + Delta;
	}
}
