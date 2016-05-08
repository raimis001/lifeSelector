using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class EnemyManager : MonoBehaviour
{
	public List<WaveClass> Waves;
	public float PauseTime = 5;

	private int _currentWave = -1;
	private float _pause;
	private bool _waveInProgress;

	void Start()
	{
		_currentWave = 0;
		_pause = PauseTime;
	}

	void Update()
	{
		if (_waveInProgress) return;

		_pause -= Time.deltaTime;
		if (_pause <= 0)
		{
			StartWave();
		}
	}

	void StartWave()
	{

		if (_waveInProgress)
		{
			Debug.Log("Wave in progress quit");
			return;
		}
		if (_currentWave >= Waves.Count)
		{
			Debug.Log("Waves ended count:" + Waves.Count + " current:" + _currentWave);
			enabled = false;
			return;
		}

		WaveClass wave = Waves[_currentWave];

		if (wave.Active)
		{
			Debug.Log("Current wave is active");
			return;
		}

		Debug.Log("Start wave");
		_waveInProgress = true;
		StartCoroutine(wave.WaitMonsters(EndWave));

	}

	void EndWave()
	{
		Debug.Log("End wave");
		_pause = PauseTime;
		_currentWave++;
		_waveInProgress = false;
	}

}
