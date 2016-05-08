using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class AttackActivity : CowActivity
{
	public AttackParams Attack;

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();

		Enemy enemy = Helper.FindClosestEnemy(Cow.Position, Cow.RadarRange);
		foreach (Monster monster in Cow.Monsters)
		{
			monster.AddAction<AttackMonster>(enemy);
		}

	}

}
