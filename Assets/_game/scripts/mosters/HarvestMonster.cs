using System;
using UnityEngine;
using System.Collections;

public enum HarvesKind
{
	None,
	Harvest, 
	Unload
}

public class HarvestMonster : MonsterAction
{
	public float HarvestStorage = 0;

	private bool _harvesting;
	public HarvesKind Status = HarvesKind.None;

	protected override void Update()
	{
		if (!Actor && HarvestStorage <= 0)
		{
			_monster.MoveToPosition(_monster.Parent);
			Destroy(this);
			return;
		}

		if (!_monster.Parent)
		{
			return;
		}

		HarvestActivity activitie = null;
		if (!activitie)
		{
			return;
		}

		if (Status == HarvesKind.Unload)
		{
			if (HarvestStorage <= 0)
			{
				Status = HarvesKind.None;
				_harvesting = false;
				return;
			}

			if (_monster.Distance(_monster.Parent) > activitie.HarvestRange)
			{
				_monster.MoveToPosition(_monster.Parent);
				return;
			}

			_monster.Stop();
			if (!_harvesting)
			{
				StartCoroutine(Unload());
			}
			return;
		}

		if (Status == HarvesKind.Harvest)
		{
			if (HarvestStorage >= activitie.HarvestStorage || !Actor)
			{
				Status = HarvesKind.Unload;
				_harvesting = false;
				return;
			}

			if (_monster.Distance(Actor) > activitie.HarvestRange)
			{
				_monster.MoveToPosition(Actor);
				return;
			}

			_monster.Stop();
			if (!_harvesting)
			{
				StartCoroutine(Harvest());
			}

			return;
		}

		if (_monster.Distance(Actor) > activitie.HarvestRange)
		{
			Status = HarvesKind.Harvest;
			_monster.MoveToPosition(Actor);
		}

	}

	IEnumerator Harvest()
	{
		if (_harvesting)
		{
			yield break;
		}

		_harvesting = true;
		HarvestActivity activitie = null;
		while (HarvestStorage < activitie.HarvestStorage)
		{
			yield return new WaitForSeconds(activitie.HarvestTime);
			if (!_harvesting) break;

			HarvestStorage += activitie.HarvestRate;
		}

		if (HarvestStorage > activitie.HarvestStorage)
		{
			HarvestStorage = activitie.HarvestStorage;
		}
		_harvesting = false;
	}

	IEnumerator Unload()
	{
		if (_harvesting)
		{
			yield break;
		}
		_harvesting = true;

		HarvestActivity activitie = null;//_monster.Parent.Activitie<HarvestActivity>();
		while (HarvestStorage > 0)
		{
			yield return new WaitForSeconds(activitie.HarvestTime);
			if (!_harvesting) break;

			HarvestStorage -= activitie.HarvestRate;
			GameLogic.Sula += activitie.HarvestRate;
		}

		if (HarvestStorage < 0)
		{
			HarvestStorage = 0;
		}
		_harvesting = false;

	}

}
