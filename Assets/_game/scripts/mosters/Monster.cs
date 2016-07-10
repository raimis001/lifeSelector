using UnityEngine;

public class Monster : MoveObject
{
	public GeneticParams Genetic = new GeneticParams();

	private Cow _parent;
	public Cow Parent

	{
		get { return _parent; }
		set
		{
			if (_parent)
			{
				_parent.Monsters.Remove(this);
			}
			_parent = value;

			if (_parent)
			{
				_parent.Monsters.Add(this);
				Genetic.AddGenetic(_parent.Genetics);
			}
			else
			{
				//TODO: find free mothers
				Genetic.Reset();
			}
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
		_line.FromPoint = transform;

		AtackTag = "monster";
	}

	protected override void Update()
	{

		base.Update();

		MonsterAction action = GetComponent<MonsterAction>();
		if (!action && !Parent)
		{
			_line.Enabled = false;
			return;
		}
		_line.Enabled = true;
		_line.ToPoint = (action && action.Actor) ? action.Actor.transform : Parent.transform;

	}

	protected override void BeforeDestroy()
	{
		//Debug.Log("Monster destroy!");
		Parent = null;
	}

	protected override void RandomizeMove()
	{
		base.RandomizeMove();
		if (Parent)
		{
			float distance = Distance(Parent);
			if (distance > 3)
			{
				AddForce(Parent.transform, 100*(1 - 1/distance));
			}
		}
	}
	
	public bool IsFree()
	{
		return !Parent || Parent.IsMother();
	}

}
