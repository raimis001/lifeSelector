using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TerrainManager : MonoBehaviour
{


	public delegate void TerrainClick(Vector3 position);
	public static event TerrainClick OnTerrainClick;

	protected static TerrainManager _instance;

	public static Vector3 CurrentMouse()
	{
		if (!_instance) return Vector3.zero;

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (_instance.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
		{
			//Debug.Log("TERRAIN: hit on terrain");
			return hit.point;
		}

		return Vector3.zero;
	}

	void Awake()
	{
		_instance = this;
	}

	void OnMouseUp()
	{
		if (EventSystem.current && EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		/*
		layerMask = 1 << LayerMask.NameToLayer("layerX"); // only check for collisions with layerX
		layerMask = ~(1 << LayerMask.NameToLayer("layerX")); // ignore collisions with layerX
		LayerMask layerMask = ~(1 << LayerMask.NameToLayer("layerX") | 1 << LayerMask.NameToLayer("layerY")); // ignore both layerX and layerY
		*/
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;
		if (GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
		{
			//Debug.Log("TERRAIN: hit on terrain");
			if (OnTerrainClick != null) OnTerrainClick(hit.point);
		}
	}
}
