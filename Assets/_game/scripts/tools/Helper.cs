using System;
using UnityEngine;

public enum TagKind
{
	Monster,
	Enemy
}

public static class Helper 
{
	
	public static Vector3 Random(this Vector3 vector, bool yzero = false)
	{
		Vector3 v = new Vector3();
		v.x = UnityEngine.Random.Range(-1f, 1f);
		v.y = yzero ? 0 : UnityEngine.Random.Range(-1f, 1f);
		v.z = UnityEngine.Random.Range(-1f, 1f);
		//Debug.Log(v);
		
		return v;
	}

	public static float Distance(this Vector3 vector, Vector3 destination)
	{
		Vector3 delta = vector - destination;
		return delta.magnitude;
	}

	public static Vector3 Direction(this Vector3 vector, Vector3 destination)
	{
		Vector3 delta = vector - destination;
		//Vector3 dir = delta/delta.magnitude;
		return delta/ delta.magnitude;
	}

	public static int ToInt(this float value)
	{
		return (int)value;
	}
	public static GameObject FindClosestObject(Vector3 position, string tag, float radius = Mathf.Infinity)
	{
		GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
		GameObject closest = null;
		float distance = Mathf.Infinity;
		float border = radius * radius;

		foreach (GameObject go in gos)
		{
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < border && curDistance < distance)
			{
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}


}

public class DefenceParams
{
	public float Damage;
}

[Serializable]
public class AttackParams
{
	public float AttackRange = 1f;
	public float AttackDamage = 1f;
	public float AttackSpeed = 0.1f;
	public float DefenceDamage = 1f;
}
