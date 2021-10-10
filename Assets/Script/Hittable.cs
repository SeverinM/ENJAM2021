using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hittable : MonoBehaviour
{
    [SerializeField]
    int sideID = 0;
    public int SideID => sideID;

    [SerializeField]
    int HP = 0;
    public int HPVal => HP;

    [SerializeField]
    int maxHP = 0;
    public int MaxHP => maxHP;
    bool triggeredDeath = false;

    public bool IsInvincible { get; private set; }

    public float RatioHP => ((float)HP / (float)maxHP);

    [SerializeField]
    float recoveryTime = 0;

    public delegate void noParam();
    public event noParam OnDeath;
    public event noParam OnStartRecovery;
    public event noParam OnEndRecovery;

    public delegate void intParam(int value);
    public event intParam OnHit;
    public event intParam OnHeal;

	private void OnDestroy()
	{
        OnDeath = delegate { };
        OnHit = delegate { };
    }
	public void Hit(int value)
	{
        if (IsInvincible)
            return;

        if (seq != null && seq.IsPlaying())
            return;

        HP -= value;
        OnHit?.Invoke(value);
        if (HP <= 0)
		{
            if ( !triggeredDeath )
            {
                triggeredDeath = true;
                OnDeath?.Invoke();
            }
            Destroy(gameObject.transform.parent.gameObject);
		}
        else if ( recoveryTime > 0)
		{
            StartRecovery();
		}
	}

    public void Heal(int value)
	{
        HP += value;
        OnHeal?.Invoke(value);
	}

    Sequence seq = null;
    void StartRecovery()
	{
        seq = DOTween.Sequence()
            .AppendCallback(() => OnStartRecovery?.Invoke())
            .AppendInterval(recoveryTime)
            .AppendCallback(() => OnEndRecovery?.Invoke())
            .SetAutoKill(true)
            .Play();
	}
}
