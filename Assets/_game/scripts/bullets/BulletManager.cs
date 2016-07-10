using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour
{

	public float ShotTime = 2;

	private bool _shoting;
	private float _shotTime;

	public void Shot(MoveObject parent, BaseObject target, TagKind tagKind, AttackParams attack)
	{
		if (_shoting) return;

		ShotTime = attack.AttackSpeed;

		_shotTime = ShotTime;
		_shoting = true;

		Bullet.Create(parent, target, tagKind, attack);

		StartCoroutine(DoShot());
	}

	IEnumerator DoShot()
	{
		while (_shotTime > 0)
		{
			_shotTime -= Time.deltaTime;
			yield return null;
		}
		_shoting = false;
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
