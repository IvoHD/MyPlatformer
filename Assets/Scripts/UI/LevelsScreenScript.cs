using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsScreenScript : MonoBehaviour
{
    [SerializeField]
    List<Button> buttons;

	public void LoadLevel(int levelIndex)
	{
        GameManager.instance.LoadLevel(levelIndex);
	}

	/// <summary>
	/// display all "unlocked" levels
	/// </summary>
	public void UpdateLevelProgression()
	{
		foreach (Button button in buttons)
			button.interactable = false;

		for (int i = 0; i < GameManager.instance.getMaxLevelIndex(); i++)
		{
			buttons[i].interactable = true;
		}
	}
}
