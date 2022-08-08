using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int maxLevelIndex = 1;  //Index 1 is equal to Level 1

    [SerializeField]
    List<GameObject> objects;

    public static GameManager instance;


    void Awake()
    {
        if (instance is null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (PlayerPrefs.HasKey("maxLevelIndex"))
            {
                maxLevelIndex = PlayerPrefs.GetInt("maxLevelIndex");
            }
            GameObject.Find("BackGroundMusic").GetComponent<BackgroundMusicScript>().PlayMusic();
        }
        else
            Destroy(gameObject);
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

    /// <summary>
    /// Reloads current scene
    /// </summary>
    public void ReloadScene()
    {
        Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Loads next level
    /// </summary>
    public void LoadNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentLevelIndex + 1 > SceneManager.sceneCountInBuildSettings - 1)
        {
            LoadMainMenu();
            return;
        }
        if (currentLevelIndex + 1 > maxLevelIndex)
            maxLevelIndex = currentLevelIndex + 1;
        Save();
        PlayerPrefs.SetInt("score", ScoreKeepScript.instance.GetScore());
        SceneManager.LoadScene(currentLevelIndex + 1);
    }

    /// <summary>
    /// Loads MainMenu
    /// </summary>
	public void LoadMainMenu()
    {
        Save();
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Saves game progress
    /// </summary>
    public void SaveSessionProgress()
    {
        PlayerPrefs.SetInt("maxLevelIndex", maxLevelIndex);
        PlayerPrefs.SetInt("score", ScoreKeepScript.instance.GetScore());
    }

    /// <summary>
    /// deletes level prefs and resets values in current session
    /// </summary>
    public void DeleteProgress()
    {
        maxLevelIndex = 1;
        ScoreKeepScript.instance.resetScore();
        SavingManager.instance.Reset();
    }

    void OnLevelWasLoaded(int levelIndex)
    {
		if (levelIndex > 10 || levelIndex < 1)
			return;
        foreach (Object obj in SavingManager.instance.GetCurrentState()) 
            Instantiate(objects[(int)obj._type], obj._pos, new Quaternion(0, 0, 0, 0));
	}

	public void Save()
    {
        SavingManager.instance.Save();
    }

    public void Start()
    {
		if (!File.Exists(Application.persistentDataPath + "DefaultGameState.json"))
		{
			TextAsset defaultCoinState = Resources.Load("DefaultGameState") as TextAsset;
			StreamWriter writer = new StreamWriter(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "DefaultGameState.json");
			writer.Write(defaultCoinState.text);
			writer.Close();

		}

		if (!File.Exists(Application.persistentDataPath + "GameState.json"))
		{
			StreamWriter writer = new StreamWriter(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "GameState.json");
			StreamReader reader = new StreamReader(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "DefaultGameState.json");
			writer.Write(reader.ReadToEnd());
			writer.Close();
		}
	}
}