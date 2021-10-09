using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shotgun : MonoBehaviour, IWeapon
{
	[SerializeField]
	float fireRate = 0.5f;

	[SerializeField]
	GameObject prefab = null;

	[SerializeField]
	float speedProjectile = 20.0f;

	[SerializeField]
	float speedInterval = 1f;

	[SerializeField]
	float angleDispersion = 40.0f;

	[SerializeField]
	int numberOfBullet = 10;

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
		for (int k = 0; k < numberOfBullet; k++) {
			GameObject instance = Instantiate(prefab);
			StraightProjectile proj = instance.GetComponent<StraightProjectile>();
			proj.transform.position = transform.position;

			Vector2 finalDirection = Quaternion.AngleAxis(Random.RandomRange(-angleDispersion, angleDispersion), Vector3.forward) * transform.up;

			if (proj != null)
			{
				proj.Direction = finalDirection;
				proj.Speed = speedProjectile+Random.RandomRange(-speedInterval, speedInterval);
			}
		}
	}
}