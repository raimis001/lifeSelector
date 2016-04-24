using UnityEngine;
using System.Collections;

public class Monster : BaseObject
{

	[HideInInspector]
	public Cow CurrentCow;
	
	public static void Create(GameObject monsterPrefab,GameObject monsterObject,  GameObject spawnPoint)
	{
		GameObject obj = Instantiate(monsterPrefab);
		obj.name = "monster";
		obj.transform.SetParent(monsterObject.transform);
		obj.transform.position = spawnPoint ? spawnPoint.transform.position : Vector3.zero;

		//Monster monster = obj.GetComponent<Monster>();
	}

}
