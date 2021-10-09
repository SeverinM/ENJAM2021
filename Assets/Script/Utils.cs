using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Utils : MonoBehaviour
{
	[SerializeField]
	float fadeTime = 0;

    public void SetTimeScale( float newVal )
	{
		Time.timeScale = newVal;
	}

	public void FadeIn(GameObject gob)
	{
		gob.SetActive(true);
		gob.GetComponent<CanvasGroup>().DOFade(1, fadeTime).SetUpdate(true).Play();
	}

	public void FadeOut(GameObject gob)
	{
		DOTween.Sequence()
			.Append(gob.GetComponent<CanvasGroup>().DOFade(0.0f, fadeTime))
			.AppendCallback(() => gob.SetActive(false)).SetUpdate(true).Play();
	}
}
