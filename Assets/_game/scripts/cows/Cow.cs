using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Cow : MoveObject
{
	public int Level = 1;

	public float RadarRange = 3;

	[Header("Monsters")]
	public int MaxMonsterCount = 2;
	public int MonsterHitpoints = 100;

	public List<Monster> Monsters = new List<Monster>();
	public int MonsterCount {
		get { return Monsters.Count; }
	}

	[Header("Genetic")]
	public List<GeneticProps> Genetics = new List<GeneticProps>();
	public GeneticParams Params = new GeneticParams();

	[HideInInspector]
	public GeneticParams CurrentParams = new GeneticParams();

	private readonly List<int> _usedStimulus = new List<int>();

	private float spawnTime;

	public static Cow Create(Vector3 position)
	{
		GameObject obj = Instantiate(The.GameLogic.CowPrefab);
		obj.transform.SetParent(The.GameLogic.CowHolder.transform);
		obj.transform.position = position;

		The.GameLogic.DoExplosion(position, The.GameLogic.SpawnCowEffect);

		return obj.GetComponent<Cow>();
	}

	protected override void Start()
	{
		base.Start();
		DragObject = true;
		AllowRandomMove = false;
		AtackTag = "monster";

		RecalculateGenetic();
	}

	public void OnDestroy()
	{
		//ReleaseMonsters();
		//The.GameLogic.Radar.Hide();
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

			monster.Parent = this;
		}
	}

	public void ReleaseMonsters()
	{
		while (Monsters.Count > 0)
		{
			Monster monster = Monsters[0];
			Monsters.Remove(monster);
			monster.Parent = null;
		}

		Monsters.Clear();
	}

	public bool AllowMonsters()
	{
		return MaxMonsterCount > MonsterCount;
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

	protected override void EndDrag(MoveObject otherObject)
	{
		Cow otherCow = (Cow) otherObject;
		if (!otherCow) return;
		if (otherCow.Level != 1 || Level != 1) return;
		if (Genetics.Count < 2 || otherCow.Genetics.Count < 2) return;

		List<GeneticProps> newList = new List<GeneticProps>();

		foreach (GeneticProps props in Genetics)
		{
			newList.Add(props);
		}
		foreach (GeneticProps props in otherCow.Genetics)
		{
			newList.Add(props);
		}

		newList.Shuffle();

		Cow newCow = Cow.Create(transform.position + Quaternion.Euler(0, Random.value * 360f, 0) * new Vector3(Random.value, 0, Random.value) * 4f);
		if (newCow)
		{
			newCow.Level = Level + 1;
			foreach (GeneticProps props in newList)
			{
				newCow.Genetics.Add(props);
			}
			newCow.RecalculateGenetic();
		}

	}

	protected override void Update()
	{
		base.Update();

		if (MonsterCount < MaxMonsterCount && CurrentParams.SpawnMonsters > 0)
		{
			spawnTime -= Time.deltaTime;
			if (spawnTime <= 0)
			{
				//TODO: spawn monsters
				Monster monster = Monster.Create(transform.position + new Vector3().Random(true) * RadarRange + Vector3.down * 0.5f);
				monster.Parent = this;
				spawnTime = CurrentParams.SpawnTime;
			}
		}

	}

	public bool IsMother()
	{
		return CurrentParams.SpawnMonsters > 0;
	}

	#region Stimulus triggers
	private IEnumerator _stimulusCoroutine;
	public void OnTriggerEnter(Collider other)
	{
		Stimulus stimulus = other.GetComponentInParent<Stimulus>();
		if (!stimulus) return;
		if (_usedStimulus.Contains(stimulus.StimulusId)) return;
		if (Genetics.Count >= Level + 1) return;

		//Debug.Log("Start stimulus");
		_stimulusCoroutine = UseStimulus(stimulus);
		StartCoroutine(_stimulusCoroutine);

	}
	public void OnTriggerExit(Collider other)
	{
		Stimulus stimulus = other.GetComponentInParent<Stimulus>();
		if (!stimulus) return;

		if (_stimulusCoroutine != null)
		{
			StopCoroutine(_stimulusCoroutine);
			_stimulusCoroutine = null;
		}
	}

	IEnumerator UseStimulus(Stimulus stimulus)
	{
		float wait = stimulus.ChargeTime;
		while (wait > 0)
		{
			wait -= Time.deltaTime;
			yield return null;
		}

		_stimulusCoroutine = null;

		_usedStimulus.Add(stimulus.StimulusId);

		//TODO: jāpievieno īpašības
		Genetics.Add(GameLogic.GameSetup.GetGenetic(stimulus.RandomGenetic()));
		Params.AddGenetic(Genetics);
		RecalculateGenetic();
	}

	public void RecalculateGenetic()
	{
		CurrentParams.Reset();
		foreach (GeneticKind kind in Helper.GetGeneticKinds())
		{
			CurrentParams.SetGeneticValue(kind, GameLogic.GameSetup.DefaultCow.GeneticValue(kind) + Params.GeneticValue(kind));
		}

		MovingSpeed = CurrentParams.MoveSpeed;
		RadarRange = CurrentParams.DetectRange;
		spawnTime = CurrentParams.SpawnTime;
		MaxMonsterCount = (int)CurrentParams.MonsterCount;

		MaxHitpoints = CurrentParams.Hitpoints;
		if (Hitpoints > MaxHitpoints)
		{
			Hitpoints = MaxHitpoints;
		}
		//Debug.Log("Hitp:" + MaxHitpoints);

		//TODO: jāapdeito monstri

		foreach (Monster monster in Monsters)
		{
			monster.Genetic.AddGeneticValue(CurrentParams);
		}

	}

	#endregion


}
