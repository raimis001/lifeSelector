using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class Monster : BaseObject
{

	private Cow _actor;
	public Cow Actor
	{
		get { return _actor; }
		set
		{
			if (_actor)
			{
				_actor.Monsters.Remove(this);
			}
			_actor = value;
			if (_actor)
			{
				_actor.Monsters.Add(this);
			}
			else
			{
				MotherActivity[] mothers = FindObjectsOfType<MotherActivity>();

				foreach (MotherActivity mother in mothers)
				{
					if (mother.Cow.AllowMonsters())
					{
						_actor = mother.Cow;
						_actor.Monsters.Add(this);
						break;
					}
				}

			}

			Enemy = null;

		}
	}

	[HideInInspector]
	public BaseObject Enemy;

	private LineRenderer _line;

	private BulletManager _bulletManager;


	public static Monster Create( GameObject spawnPoint)
	{
		return Create(spawnPoint ? spawnPoint.transform.position : Vector3.zero);
	}
	public static Monster Create(Vector3 spawnPoint)
	{
		GameObject obj = Instantiate(The.GameLogic.MonsterPrefab);
		obj.transform.SetParent(The.GameLogic.MonsterHolder.transform);
		obj.transform.position = spawnPoint;

		return obj.GetComponent<Monster>();
	}

	void Awake()
	{
		name = "monster";
	}

	protected override void Start()
	{
		base.Start();
		_line = gameObject.AddComponent<LineRenderer>();
		_line.material = new Material(Shader.Find("Particles/Additive"));
		_line.SetColors(Color.blue, Color.black);
		_line.SetWidth(0.1f, 0.1f);

		_bulletManager = gameObject.AddComponent<BulletManager>();

	}

	protected override void Update()
	{

		base.Update();
		if (!Actor && Enemy)
		{
			Enemy = null;
		}

		if (Actor && Enemy && Actor.Activitie<AttackActivity>())
		{
			AttackParams attack = Actor.Activitie<AttackActivity>().Attack;

			_line.enabled = true;
			_line.SetPosition(0, transform.position);
			_line.SetPosition(1, Enemy.transform.position);

			if (Distance(Enemy) > attack.AttackRange)
			{
				//Log(Distance(Enemy));
				MoveToPosition(Enemy.Position);
			}
			else
			{
				_bulletManager.Shot(this, Enemy, "Enemy", attack);
				Stop();
			}
			return;
		}


		if (Actor)
		{
			_line.enabled = true;
			_line.SetPosition(0, transform.position);
			_line.SetPosition(1, Actor.transform.position);
		}
		else
		{
			_line.enabled = false;
		}

	}

	protected override void BeforeDestroy()
	{
		//Debug.Log("Monster destroy!");
		if (Actor)
		{
			Actor.Monsters.Remove(this);
		}
		_actor = null;
		Enemy = null;
	}

	protected override void RandomizeMove()
	{
		base.RandomizeMove();
		if (Actor)
		{
			float distance = Distance(Actor);
			if (distance > 3)
			{
				AddForce(Actor.transform, 100*(1 - 1/distance));
			}
		}
	}

	public bool IsFree()
	{
		return !Actor || Actor.GetComponent<MotherActivity>();
	}

}
