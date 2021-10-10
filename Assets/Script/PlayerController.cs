using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed;

    private void OnEnable()
    {
        ModifierManager.Instance.OnPickedModifier += PickModifier;
    }

    private void OnDisable()
    {
        ModifierManager.Instance.OnPickedModifier -= PickModifier;
    }

    private void PickModifier(int ID)
    {
        if (ID == 0)
        {
            speed /= 2.0f;
        }
    }

    public void Translate(Vector2 newDelta)
	{
        transform.Translate(newDelta * Time.deltaTime * speed, Space.World);
	}

    public void SetupLookAt(Vector3 position)
    {
        position.z = transform.position.z;
        transform.up = position - transform.position;
    }
}
