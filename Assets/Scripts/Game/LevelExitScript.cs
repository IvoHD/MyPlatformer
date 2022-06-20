using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExitScript : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Invoke("LoadNextLevel", 2f);
	}

	void LoadNextLevel()
	{
		GameManager.instance.LoadNextLevel();
	}
}
