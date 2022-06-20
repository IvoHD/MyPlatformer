using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsScreenScript : MonoBehaviour
{
    [SerializeField]
    List<Button> buttons;

	private void Start()
	{
		for (int i = 0; i < GameManager.instance.getMaxLevelIndex(); i++)
		{
			buttons[i].interactable = true;
		}
	}

	public void LoadLevel(int levelIndex)
	{
        GameManager.instance.LoadLevel(levelIndex);
	}
}
