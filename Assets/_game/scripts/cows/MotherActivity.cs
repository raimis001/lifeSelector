using UnityEngine;
using System.Collections;

public class MotherActivity : CowActivity
{

	public float SpawnTime = 100;
	public int CowSpawnCount = 5;

	private float _spawnTime;
	private bool _spawnCow;

	protected override void Start()
	{
		base.Start();
		_spawnTime = SpawnTime;
	}

	protected override void Update()
	{
		base.Update();
		if (!Cow.AllowMonsters())
		{
			return;
		}

		if (_spawnCow)
		{
			return;
		}

		_spawnTime -= Time.deltaTime;
		if (_spawnTime <= 0)
		{
			Monster monster = Monster.Create(transform.position + new Vector3().Random(true) * Cow.RadarRange + Vector3.down * 0.5f);
			monster.Parent = Cow;
			_spawnTime = SpawnTime;
		}
	}

	public void SpawnCow()
	{
		if (Cow.MonsterCount < CowSpawnCount || _spawnCow)
		{
			return;
		}
		_spawnCow = true;

		Monster m = Cow.Monsters[0];

		for (int i = 0; i < Cow.Monsters.Count; i++)
		{
			//CowMonster mm = Cow.Monsters[i].AddAction<CowMonster>(m);
			////mm.MainMonster = i == 0;
			//mm.Monster = m;
		}
	}

	public void EndSpawn()
	{
		_spawnCow = false;
	}
}
