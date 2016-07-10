﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MoveObject : Mover
{

	[Header("Live and hitpoints")]
	public float MaxHitpoints = 100;
	public float Hitpoints;

	public float Live = Mathf.Infinity;
	public float LiveModiefier = 1;

	[Header("Interface")]
	public guiProgress Progress;
	public bool DragObject;

	[HideInInspector]
	public string AtackTag;

	public float Hp {
		get { return Hitpoints / MaxHitpoints; }
	}

	protected override void Start()
	{
		base.Start();
		Hitpoints = MaxHitpoints;
	}

	protected override void Update()
	{
		base.Update();
		Live -= LiveModiefier * Time.deltaTime;
		if (!CheckIsLive())
		{
			DoDestroy();
		}

		if (Progress)
		{
			Progress.Value = Hp;
		}

	}

	public virtual void Select()
	{

		CowActivity[] activities = GetComponents<CowActivity>();

		foreach (CowActivity activity in activities)
		{
			activity.Select();
		}

	}

	public virtual void Deselect()
	{

		CowActivity[] activities = GetComponents<CowActivity>();

		foreach (CowActivity activity in activities)
		{
			activity.Select();
		}

	}

	protected virtual void BeforeDestroy()
	{
		
	}

	public float Distance(BaseObject obj)
	{
		return Vector3.Distance(Position, obj.Position);
	}

	public void DoDamage(float damage)
	{
		Hitpoints -= damage;
	}

	public void DoDestroy(bool explode = true, GameObject effect = null)
	{
		if (explode)
		{
			The.GameLogic.DoExplosion(transform.position, effect);
		}

		BeforeDestroy();
		Destroy(gameObject);
	}

	bool CheckIsLive()
	{
		return Live > 0 && Hitpoints > 0;
	}


	protected virtual void OnMouseClick()
	{
		
	}

	bool IsIntercat()
	{
		return !EventSystem.current || !EventSystem.current.IsPointerOverGameObject();
	}



	private bool _dragg;
	private Vector3 _moseVector3 = Vector3.zero;

	private LineVector _lineDrag;

	private void OnMouseDown()
	{
		if (!IsIntercat()) return;
		if (!DragObject) return;

		_moseVector3 = Input.mousePosition;
		_dragg = false;

		if (!_lineDrag) _lineDrag = gameObject.AddComponent<LineVector>();
		_lineDrag.LineWidth = 1;
		_lineDrag.Enabled = false;
	}

	private void OnMouseUp()
	{
		if (!IsIntercat()) return;
		Debug.Log("Mouse up: " + gameObject.name);
		if (!_dragg) OnMouseClick();

		if (!DragObject) return;

		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);
		foreach (RaycastHit hit in hits)
		{
			MoveObject obj = hit.collider.GetComponent<MoveObject>();

			if (obj)
			{
				Debug.Log("Ower:" + obj.name);
				if (obj.GetHashCode() != this.GetHashCode())
				Cow.Create(transform.position + Quaternion.Euler(0, Random.value * 360f, 0) * new Vector3(Random.value,0,Random.value) * 4f);
				break;
			}
		}

			if (_lineDrag) Destroy(_lineDrag);
		_lineDrag = null;
		_dragg = false;

	}


	private void OnMouseDrag()
	{
		if (!IsIntercat()) return;
		if (!DragObject) return;

		if (_dragg)
		{
			if (_lineDrag)
			{
				_lineDrag.FromVector = transform.position;
				_lineDrag.ToVector = TerrainManager.CurrentMouse();
				_lineDrag.Enabled = true;
			}
			return;
		}

		_dragg = Vector3.Distance(_moseVector3, Input.mousePosition) > 3;
	}



	protected void Log(object message)
	{
		Debug.Log(message);
	}

}
