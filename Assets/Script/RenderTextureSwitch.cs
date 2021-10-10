using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderTextureSwitch : MonoBehaviour
{
    [SerializeField]
    List<RenderTexture> renderTextures = new List<RenderTexture>();

    [SerializeField]
    RawImage rwImage;

    Camera cam;

    float probabilityLag = 0;
    int frameDisable = 0;

    private void Start()
    {
        cam = Camera.main;
        ModifierManager.Instance.OnPickedModifier += ChangeRenderTexture;
    }

    private void OnDisable()
    {
        ModifierManager.Instance.OnPickedModifier -= ChangeRenderTexture;
    }

    void ChangeRenderTexture(int id)
    {
        if ( id == 1 )
        {
            Screen.SetResolution(400, 222, true);
        }
        if ( id == 3 )
        {
            probabilityLag += 3;
        }
        if ( id == 5 )
        {
            cam.clearFlags = CameraClearFlags.Nothing;
        }
    }

    private void Update()
    {
        if ( cam.enabled )
        {
            if ( Random.Range(0,100) < probabilityLag )
            {
                cam.enabled = false;
                frameDisable = Random.Range(30,60);
            }
        }
        else
        {
            frameDisable--;
            if ( frameDisable == 0)
            {
                cam.enabled = true;
            }
        }
    }
}
