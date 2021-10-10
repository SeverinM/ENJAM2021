using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }
	int ennemyKilled = 0;
	WaveData.Wave currentWave;
	int indexSubwave = 0;
	int indexWave = 0;

	[SerializeField]
	WaveData currentData = null;

	[SerializeField]
	UnityEvent OnWaveEnd;

	[SerializeField]
	UnityEvent OnEnd;

	private void OnEnable()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(this);
		}
		currentWave = currentData.waves[0];
		CheckState();
	}

	public void NextWave()
	{
		indexSubwave = 0;
		indexWave++;
		if (indexWave == currentData.waves.Count)
			return;
		currentWave = currentData.waves[indexWave];
		CheckState();
	}

	void NextSubWave()
	{
		ennemyKilled = 0;
		indexSubwave++;
		if (indexSubwave >= currentWave.subwaves.Count)
		{
			Endwave();
		}
		else
		{
			CheckState();
		}
	}

	void SpawnEnemy(WaveData.SpawnContext context)
	{
		GameObject instance = Instantiate(context.prefab);
		instance.transform.position = context.startPosition;
		Hittable hit = instance.GetComponent<Hittable>();
		if (hit == null)
			hit = instance.GetComponentInChildren<Hittable>();
		if (hit != null)
		{
			hit.OnDeath += () =>
			{
				ennemyKilled++;
				CheckState();
			};
		}

		instance.transform.DOMove(context.goToPosition, 1.0f).SetEase(context.curve).SetAutoKill(true)
			.OnKill(() =>
			{
				if (instance.transform.GetComponent<EnemyAI>())
				{
					instance.transform.GetComponent<EnemyAI>().Block = false;
				}
			}).Play();

	}

	void Endwave()
	{
		DOTween.Sequence()
			.AppendInterval(3.0f)
			.AppendCallback(() =>
			{
				if ( indexWave == currentData.waves.Count - 1 )
				{
					OnEnd?.Invoke();
				}
				else
				{
					OnWaveEnd?.Invoke();
				}	
			})
			.SetAutoKill(true)
			.Play();
	}

	void CheckState()
	{
		if ( ennemyKilled == currentWave.subwaves[indexSubwave].contexts.Count )
		{
			NextSubWave();
			return;
		}

		foreach(WaveData.SpawnContext context in currentWave.subwaves[indexSubwave].contexts )
		{
			if (context.scheduled)
				continue;

			if ( ennemyKilled >= context.numberOfKillRequired )
			{
				context.scheduled = true;
				DOTween.Sequence()
					.AppendInterval(context.timeOffset)
					.AppendCallback(() => SpawnEnemy(context))
					.SetAutoKill(true)
					.Play();
			}
		}
	}
}
