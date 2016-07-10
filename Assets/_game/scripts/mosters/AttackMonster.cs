using UnityEngine;
using System.Collections;

public class AttackMonster : MonsterAction
{
	private BulletManager _bulletManager;

	protected override void Start()
	{
		base.Start();
		_bulletManager = gameObject.AddComponent<BulletManager>();
	}

	protected override void Update()
	{
		base.Update();

		if (!Actor || !_monster || !_monster.Parent)
		{
			return;
		}

		AttackParams attack = null;//_monster.Parent.Activitie<AttackActivity>().Attack;

		if (_monster.Distance(Actor) > attack.AttackRange)
		{
			_monster.MoveToPosition(Actor.Position);
		}
		else
		{
			_bulletManager.Shot(_monster, Actor, "enemy", attack);
			_monster.Stop();
		}
	}
}
