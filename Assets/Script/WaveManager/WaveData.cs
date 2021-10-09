using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveData", order = 1)]
public class WaveData : ScriptableObject
{
	[Serializable]
    public class Wave
	{
		public List<Subwave> subwaves = new List<Subwave>();
	}

	[Serializable]
	public class Subwave
	{
		public List<SpawnContext> contexts = new List<SpawnContext>();
	}

	[Serializable]
	public class SpawnContext
	{
		public Vector2 startPosition;
		public Vector2 goToPosition;
		public AnimationCurve curve;
		public float timeOffset;
		public int numberOfKillRequired = 0;
		public GameObject prefab;

		[System.NonSerialized]
		public bool scheduled = false;
	}

	public List<Wave> waves = new List<Wave>();
}
