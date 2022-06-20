using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int maxLevelIndex = 1;  //Index 1 is equal to Level 1

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

	private void Awake()
	{
        if (PlayerPrefs.HasKey("maxLevelIndex"))
        {
            maxLevelIndex = PlayerPrefs.GetInt("maxLevelIndex");
        }
    }

	public void LoadMaxLevel()
	{
        SceneManager.LoadScene(maxLevelIndex);
    }

    public int getMaxLevelIndex()
	{
        return maxLevelIndex;
	}

    public int getCurrentLevel()
	{
        return SceneManager.GetActiveScene().buildIndex;
	}

    public void LoadLastLevel()
	{
        SceneManager.LoadScene(maxLevelIndex);
	}

    public void LoadLevel(int levelIndex)
	{
        if (levelIndex <= maxLevelIndex)
            SceneManager.LoadScene(levelIndex);
	}

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentLevelIndex + 1 > SceneManager.sceneCountInBuildSettings - 1)
		{
            SceneManager.LoadScene(0);
            return;
		}
        if (currentLevelIndex + 1 > maxLevelIndex)
            maxLevelIndex = currentLevelIndex + 1;
        SceneManager.LoadScene(currentLevelIndex + 1);
    }
}


