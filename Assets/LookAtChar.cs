using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtChar : MonoBehaviour
{
    [SerializeField]
    Transform lookAt = null;

    [SerializeField]
    float maxRotationPerFrame;

    // Start is called before the first frame update
    void Start()
    {
        if (lookAt == null)
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward,Vector3.up), maxRotationPerFrame);
    }
}
