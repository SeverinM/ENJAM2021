using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class WaveAnnouncer : MonoBehaviour
{
    [SerializeField]
    float time;

    [SerializeField]
    CanvasGroup grp;

    [SerializeField]
    TMP_Text text;

    [SerializeField]
    WaveManager manager;

    int index = 0;
    public void Announce(float timeoffset)
    {
        index++;
        text.text = "Vague " + index;

        DOTween.Sequence()
            .AppendInterval(timeoffset)
            .Append(grp.DOFade(1.0f, time))
            .AppendInterval(time)
            .Append(grp.DOFade(0.0f, time))
            .AppendCallback(() => manager.NextWave())
            .SetAutoKill(true)
            .Play();
    }

    private void Start()
    {
        Announce(0.5f);
    }
}
