using UnityEngine;
using System.Collections;

public class BaseObject : MonoBehaviour
{

	public Vector3 Position {
		get {
			Vector3 pos = transform.position;
			pos.y = 0;
			return pos;
		}
	}

}
