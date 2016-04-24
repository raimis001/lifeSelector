using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cow : BaseObject
{

	public GameObject RadarPlane;

	public AttackParams Attack;

	public int MaxMonsterCount = 2;
	public int MonsterCount
	{
		get { return _monsters.Count; }
	}

	private readonly List<Monster> _monsters = new List<Monster>();


	// Use this for initialization
	protected override void Start()
	{
		base.Start();
		AllowRandomMove = false;
	}

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();

		if (RadarPlane)
		{
			RadarPlane.transform.localScale = new Vector3(Attack.RadarRange * 0.5f,1, Attack.RadarRange * 0.5f);

			Vector3 plane = transform.position;
			plane.y = RadarPlane.transform.position.y;
			RadarPlane.transform.position = plane;
		}

		//Enemy[] enemys = FindObjectsOfType<Enemy>();
		//Log(Distance(enemys[0]));
		
	}

	protected override void OnMouseClick()
	{
		GameLogic.SelectedCow = this;
	}

	public void Select()
	{
		if (RadarPlane) RadarPlane.SetActive(true);
	}

	public void Deselect()
	{
		if (RadarPlane) RadarPlane.SetActive(false);
	}

	public void CallMonsters()
	{
		Monster[] monsters = FindObjectsOfType<Monster>();
		Debug.Log(monsters.Length);

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
