using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class Monster : BaseObject
{

	[HideInInspector]
	public BaseObject Actor;

	[HideInInspector]
	public BaseObject Enemy;

	private LineRenderer _line;

	private BulletManager _bulletManager;

	public static void Create( GameObject spawnPoint)
	{
		GameObject obj = Instantiate(The.GameLogic.MonsterPrefab);
		obj.name = "monster";
		obj.transform.SetParent(The.GameLogic.MonsterHolder.transform);
		obj.transform.position = spawnPoint ? spawnPoint.transform.position : Vector3.zero;

		//Monster monster = obj.GetComponent<Monster>();
	}

	protected override void Start()
	{
		base.Start();
		_line = gameObject.AddComponent<LineRenderer>();
		//Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Texture"));
		_line.material = new Material(Shader.Find("Particles/Additive"));
		_line.SetColors(Color.blue, Color.black);
		_line.SetWidth(0.1f, 0.1f);

		_bulletManager = gameObject.AddComponent<BulletManager>();

	}

	protected override void Update()
	{

		if (Enemy && Actor is Cow)
		{

			_line.SetPosition(0, transform.position);
			_line.SetPosition(1, Enemy.transform.position);
			if (Distance(Enemy) > ((Cow) Actor).Attack.AttackRange)
			{
				Log(Distance(Enemy));
				MoveToPosition(Enemy.Position);
			}
			else
			{
				_bulletManager.Shot(this, Enemy);
				Stop();
			}
			return;
		}

		base.Update();

		if (Actor)
		{
			
			_line.SetPosition(0,transform.position);
			_line.SetPosition(1, Actor.transform.position);
		}

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
}
