using System;
using UnityEngine;
using System.Collections;

//[Serializable]
public class EnemySetup : ScriptableObject
{
	public Transform SpawnPoint;

	[Range(0,5)]
	public float Speed = 1;

	[Range(0,3)]
	public float BornTime = 1;

	public AttackParams Attack;


}
