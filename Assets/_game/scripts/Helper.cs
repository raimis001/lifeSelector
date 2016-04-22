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

}
