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
		GameObject instance = Instantiate(prefab);
		StraightProjectile proj = instance.GetComponent<StraightProjectile>();
		proj.transform.position = transform.position;

		Vector2 finalDirection = Quaternion.AngleAxis(Random.RandomRange(-angleDispersion, angleDispersion), Vector3.forward) * transform.right;

		if ( proj != null)
		{
			proj.Direction = finalDirection;
			proj.Speed = speedProjectile;
		}
	}
}
