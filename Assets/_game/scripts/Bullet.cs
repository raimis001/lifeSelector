using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

	public Vector3 Target;
	private Rigidbody _rigidbody;

	private string Tag;

	public static void Create(BaseObject baseObject, BaseObject targetObject, string tag)
	{
		GameObject obj = Instantiate(The.GameLogic.BulletPrefab);
		obj.transform.position = baseObject.Position + (targetObject.Position - baseObject.Position).normalized * 0.7f + new Vector3(0,0.5f,0);

		Bullet bullet = obj.GetComponent<Bullet>();
		bullet.Target = targetObject.Position;
		bullet.Tag = tag;
	}

	// Use this for initialization
	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
		Vector3 st = (Target - transform.position).normalized;
		st.y = 0;
		_rigidbody.AddForce(st* _rigidbody.mass * 100);
	}

	// Update is called once per frame
	private void Update()
	{

	}

	private void OnCollisionEnter(Collision collision)
	{
		//Debug.Log(collision.gameObject.name);

		if (collision.gameObject.tag.Equals(Tag))
		{
			Destroy(gameObject);
		}


	}

}
