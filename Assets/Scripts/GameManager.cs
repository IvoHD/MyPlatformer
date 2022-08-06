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
        JSONSave.Reset();
    }

    void OnLevelWasLoaded(int levelIndex)
    {
		if (levelIndex > 10 || levelIndex < 1)
			return;
        foreach (Object obj in JSONSave.GetCurrentState())
			Instantiate(objects[(int)obj._type], obj._pos, new Quaternion(0, 0, 0, 0));
	}

	public void Save()
    {
        JSONSave.Save();
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

public class JSONSave 
{
    static string path = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "GameState.json";
    static string defaultStatePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "DefaultGameState.json";

    public static void Save()
    {
        int index = SceneManager.GetActiveScene().buildIndex - 1;
        List<List<Object>> objectList = new List<List<Object>>();

        //gets curret game state
        StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        reader.Close();
        objectList = JsonHelper.FromJson(json);

		objectList[index] = new List<Object>();

        var ojectsToSave = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();

		foreach (ISaveable obj in ojectsToSave)
            objectList[index].Add(new Object(obj.GetPositionToSave(), obj.GetObjectType()));

        json = JsonHelper.ToJson(objectList, true);

        StreamWriter writer = new StreamWriter(path);

        writer.Write(json);
        writer.Close();
    }

    public static List<Object> GetCurrentState()
    {
        int index = SceneManager.GetActiveScene().buildIndex - 1;
        StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        reader.Close();

        List<List<Object>> objectList = JsonHelper.FromJson(json);


        return objectList[index];
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
    public static List<List<Object>> FromJson(string json)
    {
        Wrapper2 wrapper = JsonUtility.FromJson<Wrapper2>(json);
        List<List<Object>> objectList = new List<List<Object>>(10);

        if (wrapper is null)
            return objectList;

        foreach (Wrapper1 list in wrapper.Items)
            objectList.Add(list.Items);

        return objectList;
    }

    public static string ToJson(List<List<Object>> array, bool prettyPrint)
    {

        Wrapper2 wrapper2 = new Wrapper2();

        foreach (List<Object> list in array)
        {
            Wrapper1 wrapper1 = new Wrapper1();

            foreach (Object item in list)
                wrapper1.Items.Add(item);
            wrapper2.Items.Add(wrapper1);
        }

        return JsonUtility.ToJson(wrapper2, prettyPrint);
    }

    [Serializable]
    private class Wrapper1
    {
        public List<Object> Items = new List<Object>();
    }

    [Serializable]
    private class Wrapper2
    {
        public List<Wrapper1> Items = new List<Wrapper1>();
    }
}

[Serializable]
public class Object
{
    public Vector3 _pos;
    public ObjectType _type;

    public Object(Vector3 pos, ObjectType type)
    {
        _pos = pos;
        _type = type;
    }
}


