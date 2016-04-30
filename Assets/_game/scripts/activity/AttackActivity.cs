using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class AttackActivity : Activity
{
	public AttackParams Attack;

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();

		Enemy[] enemys = FindObjectsOfType<Enemy>();
		foreach (Enemy enemy in enemys)
		{
			if (Cow.Distance(enemy) < Cow.RadarRange)
			{
				foreach (Monster monster in Cow.Monsters)
				{
					if (!monster.Enemy)
					{
						monster.Enemy = enemy;
					}
				}
				break;
			}
		}

	}

}
