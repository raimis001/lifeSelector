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
				ApplayGenetic();
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

	public void ApplayGenetic()
	{
		if (!Parent) return;
		Genetic.AddGeneticValue(Parent.CurrentParams);

		MaxHitpoints = Genetic.MonsterHitpoints;
		if (Hitpoints > MaxHitpoints) Hitpoints = MaxHitpoints;
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
		DetectAttack();

		_line.Enabled = Parent || AttackObject;
		if (_line.Enabled)
		{
			_line.ToPoint = AttackObject ? AttackObject.transform : Parent.transform;
		}

	}

	private BaseObject AttackObject;
	private BulletManager _bulletManager;

	void DetectAttack()
	{
		if (Genetic.AttackDamage <= 0) return;
		if (!Parent) return;

		if (!AttackObject)
		{
			AttackObject = Helper.FindClosestEnemy(Parent.Position, Parent.CurrentParams.DetectRange);
		}

		if (!AttackObject) return;

		if (Parent.Distance(AttackObject) > Parent.CurrentParams.DetectRange)
		{
			AttackObject = null;
			return;
		}

		if (Distance(AttackObject) > Genetic.AttackRange)
		{
			MoveToPosition(AttackObject.Position);
			return;
		}

		if (!_bulletManager)
		{
			_bulletManager = gameObject.AddComponent<BulletManager>();
		}
		if (!_bulletManager) return;

		_bulletManager.Shot(this, AttackObject, TagKind.Monster, 
			new AttackParams()
			{
				AttackDamage = Genetic.AttackDamage,
				AttackSpeed = Genetic.AttackSpeed,
				AttackRange = Genetic.AttackRange
			});
		Stop();


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
