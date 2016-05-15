using UnityEngine;
using System.Collections;

public class LineDrawer : LineVector
{
	public Transform FromPoint;
	public Transform ToPoint;


	// Update is called once per frame
	protected override void Update()
	{

		if (!Enabled || !FromPoint || !ToPoint)
		{
			_line.enabled = false;
			return;
		}
		FromVector = FromPoint.position;
		ToVector = ToPoint.position;
		
		base.Update();


	}
}
