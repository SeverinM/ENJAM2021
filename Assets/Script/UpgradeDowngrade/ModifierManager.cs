using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ModifierManager : MonoBehaviour
{
    public ModifierHolder holder;
    public static ModifierManager Instance = null;
	public List<int> allSlotsID = new List<int>();

	private void Awake()
	{
		Instance = this;
	}

	public void AcquireModifier(int ID)
	{
		ModifierHolder.Modifier modifier = holder.modifiers.Find(x => x.ID == ID);
		if (modifier != null)
			modifier.Acquired = true;
	}
}
