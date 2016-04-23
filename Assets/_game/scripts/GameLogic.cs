using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{

	public GameObject MonsterObject;
	public GameObject MonsterPrefab;
	public GameObject SpawnPoint;

	private bool _spawning;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void AddMonster()
	{
		Debug.Log("Add");



		GameObject obj = Instantiate(MonsterPrefab);
		obj.name = "monster";
		obj.transform.SetParent(MonsterObject.transform);
		obj.transform.position = SpawnPoint ? SpawnPoint.transform.position : new Vector3(0,0.5f,0);

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
