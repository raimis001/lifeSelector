using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cow : BaseObject
{

	public float RadarRange = 3;

	public int MaxMonsterCount = 2;
	public int MonsterCount
	{
		get { return Monsters.Count; }
	}

	public List<Monster> Monsters = new List<Monster>();


	// Use this for initialization
	protected override void Start()
	{
		base.Start();
		AllowRandomMove = false;
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
			if (MonsterCount >= MaxMonsterCount) break;
		}
	}



	public void ReleaseMonsters()
	{
		while (Monsters.Count > 0)
		{
			Monsters[0].Actor = null;
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

	public T Activitie<T>() where T : Activity
	{
		return GetComponent<T>();
	}

}
