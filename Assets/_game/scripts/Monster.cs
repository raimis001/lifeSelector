using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	[Range(0, 5)]
	public float MovingSpeed = 1;

	[Range(1,10)]
	public float MovingTime;

	public Vector3 MovingVector;

	Rigidbody Rigibody;
	float _movingTime;

	public float Lives = 200;
	public float LivesModiefier = 1;

	// Use this for initialization
	void Start () {
		Rigibody = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update ()
	{
		Lives -= LivesModiefier*Time.deltaTime;
		if (Lives <= 0)
		{
			Destroy(gameObject);
			return;
		}

		if (_movingTime <= 0)
		{
			RandomizeMove();
			_movingTime = MovingTime;
			return;
		}
		//Debug.DrawLine(transform.position, Stimulus.position, Color.blue);
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

		Stimulus[] stimuli = GameObject.FindObjectsOfType<Stimulus>();
		foreach (Stimulus stimulus in stimuli)
		{
			if (Vector3.Distance(transform.position, stimulus.transform.position) < stimulus.Radiuss)
			{
				AddForce(stimulus);
			}
		}
	}

	public void AddForce(Stimulus stimulus)
	{
		Vector3 stimuls = (stimulus.transform.position - transform.position).normalized;
		stimuls.y = 0;
		Rigibody.AddForce(stimuls * stimulus.Force);
	}

	void Log(object message)
	{
		Debug.Log(message);
	}
}
