using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class TerrainManager : MonoBehaviour
{


	public delegate void TerrainClick(Vector3 position);
	public static event TerrainClick OnTerrainClick;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetMouseButtonUp(0))
		{

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
			{

				int layer = 1 << LayerMask.NameToLayer("objects");
				Collider[] hitColliders = Physics. OverlapSphere(hit.point, 0.5f);

				if (hitColliders.Length > 1)
				{
					Debug.Log("Hit vēl kaut kam");
				}
				else
				{
					if (OnTerrainClick != null) OnTerrainClick(hit.point);
				}

			}
		}
	}
}
