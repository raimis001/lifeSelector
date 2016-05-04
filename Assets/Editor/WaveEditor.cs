using UnityEngine;
using System.Collections;
using Mono.CSharp;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(EnemyManager))]
public class WaveEditor : Editor
{

	EnemyManager _target;
	private readonly List<bool> _showMonster = new List<bool>();
	private readonly List<bool> _showWave = new List<bool>();

	void OnEnable()
	{
		_target = (EnemyManager)target;
	}

	public override void OnInspectorGUI()
	{
		_target.PauseTime = EditorGUILayout.Slider("Pause between waves", _target.PauseTime, 1, 60);

		EditorGUILayout.LabelField("Waves", EditorStyles.boldLabel);
		for (int i = 0; i < _target.Waves.Count; i++)
		{
			if (_showMonster.Count - 1 < i)
			{
				_showMonster.Add(true);
				_showWave.Add(true);
			}
			_showWave[i] = EditorGUILayout.Foldout(_showWave[i], "Wave " + i);

			if (!_showWave[i]) continue;
			EditorGUI.indentLevel++;

			WaveClass wave = _target.Waves[i];
			wave.SpawnPoint = EditorGUILayout.ObjectField("SpawnPoint", wave.SpawnPoint, typeof (Transform), true) as Transform;
			wave.BornTime = EditorGUILayout.Slider("Born time(s)", wave.BornTime, 0, 5);

			_showMonster[i] = EditorGUILayout.Foldout(_showMonster[i], "Monsters");
			if (_showMonster[i])
			{
				EditorGUI.indentLevel++;
				for (int j = 0; j < wave.Monsters.Count; j++)
				{
					EditorGUILayout.LabelField("Monster " + j, EditorStyles.boldLabel);
					EditorGUI.indentLevel++;
					WaveAttack monster = wave.Monsters[j];

					monster.SpawnCount = EditorGUILayout.IntSlider("Spawn count", monster.SpawnCount, 1, 50);
					monster.Hitpoints = EditorGUILayout.FloatField("Hitpoints", monster.Hitpoints);
					monster.Speed = EditorGUILayout.FloatField("Monsters speed", monster.Speed);

					monster.Params.AttackRange = EditorGUILayout.FloatField("Attack range", monster.Params.AttackRange);
					monster.Params.AttackDamage = EditorGUILayout.FloatField("Attack damage", monster.Params.AttackDamage);
					monster.Params.AttackSpeed = EditorGUILayout.FloatField("Attack speed", monster.Params.AttackSpeed);

					EditorGUI.indentLevel--;
					EditorGUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					if (GUILayout.Button("Remove monster", GUILayout.Width(120)))
					{
						wave.Monsters.Remove(monster);
					}
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.TextArea("", GUI.skin.horizontalSlider);
				}

				EditorGUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				if (GUILayout.Button("Add monster", GUILayout.Width(140)))
				{
					wave.Monsters.Add(new WaveAttack());
				}
				EditorGUILayout.EndHorizontal();

				EditorGUI.indentLevel--;
			}
			EditorGUI.indentLevel--;
			EditorGUILayout.TextArea("", GUI.skin.horizontalSlider);

			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Remove wave", GUILayout.Width(170)))
			{
				_target.Waves.Remove(wave);
			}
			EditorGUILayout.EndHorizontal();
		}

		EditorGUILayout.BeginHorizontal();
			AddWave();
		EditorGUILayout.EndHorizontal();

		//Save data
		EditorUtility.SetDirty(_target);
	}

	void AddWave()
	{
		if (GUILayout.Button("Add wave"))
		{
			_target.Waves.Add(new WaveClass());
		}
	}

	void RemoveWave()
	{
		if (GUILayout.Button("Remove"))
		{
		}
	}

}
