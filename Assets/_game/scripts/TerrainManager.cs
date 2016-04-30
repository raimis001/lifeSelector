using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TerrainManager : MonoBehaviour
{


	public delegate void TerrainClick(Vector3 position);
	public static event TerrainClick OnTerrainClick;

	void OnMouseUp()
	{
		if (EventSystem.current && EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
		{
			//Debug.Log("TERRAIN: hit on terrain");
			if (OnTerrainClick != null) OnTerrainClick(hit.point);
		}
	}
}
