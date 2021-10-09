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
			StartCoroutine(Fire());
		}
	}

	 IEnumerator Fire()
	{

		shootingCooldown = 1.0f / fireRate;
		yield return new WaitForSeconds(waitForCharge);
		GameObject instance = Instantiate(prefab, parent);
		RailgunShot proj = instance.GetComponent<RailgunShot>();
		proj.transform.position = transform.position;

		proj.transform.Translate(Vector3.up * 30f, Space.Self);


		Vector2 finalDirection = Quaternion.AngleAxis(0, Vector3.forward) * transform.up;

		if (proj != null)
		{
			proj.Direction = finalDirection;
		}
		proj.transform.parent = null;
	}
}