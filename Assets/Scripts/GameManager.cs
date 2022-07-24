using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int maxLevelIndex = 1;  //Index 1 is equal to Level 1

    [SerializeField]
    GameObject coinObj;

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
        JSONSave.Reset();
	}

	private void OnLevelWasLoaded(int level)
	{
        if (level > 10 || level < 1)
            return;
        foreach (Coin coin in JSONSave.GetCurrentState())
            Instantiate(coinObj, coin._pos, new Quaternion(0, 0, 0, 0));
    }

	public void Save()
	{
        JSONSave.Save();
	}
}

public class JSONSave 
{
    static string path = Application.dataPath + Path.AltDirectorySeparatorChar + "CoinState.json";
    static string defaultStatePath = Application.dataPath + Path.AltDirectorySeparatorChar + "DefaultCoinState.json";

    public static void Save()
    {
        int index = SceneManager.GetActiveScene().buildIndex - 1;
        List<List<Coin>> coinList = new List<List<Coin>>();

        //gets curret game state
        StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        reader.Close();
        coinList = JsonHelper.FromJson(json);

        coinList[index] = new List<Coin>();

        GameObject[] Coins = GameObject.FindGameObjectsWithTag("Coin");

        foreach (GameObject coin in Coins)
            coinList[index].Add(new Coin(coin.transform.position));

        json = JsonHelper.ToJson(coinList, true);

        StreamWriter writer = new StreamWriter(path);

        writer.Write(json);
        writer.Close();
    }

    public static List<Coin> GetCurrentState()
    {
        int index = SceneManager.GetActiveScene().buildIndex - 1;
        StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        reader.Close();

        List<List<Coin>> coinList = JsonHelper.FromJson(json);


        return coinList[index];
       
    }

    public static void Reset()
	{
        StreamReader reader = new StreamReader(defaultStatePath);
        string json = reader.ReadToEnd();
        reader.Close();
        StreamWriter writer = new StreamWriter(path);
        writer.Write(json);
        writer.Close();
    }
}

public static class JsonHelper
{
    public static List<List<Coin>> FromJson(string json)
    {
        Wrapper2 wrapper = JsonUtility.FromJson<Wrapper2>(json);
        List<List<Coin>> coinList = new List<List<Coin>>(10);

        if (wrapper is null)
            return coinList;

        foreach (Wrapper1 list in wrapper.Items)
            coinList.Add(list.Items);

        return coinList;
    }

    public static string ToJson(List<List<Coin>> array, bool prettyPrint)
    {

        Wrapper2 wrapper2 = new Wrapper2();

        foreach (List<Coin> list in array)
        {
            Wrapper1 wrapper1 = new Wrapper1();

            foreach (Coin item in list)
                wrapper1.Items.Add(item);
            wrapper2.Items.Add(wrapper1);
        }

        return JsonUtility.ToJson(wrapper2, prettyPrint);
    }

    [Serializable]
    private class Wrapper1
    {
        public List<Coin> Items = new List<Coin>();
    }

    [Serializable]
    private class Wrapper2
    {
        public List<Wrapper1> Items = new List<Wrapper1>();
    }
}

[Serializable]
public class Coin
{
    public Vector3 _pos;

    public Coin(Vector3 pos)
    {
        _pos = pos;
    }
}


