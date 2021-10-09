using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectile : Damager
{
    public Vector2 Direction { get; set; }
    public float Speed { get; set; }

    [SerializeField]
    float DestroyAfter = 0.1f;

	private void Awake()
	{
        Destroy(gameObject, DestroyAfter);
	}

	// Update is called once per frame
	void Update()
    {
        transform.right = Direction;
        transform.Translate(Direction * Speed * Time.deltaTime, Space.World);
    }
}
