using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerHolder : MonoBehaviour
{
    [SerializeField]
    Hittable hittable;
	SpriteRenderer sprt;

	public static Transform PlayerTransform = null;

	private void Awake()
	{
		sprt = GetComponent<SpriteRenderer>();
		PlayerTransform = transform;
	}

	private void OnEnable()
	{
		hittable.OnStartRecovery += StartReco;
		hittable.OnEndRecovery += EndReco;
		hittable.OnDeath += () => Application.Quit();
	}

	private void OnDisable()
	{
		hittable.OnStartRecovery -= StartReco;
		hittable.OnEndRecovery -= EndReco;
		hittable.OnDeath -= () => Application.Quit();
	}

	Tween tween = null;
	void StartReco()
	{
		tween = sprt.DOFade(0, 0.2f).SetLoops(-1, LoopType.Yoyo).Play();
	}
	void EndReco()
	{
		sprt.color = Color.white;
		tween.Kill();
	}

}
