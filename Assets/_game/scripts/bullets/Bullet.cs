﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

	public Vector3 Target;
	private Rigidbody _rigidbody;

	private string _tag;
	private float _time = 10;
	public float _damage;

	public static void Create(BaseObject moveObject, BaseObject targetObject, TagKind tag, AttackParams attack)
	{
		GameObject obj = Instantiate(The.GameLogic.BulletPrefab);
		obj.transform.position = moveObject.Position + (targetObject.Position - moveObject.Position).normalized * 0.7f + new Vector3(0,0.5f,0);

		Bullet bullet = obj.GetComponent<Bullet>();
		bullet.Target = targetObject.Position;
		bullet._tag = tag == TagKind.Enemy ? "monster" : "enemy";
		bullet.tag = "Bullet";
		bullet._damage = attack.AttackDamage;

		IgnoreTag(bullet.tag, bullet);

		IgnoreTag(tag.ToString(), bullet);
		if (tag == TagKind.Monster)
		{
			IgnoreTag("Cow", bullet);
		}

	}

	static void IgnoreTag(string tagIgnore, Bullet bullet)
	{
		GameObject[] alies = GameObject.FindGameObjectsWithTag(tagIgnore);

		foreach (GameObject aly in alies)
		{
			Collider[] c = aly.GetComponentsInChildren<Collider>();
			foreach (Collider collider1 in c)
			{
				Physics.IgnoreCollision(bullet.GetComponent<Collider>(), collider1);
			}
		}
	}

	// Use this for initialization
	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
		Vector3 st = (Target - transform.position).normalized;
		st.y = 0;
		_rigidbody.AddForce(st* _rigidbody.mass * 200);
	}

	// Update is called once per frame
	private void Update()
	{
		_time -= Time.deltaTime;
		if (_time <= 0)
		{
			Destroy(gameObject);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		//Debug.Log(collision.gameObject.name);
		MoveObject enemy = collision.gameObject.GetComponent<MoveObject>();

		if (enemy)
		{
			if (enemy.AtackTag.Equals(_tag))
			{
				enemy.DoDamage(_damage);
				Destroy(gameObject);
			}
			return;
		}

		Destroy(gameObject);

	}

}
