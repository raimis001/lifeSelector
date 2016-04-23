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
			_selectedCow = value;
			Gui.SetCow(_selectedCow);
		}
	}

	// Use this for initialization
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

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonUp(1) && _selectedCow != null)
		{
			_selectedCow = null;
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

		_selectedCow.Destination = position;
	}

	public void AddMonster()
	{
		Debug.Log("Add");



		GameObject obj = Instantiate(MonsterPrefab);
		obj.name = "monster";
		obj.transform.SetParent(MonsterObject.transform);
		obj.transform.position = SpawnPoint ? SpawnPoint.transform.position : Vector3.zero;

		Monster monster =  obj.GetComponent<Monster>();

	}

	public void StartSpawn()
	{
		if (_spawning) return;

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
}
