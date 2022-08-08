using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavingManager : MonoBehaviour
{
    public static SavingManager instance;
    public List<ISaveable> objectsToSave = new List<ISaveable>();

    static string currenStatePath;
    static string defaultStatePath;

    void Awake()
    {
        if (instance is null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

	private void Start()
	{
        currenStatePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "GameState.json";
        defaultStatePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "DefaultGameState.json";
    }

	public void Register(ISaveable obj)
	{
        objectsToSave.Add(obj);
	}

    public void Deregister(ISaveable obj)
	{
        objectsToSave.Remove(obj);
	}
    
    public void Save()
    {
        int index = SceneManager.GetActiveScene().buildIndex - 1;
        List<List<Object>> objectList = new List<List<Object>>();

        //gets curret game state
        StreamReader reader = new StreamReader(currenStatePath);
        string json = reader.ReadToEnd();
        reader.Close();
        objectList = JsonHelper.FromJson(json);

        objectList[index] = new List<Object>();

        foreach (ISaveable obj in objectsToSave)
            objectList[index].Add(new Object(obj.GetPositionToSave(), obj.GetObjectType()));
        objectsToSave = new List<ISaveable>();

        json = JsonHelper.ToJson(objectList, true);

        StreamWriter writer = new StreamWriter(currenStatePath);

        writer.Write(json);
        writer.Close();
    }

    public List<Object> GetCurrentState()
    {
        int index = SceneManager.GetActiveScene().buildIndex - 1;
        StreamReader reader = new StreamReader(currenStatePath);
        string json = reader.ReadToEnd();
        reader.Close();

        List<List<Object>> objectList = JsonHelper.FromJson(json);


        return objectList[index];
    }

    public void Reset()
    {
        StreamReader reader = new StreamReader(defaultStatePath);
        string json = reader.ReadToEnd();
        reader.Close();
        StreamWriter writer = new StreamWriter(currenStatePath);
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

