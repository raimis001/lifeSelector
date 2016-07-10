using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Stimulus : BaseObject
{

	[HideInInspector]
	public int StimulusId;

	[Range(0, 10)]
	public float TestRadius = -1;

	public float Radiuss = 4;
	public float Force = 200;

	public float ChargeTime = 5;

	public Transform Perimeter;

	public int[] Genetics;

	void Start()
	{
		TestRadius = Radiuss;
		RedrawPerimeter();
	}

	void Update()
	{
		if (TestRadius != Radiuss)
		{
			Radiuss = TestRadius;
			RedrawPerimeter();
		}
	}

	void RedrawPerimeter()
	{
		if (Perimeter)
		{
			Perimeter.localScale = new Vector3(Radiuss, Perimeter.localScale.y, Radiuss);
		}

	}

	public int RandomGenetic()
	{
		return Genetics[Random.Range(0, Genetics.Length)];
	}

	public void OnMouseUp()
	{
		TerrainManager.SimulateClick(transform.position);
	}
}
