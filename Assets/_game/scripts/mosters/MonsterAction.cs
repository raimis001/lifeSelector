using UnityEngine;
using System.Collections;

public class MonsterAction : MonoBehaviour
{
	public BaseObject Actor;

	protected Monster _monster;

	protected virtual void Start()
	{
		_monster = GetComponent<Monster>();
	}

	protected virtual void Update()
	{
		if (!Actor)
		{
			Destroy(this);
		}
	}
}
