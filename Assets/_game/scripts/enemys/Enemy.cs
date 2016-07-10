using System;
using UnityEngine;
using System.Collections;

public class Enemy : MoveObject
{

	public AttackParams Attack;

	private MoveObject _enemyObject;
	private BulletManager _bulletManager;

	public static Enemy Create(Transform spawnPoint, WaveAttack attackParams)
	{
		Enemy enemy = Create(spawnPoint);

		enemy.Attack = attackParams.Params;
		enemy.MovingSpeed = attackParams.Speed;

		return enemy;
	}
	public static Enemy Create(Transform spawnPoint)
	{
		return Create(spawnPoint ? spawnPoint.position : Vector3.zero);
	}
	public static Enemy Create(Vector3 spawnPoint)
	{
		GameObject obj = Instantiate(The.GameLogic.EnemyPrefab);
		obj.transform.SetParent(The.GameLogic.EnemyHolder.transform);
		obj.transform.position = spawnPoint;

		return obj.GetComponent<Enemy>();
	}

	protected override void Start()
	{
		base.Start();
		_bulletManager = gameObject.AddComponent<BulletManager>();
		AllowRandomMove = true;
		AtackTag = "enemy";

	}

	protected override void Update()
	{
		base.Update();

		//if (!_enemyObject)
		{
			GameObject obj = Helper.FindClosestObject(transform.position, new []{TagKind.Monster.ToString(), TagKind.Cow.ToString() } );
			if (obj)
			{
				_enemyObject = obj.GetComponent<MoveObject>();
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
				_bulletManager.Shot(this, _enemyObject, "monster", Attack);
				Stop();
			}

		}
	}
}
