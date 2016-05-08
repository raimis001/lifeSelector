using UnityEngine;

public class Monster : MoveObject
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
			RemoveAction();

		}
	}

	private LineDrawer _line;

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
		_line = gameObject.AddComponent<LineDrawer>();
	}

	protected override void Update()
	{

		base.Update();

		MonsterAction action = GetComponent<MonsterAction>();
		if (!action && !Actor)
		{
			_line.enabled = false;
			return;
		}

		_line.Enabled = true;
		if (action && action.Actor)
		{
			_line.FromPoint = transform;
			_line.ToPoint = action.Actor.transform;
		}
		else
		{
			_line.FromPoint = transform;
			_line.ToPoint = Actor.transform;
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
		RemoveAction();
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
	public T AddAction<T>(BaseObject enemy) where T : MonsterAction
	{
		T a = GetComponent<T>();
		if (!a)
		{
			a = gameObject.AddComponent<T>();
		}

		a.Actor = enemy;
		return a;
	}

	public void RemoveAction()
	{
		MonsterAction action = GetComponent<MonsterAction>();
		if (action)
		{
			Destroy(action);
		}
	}

	public bool IsFree()
	{
		return !Actor || Actor.GetComponent<MotherActivity>();
	}

}
