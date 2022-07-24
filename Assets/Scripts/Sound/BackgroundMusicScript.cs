using UnityEngine;

public class BackgroundMusicScript : MonoBehaviour
{
	AudioSource audioSource;

	static BackgroundMusicScript instance;

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

	public void PlayMusic()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.loop = true;
		audioSource.Play();
	}

}
