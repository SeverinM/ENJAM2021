using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtChar : MonoBehaviour
{
    [SerializeField]
    Transform lookAt = null;

    [SerializeField]
    float speedRotation;

    // Start is called before the first frame update
    void Start()
    {
        if (lookAt == null)
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        transform.right = Vector3.Lerp(transform.right, (lookAt.position - transform.position).normalized, speedRotation * 0.01f);
    }
}
