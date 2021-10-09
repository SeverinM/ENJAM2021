using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed;

    public void Translate(Vector2 newDelta)
	{
        transform.Translate(newDelta * Time.deltaTime * speed, Space.World);
	}

    public void SetupLookAt(Vector3 position)
    {
        position.z = transform.position.z;
        transform.right = position - transform.position;
    }
}
