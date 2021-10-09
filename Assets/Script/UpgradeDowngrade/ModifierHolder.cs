using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Modifiers", order = 1)]
public class ModifierHolder : ScriptableObject
{
    [System.Serializable]
    public class Modifier
	{
		public int ID;
		public string name;
		public Sprite icon;
		public string malusName;
		public GameObject prefab;

		[System.NonSerialized]
		public bool Acquired;
	}

	public List<Modifier> modifiers = new List<Modifier>();
}
