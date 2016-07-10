using UnityEngine;
using System.Collections;


public class HarvestActivity : CowActivity
{
	public float HarvestRange = 1f;
	public float HarvestRate = 1f;
	public float HarvestTime = 0.5f;
	public float HarvestStorage = 50;


	protected override void Update()
	{
		base.Update();
		HarvestStimulus stimulus = Helper.FindClosestTarget<HarvestStimulus>(Cow.Position, Cow.RadarRange);
		if (!stimulus)
		{
			foreach (Monster monster in Cow.Monsters)
			{
				if (monster.GetComponent<MonsterAction>()) monster.GetComponent<MonsterAction>().Actor = null;
			}
			return;
		}

		//foreach (Monster monster in Cow.Monsters)
		//{
			//monster.AddAction<HarvestMonster>(stimulus);
		//}

	}
}
