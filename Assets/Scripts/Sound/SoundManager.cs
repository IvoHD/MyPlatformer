using UnityEngine;
using Assets.Scripts.Enums;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance = null;
	private void Awake()
	{
		if (instance is null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
			Destroy(gameObject);
	}

	[SerializeField]
	List<AudioClip> clipList = new List<AudioClip>();

	AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void PlaySound(Sound sound)
	{
		audioSource.PlayOneShot(clipList[(int)sound], 0.7F);
	}
}
