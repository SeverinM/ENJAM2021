using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gatling : MonoBehaviour, IWeapon
{
	[SerializeField]
	float fireRate = 10.0f;

	[SerializeField]
	GameObject prefab = null;

	[SerializeField]
	float speedProjectile = 10.0f;

	[SerializeField]
	float angleDispersion = 10.0f;

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
		if ( shootingCooldown >= 0.0f )
		{
			shootingCooldown -= Mathf.Min(shootingCooldown, Time.deltaTime);
		}
		if ( shootingCooldown <= 0.0f && shooting )
		{
			Fire();
		}
	}

	void Fire()
	{
		shootingCooldown = 1.0f / fireRate;
		GameObject instance = Instantiate(prefab);
		StraightProjectile proj = instance.GetComponent<StraightProjectile>();
		proj.transform.position = transform.position;

		Vector2 finalDirection = Quaternion.AngleAxis(Random.Range(-angleDispersion, angleDispersion), Vector3.forward) * transform.up;

		if ( proj != null)
		{
			proj.Direction = finalDirection;
			proj.Speed = speedProjectile;
		}
	}

	void OnDestroy()
	{
		OnEndFire();
	}
}
