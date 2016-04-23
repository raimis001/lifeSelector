using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{

	public float MaxHitpoints = 100;
	public float Hitpoints;
	public Cow CurrentCow;


	public float Live = 200;
	public float LiveModiefier = 1;

	// Use this for initialization
	void Start () {
		Hitpoints = MaxHitpoints;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Live -= LiveModiefier*Time.deltaTime;
		if (!CheckIsLive())
		{
			Destroy(gameObject);
			return;
		}

	}

	bool CheckIsLive()
	{
		return Live > 0 && Hitpoints > 0;
	}

	void RandomizeMove()
	{

		Stimulus[] stimuli = GameObject.FindObjectsOfType<Stimulus>();
		foreach (Stimulus stimulus in stimuli)
		{
			if (Vector3.Distance(transform.position, stimulus.transform.position) < stimulus.Radiuss)
			{
				//AddForce(stimulus);
			}
		}
	}


	void Log(object message)
	{
		Debug.Log(message);
	}
}
