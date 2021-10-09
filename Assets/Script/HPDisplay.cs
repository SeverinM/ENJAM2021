using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPDisplay : MonoBehaviour
{
    [SerializeField]
    Hittable hit;

    [SerializeField]
    Image img = null;

    [SerializeField]
    Text txt = null;

	private void Awake()
	{
        UpdateLife();
	}

	private void OnEnable()
	{
		hit.OnHit += (x) => UpdateLife();
	}

	private void OnDisable()
	{
		hit.OnHit -= (x) => UpdateLife();
	}

	void UpdateLife()
	{
        txt.text = string.Format("{0}/{1}", hit.HPVal, hit.MaxHP);
		img.fillAmount = hit.RatioHP;
    }
}
