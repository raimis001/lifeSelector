using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{

	public GameObject MonsterObject;
	public GameObject MonsterPrefab;
	public GameObject SpawnPoint;

	public Terrain Terrain;
	public GameObject Cursor;

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
		Monster.Create(MonsterPrefab, MonsterObject, SpawnPoint);
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

	IEnumerator SpawnMonster()
	{
		float spawnTime = 0.1f;
		while (_spawning)
		{
			if (spawnTime <= 0)
			{
				AddMonster();
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
	#endregion

}
