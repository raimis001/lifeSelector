using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour
{
	public GameSetup Setup;

	[Header("Spawn points")]
	public GameObject SpawnPoint;
	public GameObject EnemyPoint;

	[Header("Holders")]
	public GameObject MonsterHolder;
	public GameObject BulletHolder;
	public GameObject EnemyHolder;
	public GameObject CowHolder;

	[Header("Prefabs")]
	public GameObject MonsterPrefab;
	public GameObject EnemyPrefab;
	public GameObject BulletPrefab;
	public GameObject DestroyParticle;
	public GameObject CowPrefab;

	[Header("Effects")]
	public GameObject DieEffect;
	public GameObject CollideMonsterEffect;
	public GameObject SpawnCowEffect;

	[Header("Interface")]
	public GameObject Cursor;
	public Radar Radar;

	[HideInInspector]
	public Stimulus[] StimulusList;

	private bool _spawning;

	public static float Sula = 0;

	private static Cow _selectedCow;
	public static Cow SelectedCow
	{
		get { return _selectedCow; }
		set
		{
			if (_selectedCow) _selectedCow.Deselect();

			_selectedCow = value;
			Gui.SetCow(_selectedCow);

			if (_selectedCow) _selectedCow.Select();

		}
	}

	public static GameSetup GameSetup
	{
		get { return The.GameLogic.Setup; }
	}

	#region INIT
	void Awake()
	{
		The.GameLogic = this;
	}
	void Start()
	{
		StimulusList = FindObjectsOfType<Stimulus>();
		for (int i = 0; i < StimulusList.Length; i++)
		{
			StimulusList[i].StimulusId = i;
		}
	}
	void OnEnable()
	{
		TerrainManager.OnTerrainClick += OnTerrainClick;
	}
	void OnDisable()
	{
		TerrainManager.OnTerrainClick -= OnTerrainClick;
	}
	#endregion

	void Update()
	{
		if (Input.GetMouseButtonUp(1) && _selectedCow != null)
		{
			SelectedCow = null;
		}
	}

	public void DoExplosion(Vector3 position, GameObject effect = null)
	{
		if (!effect && !DestroyParticle) return;
		Instantiate(effect ? effect : DestroyParticle, position, Quaternion.identity);
	}

	public void OnTerrainClick(Vector3 position)
	{
		if (_selectedCow == null)
		{
			return;
		}

		position.y = 0.05f;
		Cursor.transform.position = position;
		Cursor.SetActive(true);

		_selectedCow.MoveToPosition(position);
	}

	#region MONSTERS
	public void AddMonster()
	{
		Monster.Create(SpawnPoint);
	}

	public void StartSpawn()
	{
		if (_spawning) return;

		AddMonster();
		_spawning = true;
		StartCoroutine(SpawnMonster());
	}

	public void StopSpawn()
	{
		_spawning = false;
	}

	IEnumerator SpawnMonster(bool monster = true)
	{
		float spawnTime = 0.1f;
		while (_spawning)
		{
			if (spawnTime <= 0)
			{
				if (monster) AddMonster(); else AddEnemy();
				spawnTime = 0.1f;
				continue;
			}
			spawnTime -= Time.deltaTime;
			yield return null;
		}
	}

	public void CallMonsters()
	{
		if (!_selectedCow)
		{
			return;
		}

		_selectedCow.CallMonsters();
	}

	public void ReleaseMonsters()
	{
		if (!_selectedCow)
		{
			return;
		}

		_selectedCow.ReleaseMonsters();
	}
	#endregion

	#region ENEMIES
	public void AddEnemy()
	{
		Enemy.Create(EnemyPoint.transform);
	}

	public void StartSpawnEnemys()
	{
		if (_spawning) return;

		AddEnemy();
		_spawning = true;
		StartCoroutine(SpawnMonster(false));
	}


	#endregion
}
