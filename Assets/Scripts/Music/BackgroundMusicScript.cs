using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicScript : MonoBehaviour
{
	AudioSource audioSource;
	void Awake()
	{
		DontDestroyOnLoad(gameObject);	
	}


	public void PlayMusic()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.loop = true;
		audioSource.Play();
	}

}
