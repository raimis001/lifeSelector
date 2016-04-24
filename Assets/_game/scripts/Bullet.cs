using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

	public Vector3 Target;
	private Rigidbody _rigidbody;

	private string _tag;
	private float _time = 2;
	private float _damage;

	public static void Create(BaseObject baseObject, BaseObject targetObject, string tag, AttackParams attack)
	{
		GameObject obj = Instantiate(The.GameLogic.BulletPrefab);
		obj.transform.position = baseObject.Position + (targetObject.Position - baseObject.Position).normalized * 0.7f + new Vector3(0,0.5f,0);

		Bullet bullet = obj.GetComponent<Bullet>();
		bullet.Target = targetObject.Position;
		bullet._tag = tag;
		bullet._damage = attack.AttackDamage;
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
			BaseObject enemy = collision.gameObject.GetComponent<Enemy>();
			if (enemy)
			{
				enemy.DoDamage(_damage);
			}

			Destroy(gameObject);
		}


	}

}
