using UnityEngine;
using System.Collections;

public class CowMonster : MonsterAction
{

	public bool MainMonster;
	public Monster Monster;

	private int _counter;

	protected override void Update()
	{
		//base.Update();

		if (!MainMonster && Monster)
		{
			_monster.MoveToPosition(Monster);
		}
		else
		{
			_monster.Stop();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!MainMonster)
		{
			return;
		}

		CowMonster monster = collision.gameObject.GetComponent<CowMonster>();
		if (!monster)
		{
			return;
		}

		The.GameLogic.DoExplosion(monster.transform.position, The.GameLogic.CollideMonsterEffect);
		monster._monster.DoDestroy(false);

		_counter++;
		MotherActivity activitie = _monster.Actor.Activitie<MotherActivity>();

		if (_counter >= activitie.CowSpawnCount - 1)
		{
			Cow.Create(_monster.Position);
			_monster.DoDestroy(false);
			activitie.EndSpawn();
		}

	}
}
