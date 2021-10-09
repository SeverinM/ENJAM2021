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
		GameObject instance = Instantiate(prefab, parent);
		RailgunShot proj = instance.GetComponent<RailgunShot>();
		proj.transform.position = transform.position;

		proj.transform.Translate(Vector3.right * 30f, Space.Self);


		Vector2 finalDirection = Quaternion.AngleAxis(0 , Vector3.forward) * transform.right;

		if (proj != null)
		{
			proj.Direction = finalDirection;
		}
		proj.transform.parent = null;
	}
}