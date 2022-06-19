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
		int currSceneIndex = SceneManager.GetActiveScene().buildIndex;
		if (currSceneIndex + 1 > SceneManager.sceneCountInBuildSettings)
			SceneManager.LoadScene(0);

		SceneManager.LoadScene(currSceneIndex + 1);
	}
}
