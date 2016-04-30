using UnityEngine;
using System.Collections;

public abstract class guiProgress : MonoBehaviour
{

	private float _value;

	public float Value
	{
		get { return _value; }
		set
		{
			_value = value;
			OnValueChange();
		}
	}

	public abstract void OnValueChange();

}
