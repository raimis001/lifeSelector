using UnityEngine;
using System.Collections;

public class CowActivity : MonoBehaviour
{
	public int Level = 1;

	[HideInInspector]
	public Cow Cow;


	// Use this for initialization
	protected virtual void Start()
	{
		Cow = GetComponent<Cow>();
		if (!Cow)
		{
			Debug.LogError("Activity without cow!!!");
		}
	}

	// Update is called once per frame
	protected virtual void Update()
	{

	}

	public virtual void Select()
	{
		
	}

	public virtual void Deselect()
	{
		
	}
}
