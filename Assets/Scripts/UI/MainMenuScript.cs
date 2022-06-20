using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    GameObject settingsScreen;
    [SerializeField]
    GameObject levelsScreen;
    [SerializeField]
    LevelsScreenScript levelsScreenScript;

	private void Start()
	{
        //"instantiate" singelton, so music start playing
        GameManager.instance.instantiate();
    }

    public void Play()
    {
        GameManager.instance.LoadLastLevel();
    }

    public void OpenSettings()
    {
        settingsScreen.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsScreen.SetActive(false);
    }

    public void OpenLevels()
    {
        levelsScreen.SetActive(true);
    }

    public void CloseLevels()
    {
        levelsScreen.SetActive(false);
    }

    public void QuitGame()
    {
        PlayerPrefs.SetInt("maxLevelIndex", GameManager.instance.getMaxLevelIndex());
        Application.Quit();
    }

    public void Delete()
	{
        PlayerPrefs.DeleteAll();
    }
}
