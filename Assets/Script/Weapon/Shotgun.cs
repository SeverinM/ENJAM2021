using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shotgun : MonoBehaviour, IWeapon
{
	[SerializeField]
	float fireRate = 1.0f;

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

	Sequence seq = null;
	public void OnEndFire()
	{
		seq.Kill();
	}

	public void OnStartFire()
	{
		seq = DOTween.Sequence()
			.AppendCallback(() => Fire())
			.AppendInterval(1.0f / fireRate)
			.SetLoops(-1, LoopType.Restart)
			.Play();
	}

	void Fire()
	{
		for (int k = 0; k < numberOfBullet; k++) {
			GameObject instance = Instantiate(prefab);
			StraightProjectile proj = instance.GetComponent<StraightProjectile>();
			proj.transform.position = transform.position;

			Vector2 finalDirection = Quaternion.AngleAxis(Random.RandomRange(-angleDispersion, angleDispersion), Vector3.forward) * transform.right;

			if (proj != null)
			{
				proj.Direction = finalDirection;
				proj.Speed = speedProjectile+Random.RandomRange(-speedInterval, speedInterval);
			}
		}
	}
}