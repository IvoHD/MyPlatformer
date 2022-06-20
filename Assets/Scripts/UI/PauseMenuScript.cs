using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;

    //GameObject not implemented yet
	void OnEscape()
	{
        bool isActive = pauseMenu.activeInHierarchy;
        pauseMenu.SetActive(!isActive);
        isActive = pauseMenu.activeInHierarchy;
        if (isActive)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
	}

	public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
