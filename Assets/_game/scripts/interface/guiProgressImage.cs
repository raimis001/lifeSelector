using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class guiProgressImage : guiProgress
{

	public Image ProgressImage;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public override void OnValueChange()
	{
		ProgressImage.fillAmount = Value;
	}
}
