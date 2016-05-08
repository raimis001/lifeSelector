using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class WaveClass 
{
	public Transform SpawnPoint;
	public float BornTime = 1;
	public List<WaveAttack> Monsters = new List<WaveAttack>();
	public bool Active;

	public WaveClass()
	{
		Monsters.Add(new WaveAttack());
	}

	public IEnumerator WaitMonsters(Action callback)
	{
		if (Monsters.Count < 1)
		{
			yield break;
		}

		Active = true;

		foreach (WaveAttack monster in Monsters)
		{
			yield return new WaitForSeconds(BornTime);
			for (int spawn = 0; spawn < monster.SpawnCount; spawn++)
			{
				Enemy.Create(SpawnPoint, monster);
				yield return new WaitForSeconds(BornTime);
			}
		}

		Active = false;
		callback.Invoke();
	}
}

[Serializable]
public class WaveAttack
{
	public int SpawnCount;
	public float Hitpoints = 100;
	public float Speed = 1;
	public AttackParams Params = new AttackParams();
}