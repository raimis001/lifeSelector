using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cow : MoveObject
{

	public float RadarRange = 3;

	[Header("Monsters")]
	public int MaxMonsterCount = 2;
	public int MonsterHitpoints = 100;
	public int MonsterCount
	{
		get { return Monsters.Count; }
	}

	public List<Monster> Monsters = new List<Monster>();


	public static Cow Create(Vector3 position)
	{
		GameObject obj = Instantiate(The.GameLogic.CowPrefab);
		obj.transform.SetParent(The.GameLogic.CowHolder.transform);
		obj.transform.position = position;

		The.GameLogic.DoExplosion(position, The.GameLogic.SpawnCowEffect);

		return obj.GetComponent<Cow>();
	}

	// Use this for initialization
	protected override void Start()
	{
		base.Start();
		DragObject = true;
		AllowRandomMove = false;
		AtackTag = "monster";
	}

	protected override void OnMouseClick()
	{
		GameLogic.SelectedCow = this;
	}

	public void CallMonsters()
	{
		if (MonsterCount >= MaxMonsterCount)
		{
			return;
		}

		Monster[] monsters = FindObjectsOfType<Monster>();

		foreach (Monster monster in monsters)
		{
			if (!monster.IsFree()) continue;
			monster.Actor = this;

			if (Math.Abs(monster.MaxHitpoints - monster.Hitpoints) < 0.1f)
			{
				monster.MaxHitpoints = MonsterHitpoints;
				monster.Hitpoints = MonsterHitpoints;
			}
			else
			{
				monster.MaxHitpoints = MonsterHitpoints;
			}
			if (monster.Hitpoints > monster.MaxHitpoints)
			{
				monster.Hitpoints = monster.MaxHitpoints;
			}

			if (MonsterCount >= MaxMonsterCount) break;
		}
	}



	public void ReleaseMonsters()
	{
		while (Monsters.Count > 0)
		{
			Monster monster = Monsters[0];
			Monsters.Remove(monster);
			monster.Actor = null;
		}

		Monsters.Clear();
	}

	public override void Select()
	{
		base.Select();
		The.GameLogic.Radar.Attach(this);
	}

	public override void Deselect()
	{
		base.Deselect();
		The.GameLogic.Radar.Hide();
	}

	public bool AllowMonsters()
	{
		return MaxMonsterCount > MonsterCount;
	}

	public T Activitie<T>() where T : CowActivity
	{
		return GetComponent<T>();
	}


}
