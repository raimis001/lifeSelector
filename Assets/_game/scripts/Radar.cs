using UnityEngine;
using System.Collections;

public class Radar : MonoBehaviour
{

	private Cow _cow;

	// Update is called once per frame
	void Update()
	{
		if (_cow == null)
		{
			return;
		}

		transform.localScale = new Vector3(_cow.RadarRange * 0.5f, 1, _cow.RadarRange * 0.5f);

		Vector3 plane = _cow.transform.position;
		plane.y = transform.position.y;
		transform.position = plane;

	}

	public void Attach(Cow cow)
	{
		_cow = cow;
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
