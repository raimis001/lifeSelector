using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MoveObject : Mover
{

	public float MaxHitpoints = 100;
	public float Hitpoints;

	public float Live = Mathf.Infinity;
	public float LiveModiefier = 1;

	[Header("Interface")]
	public guiProgress Progress;

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

	private void OnMouseUp()
	{
		if (EventSystem.current && EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}
		OnMouseClick();
	}

	protected void Log(object message)
	{
		Debug.Log(message);
	}

}
