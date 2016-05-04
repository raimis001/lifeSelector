using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{

	public GameObject SpawnPoint;
	public GameObject EnemyPoint;

	public GameObject Cursor;
	public Radar Radar;

	[Header("Holders")]
	public GameObject MonsterHolder;
	public GameObject BulletHolder;
	public GameObject EnemyHolder;

	[Header("Prefabs")]
	public GameObject MonsterPrefab;
	public GameObject EnemyPrefab;
	public GameObject BulletPrefab;
	public GameObject DestroyParticle;

	private bool _spawning;

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

	#region INIT
	void Awake()
	{
		The.GameLogic = this;
	}
	void Start()
	{

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

	public void DoExplosion(Vector3 position)
	{
		if (!DestroyParticle) return;

		Instantiate(DestroyParticle, position, Quaternion.identity);

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
