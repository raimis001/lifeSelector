using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	[Range(0, 5)]
	public float MovingSpeed = 1;

	[Range(1,10)]
	public float MovingTime;

	public Transform Stimulus;

	public Vector3 MovingVector;

	Rigidbody Rigibody;
	float _movingTime;

	// Use this for initialization
	void Start () {
		Rigibody = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {
		if (_movingTime <= 0)
		{
			RandomizeMove();
			_movingTime = MovingTime;
			return;
		}

		_movingTime -= Time.deltaTime;
	}

	void RandomizeMove()
	{
		MovingVector = MovingVector.Random();

		MovingVector.Normalize();
		MovingVector.y = 0;

		//Log(MovingVector);
		Rigibody.velocity = MovingVector * MovingSpeed;
		Rigibody.angularVelocity = Vector3.zero;
		Vector3 stimuls = Vector3.MoveTowards(transform.position, Stimulus.position, 10);
		stimuls.Normalize();
		Rigibody.AddForce(stimuls * -100);
	}
	void Log(object message)
	{
		Debug.Log(message);
	}
}
