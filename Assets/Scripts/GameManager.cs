using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int currentLevelIndex = 2;  //Index 2 is equal to Level 1

    public static GameManager m_instance;
    public static GameManager instance
	{
        get {
            if (!m_instance)
            {
                m_instance = new GameObject().AddComponent<GameManager>();
                m_instance.name = m_instance.GetType().ToString();
                DontDestroyOnLoad(m_instance.gameObject);
            }
            return m_instance;
        }
	}



    public void setCurrentLevel(int levelIndex)
    {
        currentLevelIndex = levelIndex;
        if (currentLevelIndex == 10)
            SceneManager.LoadScene(0);
    }

    public int getCurrentLevel()
	{
        return currentLevelIndex;
	}

    public void LoadCurrentLevel()
	{
        SceneManager.LoadScene(currentLevelIndex);
	}

    public void LoadLevel(int levelIndex)
	{
        if (levelIndex <= currentLevelIndex)
            SceneManager.LoadScene(levelIndex);
	}

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentLevelIndex + 1 > SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(currentLevelIndex + 1);
    }
}


