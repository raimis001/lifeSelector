using System;
using System.Collections.Generic;
using UnityEngine;

public enum TagKind
{
	Monster,
	Enemy,
	Cow
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
		return delta/delta.magnitude;
	}

	public static int ToInt(this float value)
	{
		return (int) value;
	}

	public static GameObject FindClosestObject(Vector3 position, string[] tags, float radius = Mathf.Infinity)
	{
		GameObject closest = null;
		float distance = Mathf.Infinity;
		float border = radius*radius;

		foreach (string tag in tags)
		{
			GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
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
		}
		return closest;
	}

	public static T FindClosestTarget<T>(Vector3 position, float distance) where T : BaseObject
	{
		T[] objs = GameObject.FindObjectsOfType<T>();
		T closest = null;
		float dist = Mathf.Infinity;

		foreach (var obj in objs)
		{
			float d = Vector3.Distance(position, obj.Position);

			if (d <= distance && d < dist)
			{
				closest = obj;
				dist = d;
			}
		}

		return closest;
	}

	public static IEnumerable<Enemy> EnemyInRange(Vector3 position, float distance)
	{
		Enemy[] enemys = GameObject.FindObjectsOfType<Enemy>();
		foreach (Enemy enemy in enemys)
		{
			if (Vector3.Distance(position, enemy.Position) <= distance)
			{
				yield return enemy;
			}
		}
	}



	public static Enemy FindClosestEnemy(Vector3 position, float distance)
	{
		float dist = Mathf.Infinity;
		Enemy closest = null;
		foreach (Enemy enemy in EnemyInRange(position, distance))
		{
			float d = Vector3.Distance(position, enemy.Position);
			if (d < dist)
			{
				dist = d;
				closest = enemy;
			}
		}

		return closest;
	}

	public static IEnumerable<GeneticKind> GetGeneticKinds()
	{
		Array enumstrings = Enum.GetValues(typeof(GeneticKind));
		foreach (var enumstring in enumstrings)
		{
			GeneticKind kind = (GeneticKind)enumstring;
			yield return kind;
		}

	}

	public static Color CombineColors(params Color[] aColors)
	{
		Color result = new Color(0, 0, 0, 0);
		foreach (Color c in aColors)
		{
			result += c;
		}
		result /= aColors.Length;
		return result;
	}
}

