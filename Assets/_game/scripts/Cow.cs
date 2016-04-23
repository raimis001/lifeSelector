using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cow : MonoBehaviour
{

	public float MaxHitpoints = 100;
	public float Hitpoints;

	public float Hp
	{
		get { return Hitpoints/MaxHitpoints; }
	}

	private bool MoveToDestination;
	private Vector3 _destination;
	public Vector3 Destination
	{
		get { return _destination; }
		set
		{
			_destination = value;

			Mover m = GetComponent<Mover>();
			if (m) m.MoveToPosition(_destination);
		}
	}

	public float MoveSpeed = 1;

	public AttackParams Attack;

	public int MaxMonsterCount = 2;
	public int MonsterCount
	{
		get { return _monsters.Count; }
	}

	private List<Monster> _monsters = new List<Monster>();


	// Use this for initialization
	void Start()
	{
		Hitpoints = MaxHitpoints;
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnMouseUp()
	{
		Debug.Log("You click a cow!");
		GameLogic.SelectedCow = this;

	}

	public void CallMonsters()
	{
		
	}

	public void ReleaseMonsters()
	{
		foreach (Monster monster in _monsters)
		{
			monster.CurrentCow = null;
		}

		_monsters.Clear();
	}

	public bool AddMonster(Monster monster)
	{
		if (MonsterCount >= MaxMonsterCount || monster.CurrentCow != null)
		{
			return false;
		}

		_monsters.Add(monster);
		monster.CurrentCow = this;

		return true;

	}
}
