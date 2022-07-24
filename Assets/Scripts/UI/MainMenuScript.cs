using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

//followed tutorial https://www.youtube.com/watch?v=76WOa6IU_s8&
public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    GameObject settingsScreen;
    [SerializeField]
    GameObject levelsScreen;
    [SerializeField]
    LevelsScreenScript levelsScreenScript;
    [SerializeField]
    AudioMixer mixer;

	private void Start()
	{
        //sets current Audiovalue
        if (PlayerPrefs.HasKey("MasterVol"))
            mixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
        else
            mixer.SetFloat("MasterVol", 0);
    }
    
    /// <summary>
    /// Loads newest level
    /// </summary>
    public void Play()
    {
        GameManager.instance.LoadLastLevel();
    }

    /// <summary>
    /// Opens Settingsmenu
    /// </summary>
    public void OpenSettings()
    {
        settingsScreen.SetActive(true);
    }

    /// <summary>
    /// Closes Settingsmenu
    /// </summary>
    public void CloseSettings()
    {
        settingsScreen.SetActive(false);
    }

    /// <summary>
    /// Opens Levelsmenu
    /// </summary>
    public void OpenLevels()
    {
        levelsScreen.SetActive(true);
        levelsScreenScript.UpdateLevelProgression();
    }

    /// <summary>
    /// Closes Levelsmenu
    /// </summary>
    public void CloseLevels()
    {
        levelsScreen.SetActive(false);
    }

    /// <summary>
    /// Quits game and saves progress
    /// </summary>
    public void QuitGame()
    {
        GameManager.instance.SaveSessionProgress();
        Application.Quit();
    }

    /// <summary>
    /// Deletes current progress
    /// </summary>
    public void Delete()
	{
        GameManager.instance.DeleteProgress();
    }
}
