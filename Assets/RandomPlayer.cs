using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlayer : MonoBehaviour
{
    [SerializeField]
    float min;

    [SerializeField]
    float max;

    float cooldown = 0;
	AudioSource audioSource = null;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void Update()
	{
		if ( cooldown <= 0 )
		{
			cooldown = Random.Range(min, max);
			audioSource.Play();
		}

		cooldown -= Time.deltaTime;
	}
}
