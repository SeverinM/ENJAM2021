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
            lookAt = PlayerHolder.PlayerTransform;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.up = Vector3.Lerp(transform.up, (lookAt.position - transform.position).normalized, speedRotation * 0.01f);
        
        Vector3 diff = lookAt.position-transform.position;
        diff.Normalize();
        
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, rot_z - 90), speedRotation*0.01f);
    }
}
