using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BaseObject : Mover
{

	public float MaxHitpoints = 100;
	public float Hitpoints;

	public float Live = Mathf.Infinity;
	public float LiveModiefier = 1;

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
			Destroy(gameObject);
		}
	}

	public Vector3 Position
	{
		get
		{
			Vector3 pos = transform.position;
			pos.y = 0;
			return pos;
		}
	}

	public float Distance(BaseObject obj)
	{
		return Vector3.Distance(Position, obj.Position);
	}

	public void DoDamage(float damage)
	{
		Hitpoints -= damage;
		if (Hitpoints <= 0)
		{
			The.GameLogic.DoExplosion(transform.position);
			Destroy(gameObject);
		}
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
