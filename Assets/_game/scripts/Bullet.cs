using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

	public Vector3 Target;
	private Rigidbody _rigidbody;

	private string _tag;
	private float _time = 2;
	public float _damage;

	public static void Create(BaseObject baseObject, BaseObject targetObject, string tag, AttackParams attack)
	{
		GameObject obj = Instantiate(The.GameLogic.BulletPrefab);
		obj.transform.position = baseObject.Position + (targetObject.Position - baseObject.Position).normalized * 0.7f + new Vector3(0,0.5f,0);

		Bullet bullet = obj.GetComponent<Bullet>();
		bullet.Target = targetObject.Position;
		bullet._tag = tag;
		bullet.tag = tag.Equals(TagKind.Enemy.ToString()) ? TagKind.Monster.ToString() : TagKind.Enemy.ToString();
		bullet._damage = attack.AttackDamage;

		GameObject[] alies = GameObject.FindGameObjectsWithTag(bullet.tag);

		foreach (GameObject aly in alies)
		{
			Physics.IgnoreCollision(bullet.GetComponent<Collider>(),aly.GetComponent<Collider>());
		}

	}

	// Use this for initialization
	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
		Vector3 st = (Target - transform.position).normalized;
		st.y = 0;
		_rigidbody.AddForce(st* _rigidbody.mass * 200);
	}

	// Update is called once per frame
	private void Update()
	{
		_time -= Time.deltaTime;
		if (_time <= 0)
		{
			Destroy(gameObject);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		//Debug.Log(collision.gameObject.name);

		if (collision.gameObject.tag.Equals(_tag))
		{
			BaseObject enemy = collision.gameObject.GetComponent<BaseObject>();
			if (enemy)
			{
				//Debug.Log("Bullte damage to object:" + _tag + " with damage:" + _damage);
				enemy.DoDamage(_damage);
			}

			Destroy(gameObject);
		}


	}

}
