using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ModifierManager : MonoBehaviour
{
    public ModifierHolder holder;
    public static ModifierManager Instance = null;

	List<int> allSlotsID = new List<int>();

	public delegate void noParam(int value);
	public event noParam OnPickedModifier;

	public delegate void ModifierParam(int slot, ModifierHolder.Modifier modifier);
	public event ModifierParam OnUpdateSlot;

	private void OnEnable()
	{
		Instance = this;
	}

	public ModifierHolder.Modifier GetModifierBySlot(int SlotID)
	{
		return GetModifier(allSlotsID[SlotID]);
	}
	public ModifierHolder.Modifier GetModifier(int ID)
	{
		return holder.modifiers.Find(x => x.ID == ID);
	}
	public void FillSlot()
	{
		allSlotsID.Clear();

		List<ModifierHolder.Modifier> modifiersAvailable = holder.modifiers.Where(x => !x.Acquired && x.enabled).ToList();
		int numberToFind = Mathf.Min(3,modifiersAvailable.Count);

		for (int i = 0; i < numberToFind; i++)
		{
			int index = Random.Range(0, modifiersAvailable.Count);
			ModifierHolder.Modifier modifier = modifiersAvailable[index];
			modifiersAvailable.RemoveAt(index);
			allSlotsID.Add(modifier.ID);
			OnUpdateSlot?.Invoke(i, modifier);
		}

		for (int i = numberToFind; i < 3; i++)
		{
			OnUpdateSlot?.Invoke(i, null);
		}
	}

	public void AcquireModifier(int ID)
	{
		ModifierHolder.Modifier modifier = holder.modifiers.Find(x => x.ID == ID);
		if (modifier != null)
		{
			modifier.Acquired = true;
			if (modifier.prefab != null)
				Instantiate(modifier.prefab);
			OnPickedModifier?.Invoke(ID);
		}
	}

	public void AcquireModifierFromSlot(int slotID)
	{
		AcquireModifier(allSlotsID[slotID]);
	}
}
