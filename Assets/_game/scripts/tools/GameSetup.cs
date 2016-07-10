using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class AttackParams
{
	public float AttackRange = 1f;
	public float AttackDamage = 1f;
	public float AttackSpeed = 0.1f;

	public float DefenceDamage = 1f;
}

[Serializable]
public class GeneticParams
{
	public float MoveSpeed;
	public float DetectRange;

	public float Hitpoints;

	public float AttackRange;
	public float AttackSpeed;
	public float AttackDamage;

	public float HarvestSpeed;

	public float SpawnMonsters;
	public float SpawnTime;
	public float MonsterCount;
	public float MonsterHitpoints;

	public float MonsterSpeed;

	public float GeneticValue(List<GeneticProps> genetic, GeneticKind kind)
	{
		float result = 0;

		foreach (GeneticProps props in genetic)
		{
			foreach (GeneticValue value in props.Genetics)
			{
				if (value.Kind == kind)
				{
					result += GeneticValue(kind);
				}
			}
		}

		return result;
	}

	public float GeneticValue(GeneticKind kind)
	{
		float result = 0;
		switch (kind)
		{
			case GeneticKind.MoveSpeed:
				result = MoveSpeed;
				break;
			case GeneticKind.DetectRange:
				result = DetectRange;
				break;
			case GeneticKind.Hitpoints:
				result = Hitpoints;
				break;
			case GeneticKind.AttackRange:
				result = AttackRange;
				break;
			case GeneticKind.AttackSpeed:
				result = AttackSpeed;
				break;
			case GeneticKind.AttackDamage:
				result = AttackDamage;
				break;
			case GeneticKind.HarvestSpeed:
				result = HarvestSpeed;
				break;
			case GeneticKind.SpawnMonsters:
				result = SpawnMonsters;
				break;
			case GeneticKind.SpawnTime:
				result = SpawnTime;
				break;
			case GeneticKind.MonsterCount:
				result = MonsterCount;
				break;
			case GeneticKind.MonsterHitpoints:
				result = MonsterHitpoints;
				break;
			case GeneticKind.MonsterSpeed:
				result = MonsterSpeed;
				break;
		}
		return result;
	}

	public void AddGeneticValue(GeneticParams value)
	{
		Reset();
		foreach (GeneticKind kind in Helper.GetGeneticKinds())
		{
			SetGeneticValue(kind, value.GeneticValue(kind));
		}

	}

	public void AddGeneticValue(GeneticValue value)
	{
		AddGeneticValue(value.Kind, value.Value);
	}
	public void AddGeneticValue(GeneticKind kind, float value)
	{
		SetGeneticValue(kind, GeneticValue(kind) + value);
	}

	public void SetGeneticValue(GeneticKind kind, float value)
	{
		switch (kind)
		{
			case GeneticKind.MoveSpeed:
				MoveSpeed = value;
				break;
			case GeneticKind.DetectRange:
				DetectRange = value;
				break;
			case GeneticKind.Hitpoints:
				Hitpoints = value;
				break;
			case GeneticKind.AttackRange:
				AttackRange = value;
				break;
			case GeneticKind.AttackSpeed:
				AttackSpeed = value;
				break;
			case GeneticKind.AttackDamage:
				AttackDamage = value;
				break;
			case GeneticKind.HarvestSpeed:
				HarvestSpeed = value;
				break;
			case GeneticKind.SpawnMonsters:
				SpawnMonsters = value;
				break;
			case GeneticKind.SpawnTime:
				SpawnTime = value;
				break;
			case GeneticKind.MonsterCount:
				MonsterCount = value;
				break;
			case GeneticKind.MonsterHitpoints:
				MonsterHitpoints = value;
				break;
			case GeneticKind.MonsterSpeed:
				MonsterSpeed = value;
				break;
		}
	}

	public void AddGenetic(List<GeneticProps> genetic)
	{
		Reset();
		foreach (GeneticProps props in genetic)
		{
			AddGenetic(props);
		}
	}

	public void AddGenetic(GeneticProps props)
	{
		foreach (GeneticValue value in props.Genetics)
		{
			AddGeneticValue(value);
		}
	}

	public void Reset()
	{
		MoveSpeed = 0;
		DetectRange = 0;

		Hitpoints = 0;

		AttackRange = 0;
		AttackSpeed = 0;
		AttackDamage = 0;

		HarvestSpeed = 0;

		SpawnMonsters = 0;
		SpawnTime = 0;
	}
}

public enum GeneticKind
{
	MoveSpeed,
	DetectRange,

	Hitpoints,

	AttackRange,
	AttackDamage,
	AttackSpeed,

	HarvestSpeed,

	SpawnMonsters,
	SpawnTime,
	MonsterCount,
	MonsterHitpoints,

	MonsterSpeed
}

[Serializable]
public class GeneticValue
{
	public GeneticKind Kind;
	public float Value;
}

[Serializable]
public class GeneticProps
{
	public string Name;
	public string Description;
	public List<GeneticValue> Genetics;
}

public class GameSetup : ScriptableObject
{

	public GeneticParams DefaultCow = new GeneticParams();
	public List<GeneticProps> GeneticlList;

	public GeneticProps GetGenetic(int id)
	{
		return GeneticlList[id];
	}

}
