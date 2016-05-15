using UnityEngine;
using System.Collections;

public class LineVector : MonoBehaviour
{

	public Vector3 FromVector;
	public Vector3 ToVector;
	public float LineWidth = 0.1f;

	public bool Enabled = true;

	protected LineRenderer _line;

	// Use this for initialization
	protected virtual void Start()
	{
		_line = gameObject.AddComponent<LineRenderer>();
		_line.material = new Material(Shader.Find("Particles/Additive"));
		_line.SetColors(Color.blue, Color.white);
		_line.SetWidth(LineWidth, LineWidth);
	}

	protected virtual void OnDestroy()
	{
		if (_line) Destroy(_line);
	}

	// Update is called once per frame
	protected virtual void Update()
	{
		if (!Enabled)
		{
			_line.enabled = false;
			return;
		}
		_line.enabled = true;

		_line.SetPosition(0, FromVector);
		_line.SetPosition(1, ToVector);

	}
}
