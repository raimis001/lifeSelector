﻿using UnityEngine;
using UnityEngine.Events;

public class Mover : MonoBehaviour
{
	[Range(0, 5)]
	public float MovingSpeed = 1;
	[Range(1, 10)]
	public float MovingTime;

	private Rigidbody _rigibody;
	private float _movingTime;

	private bool _moveToPosition;
	private Vector3 _destination;
	private Vector3 _velocity;
	private float _lastDistance;

	// Use this for initialization
	void Start()
	{
		_rigibody = GetComponent<Rigidbody>();

	}

	// Update is called once per frame
	void Update()
	{
		if (_moveToPosition)
		{
			float sqrMag = (_destination - transform.position).sqrMagnitude;

			if (sqrMag > _lastDistance)
			{
				Stop();
				_moveToPosition = false;
			}

			_lastDistance = sqrMag;
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

	void FixedUpdate()
	{
		if (_moveToPosition)
		{
			_rigibody.velocity = _velocity;
		}
	}

	void RandomizeMove()
	{
		Vector3 movingVector = new Vector3().Random();

		movingVector.Normalize();
		movingVector.y = 0;

		_rigibody.velocity = movingVector * MovingSpeed;
		_rigibody.angularVelocity = Vector3.zero;
	}

	public void Stop()
	{
		Debug.Log("Stop mover");
		_rigibody.velocity = Vector3.zero;
		_rigibody.angularVelocity = Vector3.zero;
	}

	public void MoveToPosition(Vector3 position)
	{
		_moveToPosition = true;
		_destination = position;
		_destination.y = 0;

		Vector3 velocity = (_destination - transform.position).normalized; ;
		velocity.Normalize();
		velocity.y = 0;

		_velocity = velocity*MovingSpeed;

		_rigibody.velocity = _velocity;

		_lastDistance = Mathf.Infinity;
	}

	public void AddForce(Transform stimulus, float force)
	{
		Vector3 st = (stimulus.position - transform.position).normalized;
		st.y = 0;
		_rigibody.AddForce(st * force);
	}


}