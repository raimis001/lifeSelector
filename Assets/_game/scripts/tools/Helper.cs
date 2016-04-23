using UnityEngine;

public static class Helper 
{
	
	public static Vector3 Random(this Vector3 vector)
	{
		Vector3 v = new Vector3();
		v.x = UnityEngine.Random.Range(-1f, 1f);
		v.y = UnityEngine.Random.Range(-1f, 1f);
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


}
