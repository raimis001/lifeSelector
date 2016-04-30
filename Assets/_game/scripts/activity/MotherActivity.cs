using UnityEngine;
using System.Collections;

public class MotherActivity : Activity
{

	public float SpawnTime = 100;

	private float _spawnTime;

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

		_spawnTime -= Time.deltaTime;
		if (_spawnTime <= 0)
		{
			Monster monster = Monster.Create(transform.position + new Vector3().Random(true) * Cow.RadarRange + Vector3.down * 0.5f);
			monster.Actor = Cow;
			_spawnTime = SpawnTime;
		}
	}
}
