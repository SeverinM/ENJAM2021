using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponHolder : MonoBehaviour
{
    List<IWeapon> allWeapon = new List<IWeapon>();

	[SerializeField]
	bool autoFire = false;

	private void Awake()
	{
		allWeapon = GetComponentsInChildren<IWeapon>().ToList();

		if (autoFire)
			StartFire();

		IWeapon weap = GetComponent<IWeapon>();
		if (weap != null)
			allWeapon.Add(weap);
	}

	public void StartFire()
	{
		foreach (IWeapon weap in allWeapon)
			weap.OnStartFire();
	}

	public void EndFire()
	{
		foreach (IWeapon weap in allWeapon)
			weap.OnEndFire();
	}
}
