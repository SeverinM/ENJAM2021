using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunShot : Damager
{
    public Vector2 Direction { get; set; }

    [SerializeField]
    float DestroyAfter = 0f;

    [SerializeField]
    float waitSecond = 0.5f;

    private void Awake()
    {
        StartCoroutine(MyDestroy());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator MyDestroy()
    {
        //Stuuf before

        yield return new WaitForSeconds(waitSecond); ;
        //Stuff after
        Destroy(gameObject, DestroyAfter);
    }
}
