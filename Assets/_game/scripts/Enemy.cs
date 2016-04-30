using System;
using UnityEngine;
using System.Collections;

public class Enemy : BaseObject
{

	public AttackParams Attack;

	private BaseObject _enemyObject;
	private BulletManager _bulletManager;


	public static Monster Create(GameObject spawnPoint)
	{
		return Create(spawnPoint ? spawnPoint.transform.position : Vector3.zero);
	}
	public static Monster Create(Vector3 spawnPoint)
	{
		GameObject obj = Instantiate(The.GameLogic.EnemyPrefab);
		obj.transform.SetParent(The.GameLogic.EnemyHolder.transform);
		obj.transform.position = spawnPoint;

		return obj.GetComponent<Monster>();
	}

	protected override void Start()
	{
		base.Start();
		_bulletManager = gameObject.AddComponent<BulletManager>();
		AllowRandomMove = true;
	}

	protected override void Update()
	{
		base.Update();

		//if (!_enemyObject)
		{
			GameObject obj = Helper.FindClosestObject(transform.position, TagKind.Monster.ToString());
			if (obj)
			{
				_enemyObject = obj.GetComponent<BaseObject>();
			}
		}

		if (_enemyObject)
		{
			if (Distance(_enemyObject) > Attack.AttackRange)
			{
				MoveToPosition(_enemyObject.Position);
			}
			else
			{
				_bulletManager.Shot(this, _enemyObject, TagKind.Monster.ToString(), Attack);
				Stop();
			}
			
		}

		


	}
}
