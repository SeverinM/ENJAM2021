using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Railgun : MonoBehaviour, IWeapon
{
	[SerializeField]
	float fireRate = 1.0f;

	[SerializeField]
	GameObject prefab = null;

	[SerializeField]
	Transform parent = null;

	[SerializeField]
	float waitForCharge = 0.2f;


	bool shooting = false;
	float shootingCooldown = 0;

	public void OnEndFire()
	{
		shooting = false;
	}

	public void OnStartFire()
	{
		shooting = true;
	}

	private void Update()
	{
		if (shootingCooldown >= 0.0f)
		{
			shootingCooldown -= Mathf.Min(shootingCooldown, Time.deltaTime);
		}
		if (shootingCooldown <= 0.0f && shooting)
		{
			Fire();
		}
	}

	void Fire()
	{
		shootingCooldown = 1.0f / fireRate;
		GameObject instance = Instantiate(prefab, parent);
		instance.transform.localEulerAngles = new Vector3(0, 0, 90);
		instance.transform.localPosition = Vector3.zero;
		Destroy(instance, 0.75f);
	}
}