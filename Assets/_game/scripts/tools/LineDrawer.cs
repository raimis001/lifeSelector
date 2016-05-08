using UnityEngine;
using System.Collections;

public class LineDrawer : MonoBehaviour
{
	public Transform FromPoint;
	public Transform ToPoint;

	public bool Enabled = true;

	private LineRenderer _line;

	// Use this for initialization
	void Start()
	{
		_line = gameObject.AddComponent<LineRenderer>();
		_line.material = new Material(Shader.Find("Particles/Additive"));
		_line.SetColors(Color.blue, Color.black);
		_line.SetWidth(0.1f, 0.1f);
	}

	// Update is called once per frame
	void Update()
	{

		if (!Enabled || !FromPoint || !ToPoint)
		{
			_line.enabled = false;
			return;
		}
		_line.enabled = true;

		_line.SetPosition(0, FromPoint.position);
		_line.SetPosition(1, ToPoint.position);

	}
}
