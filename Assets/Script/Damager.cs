using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField]
    int sideID = 0;
	public int SideID => sideID;

    [SerializeField]
    int damage = 0;

	[SerializeField]
	bool destroyOnNotHittable = false;

	[SerializeField]
	bool destroyWhenHit = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Hittable hittable = collision.GetComponent<Hittable>();

		if (hittable == null)
		{
			if (destroyOnNotHittable)
				Destroy(gameObject);
			return;
		}

		if (hittable.SideID == sideID)
			return;

		hittable.Hit(damage);
		if (destroyWhenHit)
			Destroy(gameObject);
	}
}
