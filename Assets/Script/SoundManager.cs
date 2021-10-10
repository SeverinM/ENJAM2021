using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;
using DG.Tweening;

public class SoundManager : MonoBehaviour
{
    FMOD.Studio.EventInstance instance;

    [EventRef]
    public string eventSound = "";
    Tween tween = null;
    

    private void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(eventSound);
        instance.start();
    }

    public void WaveStart()
    {
        tween?.Kill();
        tween = DOTween.To(() => 0.0f, x => instance.setParameterByName("Ennemies", x), 100.0f, 100.0f).SetAutoKill(true).Play();

        instance.setParameterByName("Vague", 1);
    }

    public void WaveEnd()
    {
        tween?.Kill();
        instance.setParameterByName("Vague", 0);
    }
}
